using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
    }
}