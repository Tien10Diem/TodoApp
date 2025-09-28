using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly TodoApp2Context _db;

        public JobRepository(TodoApp2Context db)
        {
            _db = db;
        }

        public async Task AddJobAsync(JobsDTO jobDto)
        {
            var job = new Job
            {
                JobId = jobDto.JobId,
                JobName = jobDto.JobName,
                JobCreateAt = DateTime.UtcNow,
                JobMembers = jobDto.JobMembers,
                JobDateEnd = jobDto.JobDateEnd,
                JobDateStart = jobDto.JobDateStart,
                JobRemainingTime = jobDto.JobRemainingTime,
                JobStatus = jobDto.JobStatus,
                JobUpdateAt = DateTime.UtcNow,
                JobFlag = 1,
                UserId = jobDto.UserId
            };

            _db.Jobs.Add(job);
            await _db.SaveChangesAsync();
        }

        public async Task<PagedResult<Job?>> GetJobsPagedAsync(
            int userid,
            int page,
            int pageSize,
            CancellationToken ct = default)
        {


            var totalCount = await _db.Jobs.CountAsync(j => j.JobFlag == 1 && j.UserId == userid, ct);
            Console.WriteLine($"Total count: {totalCount}");

            var jobs = await _db.Jobs.Where(j => j.JobFlag == 1 && j.UserId == userid)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedResult<Job?>(jobs, totalCount, page, pageSize);
        }

        public async Task<Job?> GetJob(int Id, CancellationToken ct = default)
        {
            var job = await _db.Jobs
           .AsNoTracking()
           .FirstOrDefaultAsync(j => j.JobId == Id && j.JobFlag == 1, ct);

            return job;
        }

        public async Task UpdateJob(JobsDTO jobDto, CancellationToken ct = default)
        {
            var job = await _db.Jobs.FirstOrDefaultAsync(j => j.JobId == jobDto.JobId, ct);

            if (job == null) return;
            job.JobName = jobDto.JobName;
            job.JobMembers = jobDto.JobMembers;
            job.JobDateStart = jobDto.JobDateStart;
            job.JobDateEnd = jobDto.JobDateEnd;
            job.JobRemainingTime = jobDto.JobRemainingTime;
            job.JobStatus = jobDto.JobStatus;
            job.JobUpdateAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
        }

        public async Task softdelete(int id, CancellationToken ct = default)
        {
            var job = await _db.Jobs.FirstOrDefaultAsync(j => j.JobId == id, ct);
            if (job == null) return;
            job.JobFlag = 0;
            job.JobDeleteAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        public async Task<PagedResult<Job?>> GetJobsBinPagedAsync(int userid, int page, int pageSize, CancellationToken ct = default)
        {
            var totalCount = await _db.Jobs.CountAsync(j => j.JobFlag == 0 && j.UserId == userid, ct);
            Console.WriteLine($"Total count: {totalCount}");

            var jobs = await _db.Jobs.Where(j => j.JobFlag == 0 && j.UserId == userid)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedResult<Job?>(jobs, totalCount, page, pageSize);
        }
        public async Task<Job?> GetBin(int id, int userid, CancellationToken ct = default)
        {
            var job = await _db.Jobs.FirstOrDefaultAsync(j => j.JobId == id && j.UserId == userid && j.JobFlag == 0, ct);
            if (job == null) return null;
            return job;
        }

        public async Task deletebin(int id, int userid, CancellationToken ct = default)
        {
            try
            {
                var job = await _db.Jobs.FirstOrDefaultAsync(j => j.JobId == id, ct);
                if (job == null) return;
                _db.Jobs.Remove(job);
                await _db.SaveChangesAsync();
            }
            catch (Exception er)
            {
                throw new Exception("erorr", er);
            }
        }

        public async Task restore(int id, int userid, CancellationToken ct = default)
        {
            var job = await _db.Jobs.FirstOrDefaultAsync(j => j.JobId == id & j.UserId == userid, ct);
            if (job == null) return;
            job.JobFlag = 1;
            job.JobDeleteAt = null;
            await _db.SaveChangesAsync();
        }
    }
}