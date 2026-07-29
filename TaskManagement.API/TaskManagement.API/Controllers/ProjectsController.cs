using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetProjects()
        {
            return Ok(new
            {
                message = "Get all projects endpoint working",
                data = new List<object>()
            });
        }


        [HttpGet("{id}")]
        public IActionResult GetProject(int id)
        {
            return Ok(new
            {
                message = $"Get project {id} endpoint working"
            });
        }


        [HttpPost]
        public IActionResult CreateProject()
        {
            return Ok(new
            {
                message = "Create project endpoint working"
            });
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id)
        {
            return Ok(new
            {
                message = $"Update project {id} endpoint working"
            });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            return Ok(new
            {
                message = $"Delete project {id} endpoint working"
            });
        }

    }
}