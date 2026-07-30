using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.DTOModels.Auth;
using TaskManagement.API.Models;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Handles user authentication, registration, and session management endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">Service interface for authentication processing.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates user credentials and returns an access token upon successful login.
        /// </summary>
        /// <param name="request">Contains the user's login payload including email and password.</param>
        /// <returns>An HTTP response with user authentication details or error message.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
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

        /// <summary>
        /// Registers a new user account with the provided registration details.
        /// </summary>
        /// <param name="request">Contains the registration payload with email and credentials.</param>
        /// <returns>An HTTP response confirming account creation or detailed error response.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
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