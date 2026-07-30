using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories.Interfaces
{
    /// <summary>
    /// Defines repository contract for authentication and user account operations.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Retrieves a user entity by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The matching <see cref="User"/> entity if found; otherwise, <c>null</c>.</returns>
        Task<User?> GetUserByEmail(string email);

        /// <summary>
        /// Creates and persists a new user entity in the database.
        /// </summary>
        /// <param name="user">The user entity to create.</param>
        /// <returns>The created <see cref="User"/> entity with assigned identifiers.</returns>
        Task<User> CreateUser(User user);
    }
}