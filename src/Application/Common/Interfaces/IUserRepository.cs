using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExitsByUserOrEmailAsync(string? userName ,string? Email, CancellationToken ct = default);
        Task AddAsync(User user, CancellationToken ct = default);
    }
}