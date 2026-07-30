using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Handles API endpoints for retrieving dashboard analytics and statistical summaries.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="dashboardService">Service interface for processing dashboard metrics.</param>
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Retrieves aggregated statistical data and metrics for the user dashboard.
        /// </summary>
        /// <returns>An HTTP response containing the dashboard statistics object.</returns>
        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _dashboardService.GetDashboardStatsAsync();
            return Ok(stats);
        }
    }
}