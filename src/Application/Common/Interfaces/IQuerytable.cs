using Application.DTOs;
namespace Application.Common.Interfaces;

public interface IQuery
{
    Task<PagedResult<JobsDTO>> QueryTableAsync(int userid, int page = 1, int pageSize = 5, CancellationToken ct = default);
    Task<PagedResult<JobsDTO>> QueryBinTableAsync(int userid, int page = 1, int pageSize = 5, CancellationToken ct = default);
}
