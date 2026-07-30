using TaskManagement.API.Models;
using TaskManagement.API.DTOModels.Auth;

namespace TaskManagement.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
        Task<LoginResponseDTO> Register(RegisterRequestDTO request);
    }
}