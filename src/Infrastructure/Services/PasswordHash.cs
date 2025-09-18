using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces; 
namespace Infrastructure.Repositories;

public class PasswordHash : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}