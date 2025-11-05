
using LibraryBackend.Data;
using LibraryBackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add CORS Configurations

const string AllowReactApp = "AllowReactApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowReactApp, policy =>
    {
        policy
           .WithOrigins("http://localhost:5173") // React dev URL
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
    });
});


// Add services to the container
builder.Services.AddControllers();

// Configure EF Core with SQLite
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add your BookService if you have one
builder.Services.AddScoped<IBookService, BookService>();

// Add AuthService for authentication
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure middleware
app.UseHttpsRedirection();

// Apply CORS middleware
if (app.Environment.IsDevelopment())
{
    app.UseCors(AllowReactApp);
}

app.UseAuthorization();
app.MapControllers();

app.Run();