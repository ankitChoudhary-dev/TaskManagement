using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {

            // Temporary response
            // Actual JWT generation later

            if (string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new
                {
                    message = "Email and password are required"
                });
            }


            return Ok(new
            {
                message = "Login endpoint working",
                token = "dummy-token",
                role = "Admin"
            });
        }
    }


    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}