using Application.DTOs;
using Application.Common.Interfaces;
using Domain.Entities;

public class QueryService : IQuery
{
    private readonly IJobService _Jobs;

    public QueryService(IJobService userRepository)
    {
        _Jobs = userRepository;
    }

    public async Task<PagedResult<JobsDTO>> QueryTableAsync(
        int userid,
        int page = 1,
        int pageSize = 5,
        CancellationToken ct = default)
    {
        var result = await _Jobs.GetJobsPagedAsync(userid, page, pageSize, ct);

        var JobsDTO = result.Items
            .Where(u => u != null && u.JobFlag == 1)
            .Select(u => new JobsDTO
            {
                JobId = u!.JobId,
                JobName = u!.JobName,
                JobCreateAt = u!.JobCreateAt,
                JobMembers = u!.JobMembers,
                JobDateStart = u!.JobDateStart,
                JobDateEnd = u!.JobDateEnd,
                JobRemainingTime = u!.JobRemainingTime,
                JobStatus = u!.JobStatus
            })
            .ToList();

        return new PagedResult<JobsDTO>(JobsDTO, result.TotalCount, result.Page, result.PageSize);
    }
    public async Task<PagedResult<JobsDTO>> QueryBinTableAsync(int userid, int page = 1, int pageSize = 5, CancellationToken ct = default)
    {
        var result = await _Jobs.GetJobsBinPagedAsync(userid, page, pageSize, ct);
        var JobsDTO = result.Items
            .Where(u => u != null && u.JobFlag == 0)
            .Select(u => new JobsDTO
            {
                JobId = u!.JobId,
                JobName = u!.JobName,
                JobCreateAt = u!.JobCreateAt,
                JobMembers = u!.JobMembers,
                JobDateStart = u!.JobDateStart,
                JobDateEnd = u!.JobDateEnd,
                JobRemainingTime = u!.JobRemainingTime,
                JobStatus = u!.JobStatus
            })
            .ToList();

        return new PagedResult<JobsDTO>(JobsDTO, result.TotalCount, result.Page, result.PageSize);
    }
}

