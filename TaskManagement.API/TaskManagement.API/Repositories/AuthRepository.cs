using Microsoft.EntityFrameworkCore;
using TaskManagement.API;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    /// <summary>
    /// Implements repository operations for user authentication and account management using Entity Framework Core.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing user data.</param>
        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves a user entity matching the specified email address.
        /// </summary>
        /// <param name="email">The email address of the user to locate.</param>
        /// <returns>The <see cref="User"/> entity if found; otherwise, <c>null</c>.</returns>
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        /// <summary>
        /// Asynchronously creates and persists a new user entity in the database.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        /// <returns>The created <see cref="User"/> entity with assigned database identifiers.</returns>
        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}