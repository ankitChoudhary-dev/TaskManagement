using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                return await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}