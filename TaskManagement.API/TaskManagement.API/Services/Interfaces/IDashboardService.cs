using TaskManagement.API.DTOModels.Dashboard;

namespace TaskManagement.API.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}