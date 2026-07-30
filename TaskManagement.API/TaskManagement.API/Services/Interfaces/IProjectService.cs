using TaskManagement.API.DTOModels;
using TaskManagement.API.DTOModels.Project;

namespace TaskManagement.API.Services.Interfaces
{
    /// <summary>
    /// Defines service contract for managing project business logic and data transformations.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Retrieves all projects mapped to response DTOs.
        /// </summary>
        /// <returns>A list of <see cref="ProjectResponseDTO"/> objects.</returns>
        Task<List<ProjectResponseDTO>> GetAllProjects();

        /// <summary>
        /// Retrieves a single project by its unique identifier mapped to a response DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        /// <returns>The matching <see cref="ProjectResponseDTO"/> if found; otherwise, <c>null</c>.</returns>
        Task<ProjectResponseDTO?> GetProjectById(int id);

        /// <summary>
        /// Creates a new project associated with the specified user.
        /// </summary>
        /// <param name="request">The creation payload containing project details.</param>
        /// <param name="createdBy">The unique identifier of the user creating the project.</param>
        /// <returns>The created <see cref="ProjectResponseDTO"/>.</returns>
        Task<ProjectResponseDTO> CreateProject(CreateProjectDTO request, int createdBy);

        /// <summary>
        /// Updates an existing project matching the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the project to update.</param>
        /// <param name="request">The update payload containing modified project values.</param>
        /// <returns>The updated <see cref="ProjectResponseDTO"/> if found; otherwise, <c>null</c>.</returns>
        Task<ProjectResponseDTO?> UpdateProject(int id, UpdateProjectDTO request);

        /// <summary>
        /// Deletes a project by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the project to delete.</param>
        /// <returns><c>true</c> if deletion succeeded; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteProject(int id);
    }
}