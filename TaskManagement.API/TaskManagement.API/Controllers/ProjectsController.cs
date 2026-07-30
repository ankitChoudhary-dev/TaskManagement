using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.API.DTOModels.Project;
using TaskManagement.API.Models;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Handles API endpoints for CRUD operations and management of projects.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="projectService">Service interface for project data access and business logic.</param>
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Retrieves a list of all existing projects.
        /// </summary>
        /// <returns>An HTTP response containing the collection of projects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                var projects = await _projectService.GetAllProjects();
                return Ok(projects);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An internal server error occurred. Please try again later."
                });
            }
        }

        /// <summary>
        /// Retrieves a specific project by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the project to retrieve.</param>
        /// <returns>An HTTP response containing the requested project, or a 404 Not Found error.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            try
            {
                var project = await _projectService.GetProjectById(id);

                if (project == null)
                {
                    return NotFound(new
                    {
                        message = "Project not found."
                    });
                }

                return Ok(project);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An internal server error occurred. Please try again later."
                });
            }
        }

        /// <summary>
        /// Creates a new project in the system. Requires Admin authorization.
        /// </summary>
        /// <param name="request">Contains the details required to create the project.</param>
        /// <returns>An HTTP response with the newly created project data.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Validation failed.",
                        errors = ModelState
                            .Where(x => x.Value!.Errors.Count > 0)
                            .ToDictionary(
                                x => x.Key,
                                x => x.Value!.Errors
                                    .Select(e => e.ErrorMessage)
                                    .ToArray()
                            )
                    });
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrWhiteSpace(userId))
                {
                    return Unauthorized(new
                    {
                        message = "Invalid token."
                    });
                }

                var createdBy = int.Parse(userId);
                var project = await _projectService.CreateProject(request, createdBy);

                return Ok(project);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An internal server error occurred. Please try again later."
                });
            }
        }

        /// <summary>
        /// Updates an existing project's details by its ID. Requires Admin authorization.
        /// </summary>
        /// <param name="id">The unique identifier of the project to update.</param>
        /// <param name="request">Contains the updated fields for the project.</param>
        /// <returns>An HTTP response with the updated project details, or a 404 Not Found error.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Validation failed.",
                        errors = ModelState
                            .Where(x => x.Value!.Errors.Count > 0)
                            .ToDictionary(
                                x => x.Key,
                                x => x.Value!.Errors
                                    .Select(e => e.ErrorMessage)
                                    .ToArray()
                            )
                    });
                }

                var project = await _projectService.UpdateProject(id, request);

                if (project == null)
                {
                    return NotFound(new
                    {
                        message = "Project not found."
                    });
                }

                return Ok(project);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An internal server error occurred. Please try again later."
                });
            }
        }

        /// <summary>
        /// Deletes a project from the system by its ID. Requires Admin authorization.
        /// </summary>
        /// <param name="id">The unique identifier of the project to remove.</param>
        /// <returns>An HTTP response confirming deletion or a 404 Not Found error.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var deleted = await _projectService.DeleteProject(id);

                if (!deleted)
                {
                    return NotFound(new
                    {
                        message = "Project not found."
                    });
                }

                return Ok(new
                {
                    message = "Project deleted successfully."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An internal server error occurred. Please try again later."
                });
            }
        }
    }
}