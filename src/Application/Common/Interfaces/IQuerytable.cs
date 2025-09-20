using Application.DTOs;
namespace Application.Common.Interfaces;

public interface IQuery
{
    Task<PagedResult<UserDTO>> QueryTableAsync(int page = 1, int pageSize = 10, CancellationToken ct = default);
}
