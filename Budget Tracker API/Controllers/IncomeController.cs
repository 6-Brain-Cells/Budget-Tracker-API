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
    public class IncomeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IncomeController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Income/add
        [HttpPost("add")]
        public async Task<IActionResult> AddIncome([FromBody] IncomeDto dto, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check user exists
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return BadRequest(new { message = "User not found." });

            var income = new Income
            {
                UserId = userId,
                Amount = dto.Amount,
                SourceName = dto.SourceName,
                IncomeDate = dto.IncomeDate,
                IsRecurring = dto.IsRecurring
            };

            _context.IncomeRecords.Add(income);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Income added successfully.", incomeId = income.IncomeId });
        }

        // GET: api/Income/user/1
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserIncomes(int userId)
        {
            var incomes = await _context.IncomeRecords
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.IncomeDate)
                .ToListAsync();

            return Ok(incomes);
        }

        // GET: api/Income/my
        [HttpGet("my")]
        public async Task<IActionResult> GetMyIncomes([FromQuery] int userId)
        {
            // Simulate "logged in user" by query parameter
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return BadRequest(new { message = "User not found." });

            var incomes = await _context.IncomeRecords
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.IncomeDate)
                .ToListAsync();

            return Ok(incomes);
        }

    }
}
