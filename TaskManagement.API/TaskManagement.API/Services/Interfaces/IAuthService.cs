using TaskManagement.API.Models;

namespace TaskManagement.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<LoginResponse> Register(RegisterRequest request);
    }
}