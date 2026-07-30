using TaskManagement.API.DTOModels.User;

namespace TaskManagement.API.Services.Interfaces
{
    /// <summary>
    /// Defines service contract for managing user business logic and data transformations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Asynchronously retrieves all users mapped to response DTOs.
        /// </summary>
        /// <returns>A collection of <see cref="UserDTO"/> objects.</returns>
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}