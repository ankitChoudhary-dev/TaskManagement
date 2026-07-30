using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllProjects();

        Task<Project?> GetProjectById(int id);

        Task<Project> CreateProject(Project project);

        Task<Project> UpdateProject(Project project);

        Task<bool> DeleteProject(Project project);
    }
}