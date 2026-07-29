using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {

        [HttpGet]
        public ActionResult GetDashboard()
        {
            return Ok(new
            {
                totalProjects = 0,
                totalTasks = 0,
                pendingTasks = 0,
                inProgressTasks = 0,
                completedTasks = 0
            });
        }

    }
}