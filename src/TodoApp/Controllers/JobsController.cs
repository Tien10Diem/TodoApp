using Microsoft.AspNetCore.Mvc;
using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("addjob")]
        public async Task<IActionResult> CreateJob([FromBody] JobsDTO jobDto)
        {
            try
            {
                await _jobService.AddJobAsync(jobDto);
                return Ok(new { message = "Job created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetJobsPaged([FromQuery] int userid, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                var result = await _jobService.GetJobsPagedAsync(userid, page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("by-id/{id:int}")]
        public async Task<IActionResult> GetJob(int id, CancellationToken ct = default)
        {
            try
            {
                Job? job = await _jobService.GetJob(id, ct);
                if (job == null)
                {
                    return NotFound(new { message = "Job not found" });
                }
                return Ok(job);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateJob/{id:int}")]
        public async Task<IActionResult> UpdateJob([FromRoute] int id, [FromBody] JobsDTO job, CancellationToken ct = default)
        {
            try
            {
                if (id != job.JobId) return BadRequest("Not Found");
                await _jobService.UpdateJob(job, ct);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("softdelete/{id:int}")]
        public async Task<IActionResult> softdelete(int id, CancellationToken ct = default)
        {
            try
            {
                Job? job = await _jobService.GetJob(id, ct);
                if (job == null) return BadRequest("Not Found");
                await _jobService.softdelete(id, ct);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getbinpage")]
        public async Task<IActionResult> getBinPage([FromQuery] int userid, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                var result = await _jobService.GetJobsBinPagedAsync(userid, page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getbin/{id:int}/{userid:int}")]
        public async Task<IActionResult> GetBin(int id, int userid, CancellationToken ct = default)
        {
            try
            {
                Job? job = await _jobService.GetBin(id, userid, ct);
                if (job == null) return BadRequest("Not Found!!!");
                return Ok(job);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpDelete("delete/{id:int}/{userid:int}")]
        public async Task<IActionResult> delete(int id, int userid, CancellationToken ct = default)
        {
            try
            {
                await _jobService.deletebin(id, userid, ct);
                return Ok();
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }
        [HttpPut("restore/{id:int}/{userid:int}")]
        public async Task<IActionResult> restore(int id, int userid, CancellationToken ct = default)
        {
            try
            {
                await _jobService.restore(id, userid, ct);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
    }
}
