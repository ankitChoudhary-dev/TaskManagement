using Microsoft.EntityFrameworkCore;
using TaskManagement.API;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    /// <summary>
    /// Implements repository operations for managing projects and their related user data in the database.
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for project data operations.</param>
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves all projects ordered by creation date descending.
        /// </summary>
        /// <returns>A list of <see cref="Project"/> entities including creator details.</returns>
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

        /// <summary>
        /// Asynchronously retrieves a project by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        /// <returns>The matching <see cref="Project"/> entity if found; otherwise, <c>null</c>.</returns>
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

        /// <summary>
        /// Asynchronously creates a new project and loads creator details.
        /// </summary>
        /// <param name="project">The project entity to add.</param>
        /// <returns>The created <see cref="Project"/> entity with loaded navigation properties.</returns>
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

        /// <summary>
        /// Asynchronously updates an existing project and reloads creator details.
        /// </summary>
        /// <param name="project">The project entity containing updated values.</param>
        /// <returns>The updated <see cref="Project"/> entity with loaded navigation properties.</returns>
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

        /// <summary>
        /// Asynchronously deletes a project entity from the database.
        /// </summary>
        /// <param name="project">The project entity to remove.</param>
        /// <returns><c>true</c> if the deletion succeeded.</returns>
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