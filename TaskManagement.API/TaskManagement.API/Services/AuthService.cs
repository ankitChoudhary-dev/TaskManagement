using TaskManagement.API.Models;
using TaskManagement.API.DTOModels.Auth;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            IAuthRepository authRepository,
            ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

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