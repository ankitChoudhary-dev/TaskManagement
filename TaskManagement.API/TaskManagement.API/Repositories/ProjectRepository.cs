using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            try
            {
                return await _context.Projects
                    .Include(p => p.CreatedByUser)
                    .OrderByDescending(p => p.CreatedOn)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Project?> GetProjectById(int id)
        {
            try
            {
                return await _context.Projects
                    .Include(p => p.CreatedByUser)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Project> CreateProject(Project project)
        {
            try
            {
                await _context.Projects.AddAsync(project);

                await _context.SaveChangesAsync();

                await _context.Entry(project)
                    .Reference(p => p.CreatedByUser)
                    .LoadAsync();

                return project;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Project> UpdateProject(Project project)
        {
            try
            {
                _context.Projects.Update(project);

                await _context.SaveChangesAsync();

                await _context.Entry(project)
                    .Reference(p => p.CreatedByUser)
                    .LoadAsync();

                return project;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteProject(Project project)
        {
            try
            {
                _context.Projects.Remove(project);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}