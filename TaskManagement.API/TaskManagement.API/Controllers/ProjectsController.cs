using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.API.Models;
using TaskManagement.API.Services.Interfaces;
using TaskManagement.API.DTOModels.Project;

namespace TaskManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;


        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }



        // GET: api/Projects
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



        // GET: api/Projects/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            try
            {
                var project =
                    await _projectService.GetProjectById(id);


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



        // POST: api/Projects
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProject(
            [FromBody] CreateProjectDTO request)
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


                var userId =
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier);


                if (string.IsNullOrWhiteSpace(userId))
                {
                    return Unauthorized(new
                    {
                        message = "Invalid token."
                    });
                }


                var createdBy = int.Parse(userId);


                var project =
                    await _projectService.CreateProject(
                        request,
                        createdBy);


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



        // PUT: api/Projects/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(
            int id,
            [FromBody] UpdateProjectDTO request)
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


                var project =
                    await _projectService.UpdateProject(
                        id,
                        request);


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



        // DELETE: api/Projects/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var deleted =
                    await _projectService.DeleteProject(id);


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