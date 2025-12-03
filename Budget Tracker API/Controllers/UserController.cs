using Budget_Tracker_API.Database;
using Budget_Tracker_API.DTO;
using Budget_Tracker_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Budget_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/User/signup
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if email already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
                return BadRequest(new { message = "Email already registered." });

            // Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return BadRequest(new { message = "Invalid email or password." });

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!validPassword)
                return BadRequest(new { message = "Invalid email or password." });

            // Return basic user info (without password)
            return Ok(new
            {
                message = "Login successful.",
                user = new
                {
                    user.UserId,
                    user.Name,
                    user.Email,
                    user.CreatedAt
                }
            });
        }
    }
}
