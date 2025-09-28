using Domain.Entities;
using Application.DTOs;


namespace Application.Common.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsByUserOrEmailAsync(string? userName, string? Email, CancellationToken ct = default);

    Task<User?> GetByUserOrEmailAsync(string? userName, string? Email, CancellationToken ct = default);
    Task AddAsync(User user, CancellationToken ct = default);
    
}
