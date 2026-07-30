using TaskManagement.API.DTOModels.Dashboard;

namespace TaskManagement.API.Repositories.Interfaces
{
    /// <summary>
    /// Defines repository contract for retrieving system-wide dashboard statistics and metrics.
    /// </summary>
    public interface IDashboardRepository
    {
        /// <summary>
        /// Asynchronously retrieves aggregated dashboard statistics including task and project counts.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing the <see cref="DashboardStatsDto"/> stats.</returns>
        Task<DashboardStatsDto> GetStatsAsync();
    }
}