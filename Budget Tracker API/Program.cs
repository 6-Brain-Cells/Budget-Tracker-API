using Budget_Tracker_API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// --- FIX 1: Remove conflicting OpenApi lines ---
// You were mixing the new Microsoft OpenAPI and Swashbuckle. 
// Stick to Swashbuckle (AddSwaggerGen) for now as it works best with SwaggerUI.
// builder.Services.AddOpenApi(); <--- REMOVE THIS

builder.Services.AddEndpointsApiExplorer(); // Add this line (required for minimal APIs/Swagger)
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

// --- FIX 2: Ensure Swagger runs in Production ---
// app.MapOpenApi(); <--- REMOVE THIS (Conflict)

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // --- FIX 3: REMOVE THE LEADING SLASH ---
    // Change "/swagger/v1/swagger.json" to "swagger/v1/swagger.json"
    // This fixes the 404 error if your app is hosted in a sub-folder.
    c.SwaggerEndpoint("swagger/v1/swagger.json", "My API V1");

    // Keep this to make Swagger the homepage
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();