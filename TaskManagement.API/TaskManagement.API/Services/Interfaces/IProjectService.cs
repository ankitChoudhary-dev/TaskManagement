using TaskManagement.API.DTOModels;
using TaskManagement.API.DTOModels.Project;

namespace TaskManagement.API.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectResponseDTO>> GetAllProjects();

        Task<ProjectResponseDTO?> GetProjectById(int id);

        Task<ProjectResponseDTO> CreateProject(
            CreateProjectDTO request,
            int createdBy);

        Task<ProjectResponseDTO?> UpdateProject(
            int id,
            UpdateProjectDTO request);

        Task<bool> DeleteProject(int id);
    }
}