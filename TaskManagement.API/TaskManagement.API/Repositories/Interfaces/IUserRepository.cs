using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories.Interfaces
{
    /// <summary>
    /// Defines repository contract for data access operations related to user entities.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves all registered users from the system.
        /// </summary>
        /// <returns>A collection of all <see cref="User"/> entities.</returns>
        Task<IEnumerable<User>> GetAllAsync();
    }
}