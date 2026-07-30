using TaskManagement.API.DTOModels;
using TaskManagement.API.DTOModels.Project;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    /// <summary>
    /// Implements service operations for managing project business logic, repository calls, and DTO mappings.
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="projectRepository">The repository used for project persistence operations.</param>
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Asynchronously retrieves all projects and maps them to response DTOs.
        /// </summary>
        /// <returns>A list of <see cref="ProjectResponseDTO"/> objects.</returns>
        public async Task<List<ProjectResponseDTO>> GetAllProjects()
        {
            try
            {
                var projects = await _projectRepository.GetAllProjects();

                return projects
                    .Select(MapToResponse)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a project by its unique identifier and maps it to a response DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        /// <returns>The matching <see cref="ProjectResponseDTO"/> if found; otherwise, <c>null</c>.</returns>
        public async Task<ProjectResponseDTO?> GetProjectById(int id)
        {
            try
            {
                var project = await _projectRepository.GetProjectById(id);

                if (project == null)
                {
                    return null;
                }

                return MapToResponse(project);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously creates a new project entity and maps the saved entity to a response DTO.
        /// </summary>
        /// <param name="request">The project creation request payload.</param>
        /// <param name="createdBy">The unique identifier of the user creating the project.</param>
        /// <returns>The created <see cref="ProjectResponseDTO"/>.</returns>
        public async Task<ProjectResponseDTO> CreateProject(
            CreateProjectDTO request,
            int createdBy)
        {
            try
            {
                var project = new Project
                {
                    Name = request.Name,
                    Description = request.Description,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Status = request.Status,
                    CreatedBy = createdBy,
                    CreatedOn = DateTime.UtcNow
                };

                var createdProject = await _projectRepository.CreateProject(project);

                return MapToResponse(createdProject);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing project entity with new values and maps it to a response DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the project to update.</param>
        /// <param name="request">The updated project details.</param>
        /// <returns>The updated <see cref="ProjectResponseDTO"/> if found; otherwise, <c>null</c>.</returns>
        public async Task<ProjectResponseDTO?> UpdateProject(
            int id,
            UpdateProjectDTO request)
        {
            try
            {
                var existingProject = await _projectRepository.GetProjectById(id);

                if (existingProject == null)
                {
                    return null;
                }

                existingProject.Name = request.Name;
                existingProject.Description = request.Description;
                existingProject.StartDate = request.StartDate;
                existingProject.EndDate = request.EndDate;
                existingProject.Status = request.Status;

                var updatedProject = await _projectRepository.UpdateProject(existingProject);

                return MapToResponse(updatedProject);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes a project entity matching the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the project to delete.</param>
        /// <returns><c>true</c> if the deletion succeeded; otherwise, <c>false</c>.</returns>
        public async Task<bool> DeleteProject(int id)
        {
            try
            {
                var project = await _projectRepository.GetProjectById(id);

                if (project == null)
                {
                    return false;
                }

                return await _projectRepository.DeleteProject(project);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Maps a <see cref="Project"/> entity model to a <see cref="ProjectResponseDTO"/>.
        /// </summary>
        /// <param name="project">The project entity to transform.</param>
        /// <returns>A mapped <see cref="ProjectResponseDTO"/> instance.</returns>
        private ProjectResponseDTO MapToResponse(Project project)
        {
            return new ProjectResponseDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                CreatedBy = project.CreatedBy,
                CreatedByName = project.CreatedByUser?.Name ?? string.Empty,
                CreatedOn = project.CreatedOn
            };
        }
    }
}