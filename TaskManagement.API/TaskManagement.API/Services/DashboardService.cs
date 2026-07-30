using TaskManagement.API.DTOModels.Dashboard;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    /// <summary>
    /// Implements service operations for retrieving dashboard metrics and statistics.
    /// </summary>
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardService"/> class.
        /// </summary>
        /// <param name="dashboardRepository">The repository used to fetch dashboard data.</param>
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        /// <summary>
        /// Asynchronously retrieves aggregated stats for projects and tasks across status and priority levels.
        /// </summary>
        /// <returns>A task containing the aggregated <see cref="DashboardStatsDto"/> metrics.</returns>
        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            return await _dashboardRepository.GetStatsAsync();
        }
    }
}