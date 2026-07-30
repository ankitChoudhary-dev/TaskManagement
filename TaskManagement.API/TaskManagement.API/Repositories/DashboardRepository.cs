using Microsoft.EntityFrameworkCore;
using TaskManagement.API.DTOModels.Dashboard;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

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