using Application.Common.Interfaces;   // IAuthService, IUserRepository, IPasswordHasher
using Application.Services;           // AuthService (nếu bạn có)
using Infrastructure.Data;            // TodoApp2Context
using Infrastructure.Repositories;    // UserRepository       // PasswordHasher (nếu bạn có)
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;  // UseSqlServer
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args); 

// Lấy connection string từ appsettings.json -> "ConnectionStrings": { "DefaultConnection": "..." }
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký DbContext + SQL Server
builder.Services.AddDbContext<TodoApp2Context>(options =>
    options.UseSqlServer(connectionString));


// Đăng ký DI cho repository & service
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHash>();

builder.Services.AddControllers();

var app = builder.Build();          

app.MapControllers();
app.Run();
