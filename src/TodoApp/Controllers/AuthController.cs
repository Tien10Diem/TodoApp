using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Infrastructure.Helper;
using Azure.Core;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _db;
        private readonly IConfiguration _config;
        public AuthController(AuthService db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] registerRequestDTO request)
        {
            try
            {
                await _db.RegisterAsync(request);
                return Ok(new { Message = "Register successfully" });
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginRequestDTO request, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var ok = await _db.LoginAsync(request, ct);
            
            if (ok==null) return Unauthorized(new { message = "Invalid credentials" });

            var token = Infrastructure.Helper.JwtHelper.GenerateToken(ok, _config);

            return Ok(new
            {
                message = "Login successfully",
                AccessToken = token,
                expriseIn = 60 * 60
             });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdStr))
                return Unauthorized(new { message = "User Id missing in token." });

            if (!int.TryParse(userIdStr, out var userId))
                return BadRequest(new { message = "User Id in token is invalid." });

            var username = User.Identity?.Name ?? "unknown";

            return Ok(new { userId, username });
        }



    }
}
