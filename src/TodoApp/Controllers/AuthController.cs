using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _db;
        public AuthController(AuthService db)
        {
            _db = db;
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
            if (!ok) return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new { message = "Login successfully" });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            // Lấy id theo ClaimTypes.NameIdentifier
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            // User.Identity.Name lấy từ claim ClaimTypes.Name
            var username = User.Identity?.Name;

            return Ok(new { userId, username });
        }


    }
}
