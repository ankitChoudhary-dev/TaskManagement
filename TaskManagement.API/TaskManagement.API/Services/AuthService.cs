using TaskManagement.API.Models;
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

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                var user = await _authRepository.GetUserByEmail(request.Email);

                if (user == null)
                {
                    return new LoginResponse
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
                    return new LoginResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid password"
                    };
                }


                var token = _tokenService.GenerateToken(
                    user.Id,
                    user.Email,
                    user.Role);


                return new LoginResponse
                {
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


        public async Task<LoginResponse> Register(RegisterRequest request)
        {
            try
            {
                var existingUser = await _authRepository.GetUserByEmail(request.Email);

                if (existingUser != null)
                {
                    return new LoginResponse
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


                return new LoginResponse
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