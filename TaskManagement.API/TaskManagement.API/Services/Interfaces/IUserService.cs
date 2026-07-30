using TaskManagement.API.DTOModels.User;

namespace TaskManagement.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}