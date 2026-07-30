using TaskManagement.API.DTOModels;
using TaskManagement.API.DTOModels.Project;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


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


                var createdProject =
                    await _projectRepository.CreateProject(project);


                return MapToResponse(createdProject);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ProjectResponseDTO?> UpdateProject(
            int id,
            UpdateProjectDTO request)
        {
            try
            {
                var existingProject =
                    await _projectRepository.GetProjectById(id);


                if (existingProject == null)
                {
                    return null;
                }


                existingProject.Name = request.Name;

                existingProject.Description = request.Description;

                existingProject.StartDate = request.StartDate;

                existingProject.EndDate = request.EndDate;

                existingProject.Status = request.Status;


                var updatedProject =
                    await _projectRepository.UpdateProject(existingProject);


                return MapToResponse(updatedProject);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> DeleteProject(int id)
        {
            try
            {
                var project =
                    await _projectRepository.GetProjectById(id);


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

                CreatedByName =
                    project.CreatedByUser?.Name ?? string.Empty,

                CreatedOn = project.CreatedOn
            };
        }
    }
}