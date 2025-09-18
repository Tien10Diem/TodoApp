using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;


namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository{
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
    public async Task<bool> ExitsByUserOrEmailAsync(string? userName, string? Email, CancellationToken ct = default)
    {
        return await _db.Users.AnyAsync(u =>  u.UserName == userName || u.UserEmail == Email, ct); //cần sửa lại
    }
}