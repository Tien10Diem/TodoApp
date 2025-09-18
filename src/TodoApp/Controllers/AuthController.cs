using Microsoft.AspNetCore.Mvc;
using Application.Common.Interfaces;
using Application.DTOs;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Azure.Messaging;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase {
        private readonly AuthService _db;
        public AuthController(AuthService db) {
            _db = db;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] registerRequestDTO request) {
            try
            {
                await _db.RegisterAsync(request);
                return Ok(new { Message = "Register successfully" });
            }
            catch (ArgumentException e) {
                return BadRequest(e);
            }
    
        }
        
    }
}
