using Application.Common.Interfaces;   // IAuthService, IUserRepository, IPasswordHasher
using Application.Services;           
using Infrastructure.Data;            // TodoApp2Context
using Infrastructure.Repositories;    // UserRepository     
using Microsoft.EntityFrameworkCore;  // UseSqlServer
using Microsoft.OpenApi.Models;

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
builder.Services.AddScoped<AuthService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });
});

var app = builder.Build();

// Chỉ định môi trường, hoặc bật luôn
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
        
    });
}

app.MapControllers();
app.Run();

