using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task AddJobAsync(JobsDTO jobDto)
        {
            try
            {
                await _jobRepository.AddJobAsync(jobDto);
            }
            catch
            {
                throw;
            }
        }

        public async Task<PagedResult<Job?>> GetJobsPagedAsync(int userid, int page, int pageSize, CancellationToken ct = default)
        {
            return await _jobRepository.GetJobsPagedAsync(userid, page, pageSize, ct);
        }

        public async Task<Job?> GetJob(int id, CancellationToken ct = default)
        {
            return await _jobRepository.GetJob(id, ct);
        }

        public async Task UpdateJob(JobsDTO jobDto, CancellationToken ct = default)
        {
            try
            {
                await _jobRepository.UpdateJob(jobDto, ct);
            }
            catch
            {
                throw;
            }

        }

        public async Task softdelete(int id, CancellationToken ct = default)
        {
            try
            {
                await _jobRepository.softdelete(id, ct);
            }
            catch
            {
                throw;
            }
        }

        public async Task<PagedResult<Job?>> GetJobsBinPagedAsync(int userid, int page, int pageSize, CancellationToken ct = default)
        {
            return await _jobRepository.GetJobsBinPagedAsync(userid, page, pageSize, ct);
        }
        public async Task<Job?> GetBin(int id, int userid, CancellationToken ct = default)
        {
            try
            {
                return await _jobRepository.GetBin(id, userid, ct);
            }
            catch
            {
                throw;
            }

        }
        public async Task deletebin(int id, int userid, CancellationToken ct = default)
        {
            try
            {
                await _jobRepository.deletebin(id, userid, ct);
            }
            catch
            {
                throw;
            }
        }
        public async Task restore(int id, int userid, CancellationToken ct = default)
        {
            try
            {
                await _jobRepository.restore(id, userid, ct);
            }
            catch
            {
                throw;
            }
        }
    }
}