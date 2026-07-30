using TaskManagement.API.DTOModels.Auth;
using TaskManagement.API.Models;

namespace TaskManagement.API.Services.Interfaces
{
    /// <summary>
    /// Defines service contract for user authentication and registration business logic.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user with credentials and generates an access token.
        /// </summary>
        /// <param name="request">The login request payload containing email and password.</param>
        /// <returns>A <see cref="LoginResponseDTO"/> containing user details and token information.</returns>
        Task<LoginResponseDTO> Login(LoginRequestDTO request);

        /// <summary>
        /// Registers a new user account in the system.
        /// </summary>
        /// <param name="request">The registration request payload containing user details.</param>
        /// <returns>A <see cref="LoginResponseDTO"/> containing created user details and authentication token.</returns>
        Task<LoginResponseDTO> Register(RegisterRequestDTO request);
    }
}