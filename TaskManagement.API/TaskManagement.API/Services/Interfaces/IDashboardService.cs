using TaskManagement.API.DTOModels.Dashboard;

namespace TaskManagement.API.Services.Interfaces
{
    /// <summary>
    /// Defines service contract for retrieving high-level dashboard metrics and statistics.
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// Asynchronously retrieves aggregated stats for projects and tasks across status and priority levels.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing the <see cref="DashboardStatsDto"/> metrics.</returns>
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}