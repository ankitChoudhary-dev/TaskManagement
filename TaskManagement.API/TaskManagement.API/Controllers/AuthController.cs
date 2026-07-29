using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }

            try
            {
                var response = await _authService.Login(request);

                if (!response.IsSuccess)
                {
                    return Unauthorized(new { message = response.Message });
                }

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An internal server error occurred. Please try again later."
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }

            try
            {
                var response = await _authService.Register(request);

                if (!response.IsSuccess)
                {
                    return BadRequest(new { message = response.Message });
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An internal server error occurred. Please try again later."
                });
            }
        }
    }
}