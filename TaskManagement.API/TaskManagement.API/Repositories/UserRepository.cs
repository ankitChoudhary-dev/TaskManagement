using Microsoft.EntityFrameworkCore;
using TaskManagement.API;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    /// <summary>
    /// Implements repository operations for retrieving user entity data from the database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for user data access.</param>
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves all registered users without change tracking overhead.
        /// </summary>
        /// <returns>A collection of all <see cref="User"/> entities.</returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }
    }
}