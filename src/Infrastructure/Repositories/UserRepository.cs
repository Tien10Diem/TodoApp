using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Infrastructure.Data;
using Domain.Entities;
using Application.DTOs;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TodoApp2Context _db;
    public UserRepository(TodoApp2Context db)
    {
        _db = db;
    }
    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await _db.Users.AddAsync(user, ct);
        await _db.SaveChangesAsync(ct);
    }
    public async Task<bool> ExistsByUserOrEmailAsync(string? userName, string? Email, CancellationToken ct = default)
    {
        return await _db.Users.AnyAsync(u => !String.IsNullOrEmpty(u.UserName) && u.UserName == userName || !String.IsNullOrEmpty(u.UserEmail) && u.UserEmail == Email, ct);
    }

    public async Task<User?> GetByUserOrEmailAsync(string? userName, string? Email, CancellationToken ct = default)
    {
        return await _db.Users
        .FirstOrDefaultAsync(u =>
            (!string.IsNullOrEmpty(userName) && u.UserName == userName) ||
            (!string.IsNullOrEmpty(Email) && u.UserEmail == Email), ct);
    }


}