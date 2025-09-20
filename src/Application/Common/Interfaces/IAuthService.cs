using Application.DTOs;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(registerRequestDTO request, CancellationToken ct = default);
    Task<User?> LoginAsync(loginRequestDTO request, CancellationToken cd = default);
}
