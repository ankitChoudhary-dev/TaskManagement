using TaskManagement.API.DTOModels.Dashboard;

namespace TaskManagement.API.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardStatsDto> GetStatsAsync();
    }
}