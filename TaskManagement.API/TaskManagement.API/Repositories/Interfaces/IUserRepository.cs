using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}