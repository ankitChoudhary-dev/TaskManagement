using TaskManagement.API.DTOModels.Auth;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    /// <summary>
    /// Implements service logic for user authentication, registration, password validation, and JWT generation.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="authRepository">The repository used for user persistence operations.</param>
        /// <param name="tokenService">The service used for generating JWT security tokens.</param>
        public AuthService(
            IAuthRepository authRepository,
            ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Authenticates a user with email and password, returning user info and a JWT token upon success.
        /// </summary>
        /// <param name="request">The login credentials.</param>
        /// <returns>A <see cref="LoginResponseDTO"/> indicating success status and payload.</returns>
        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            try
            {
                var user = await _authRepository.GetUserByEmail(request.Email);

                if (user == null)
                {
                    return new LoginResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Account not found. Please sign up to continue."
                    };
                }

                var isPasswordValid = BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

                if (!isPasswordValid)
                {
                    return new LoginResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Invalid password"
                    };
                }

                var token = _tokenService.GenerateToken(
                    user.Id,
                    user.Email,
                    user.Role);

                return new LoginResponseDTO
                {
                    Name = user.Name,
                    IsSuccess = true,
                    Message = "Login successful",
                    Token = token,
                    Role = user.Role
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Registers a new user with hashed credentials and creates an active account.
        /// </summary>
        /// <param name="request">The registration details.</param>
        /// <returns>A <see cref="LoginResponseDTO"/> indicating registration result.</returns>
        public async Task<LoginResponseDTO> Register(RegisterRequestDTO request)
        {
            try
            {
                var existingUser = await _authRepository.GetUserByEmail(request.Email);

                if (existingUser != null)
                {
                    return new LoginResponseDTO
                    {
                        IsSuccess = false,
                        Message = "An account with this email already exists."
                    };
                }

                var newUser = await _authRepository.CreateUser(new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Role = "User",
                    IsActive = true,
                    CreatedOn = DateTime.UtcNow
                });

                return new LoginResponseDTO
                {
                    IsSuccess = true,
                    Message = "Registration successful"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}