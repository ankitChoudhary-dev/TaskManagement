using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    /// <summary>
    /// Implements service operations for constructing and signing JWT access tokens.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration root containing JWT settings.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a signed JWT access token embedded with user claims, issuer, audience, and expiration details.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="email">The email address of the user.</param>
        /// <param name="role">The system role assigned to the user.</param>
        /// <returns>A signed JWT token string.</returns>
        public string GenerateToken(
            int userId,
            string email,
            string role)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(
                        JwtRegisteredClaimNames.Sub,
                        userId.ToString()),

                    new Claim(
                        JwtRegisteredClaimNames.Email,
                        email),

                    new Claim(
                        ClaimTypes.Role,
                        role)
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"]!));

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler()
                    .WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}