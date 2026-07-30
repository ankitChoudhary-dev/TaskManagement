using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.API.DTOModels.Task;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Handles API endpoints for task creation, retrieval, updates, and deletion.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TasksController"/> class.
        /// </summary>
        /// <param name="taskService">Service interface for handling task business logic and data operations.</param>
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Retrieves a filtered list of all tasks based on query parameters.
        /// </summary>
        /// <param name="filter">Filter parameters to narrow down the retrieved task list.</param>
        /// <returns>An HTTP response containing the collection of matching tasks.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskFilterDto filter)
        {
            var tasks = await _taskService.GetAllTasksAsync(filter);
            return Ok(tasks);
        }

        /// <summary>
        /// Retrieves a specific task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to retrieve.</param>
        /// <returns>An HTTP response containing the requested task, or a 404 Not Found response.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found." });
            }

            return Ok(task);
        }

        /// <summary>
        /// Creates a new task in the system. Requires Admin authorization.
        /// </summary>
        /// <param name="dto">Contains details necessary for creating a new task.</param>
        /// <returns>An HTTP 201 Created response containing the newly created task entity.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("id")?.Value;

            int createdById = int.TryParse(userIdClaim, out var parsedId) ? parsedId : 1;

            var createdTask = await _taskService.CreateTaskAsync(dto, createdById);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        /// <summary>
        /// Updates an existing task by its unique identifier. Requires Admin authorization.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="dto">Contains updated values for the task fields.</param>
        /// <returns>An HTTP response with the updated task object, or a 404 Not Found response.</returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTask = await _taskService.UpdateTaskAsync(id, dto);

            if (updatedTask == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found." });
            }

            return Ok(updatedTask);
        }

        /// <summary>
        /// Deletes a task from the system by its ID. Requires Admin authorization.
        /// </summary>
        /// <param name="id">The unique identifier of the task to remove.</param>
        /// <returns>An HTTP response confirming task deletion, or a 404 Not Found response.</returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);

            if (!success)
            {
                return NotFound(new { message = $"Task with ID {id} not found." });
            }

            return Ok(new { message = "Task deleted successfully." });
        }
    }
}