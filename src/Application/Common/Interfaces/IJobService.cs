using Application.DTOs;
using Domain.Entities;

public interface IJobService
{
    Task AddJobAsync(JobsDTO jobDto);
    Task<PagedResult<Job?>> GetJobsPagedAsync(int userid, int page, int pageSize, CancellationToken ct = default);
    Task<Job?> GetJob(int Id, CancellationToken ct = default);
    Task UpdateJob(JobsDTO job, CancellationToken ct = default);
    Task softdelete(int id, CancellationToken ct = default);
    Task<PagedResult<Job?>> GetJobsBinPagedAsync(int userid, int page, int pageSize, CancellationToken ct = default);
    Task<Job?> GetBin(int id, int userid, CancellationToken cr = default);
    Task deletebin(int id, int userid, CancellationToken ct = default);
    Task restore(int id, int userid, CancellationToken ct = default);
}