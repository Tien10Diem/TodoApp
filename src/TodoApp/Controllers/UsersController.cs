using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Services;
using Application.Common.Interfaces;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IQuery _queryService;
    
    public UsersController(IQuery queryService)
    {
        _queryService = queryService;
    }
    
    [HttpGet("paged")]
    public async Task<IActionResult> GetUsersPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var result = await _queryService.QueryTableAsync(page, pageSize);
        return Ok(result);
    }
}
