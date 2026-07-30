using Microsoft.EntityFrameworkCore;
using TaskManagement.API;
using TaskManagement.API.DTOModels.Dashboard;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    /// <summary>
    /// Implements repository operations for generating dashboard statistics and metric summaries.
    /// </summary>
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for querying metrics.</param>
        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves aggregated stats for projects and tasks across status and priority levels.
        /// </summary>
        /// <returns>A task containing the aggregated <see cref="DashboardStatsDto"/> metrics.</returns>
        public async Task<DashboardStatsDto> GetStatsAsync()
        {
            var totalProjects = await _context.Projects.CountAsync();
            var tasks = await _context.Tasks.ToListAsync();

            return new DashboardStatsDto
            {
                TotalProjects = totalProjects,
                TotalTasks = tasks.Count,
                PendingTasks = tasks.Count(t => t.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase)),
                InProgressTasks = tasks.Count(t => t.Status.Equals("In Progress", StringComparison.OrdinalIgnoreCase)),
                CompletedTasks = tasks.Count(t => t.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase)),
                HighPriorityTasks = tasks.Count(t => t.Priority.Equals("High", StringComparison.OrdinalIgnoreCase))
            };
        }
    }
}