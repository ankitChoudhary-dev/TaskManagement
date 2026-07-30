using TaskManagement.API.DTOModels.Task;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    /// <summary>
    /// Implements service operations for managing task business logic, repository interactions, and DTO mappings.
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="taskRepository">The repository used for task data operations.</param>
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Asynchronously retrieves a collection of filtered tasks mapped to response DTOs.
        /// </summary>
        /// <param name="filter">The filtering options for querying tasks.</param>
        /// <returns>A collection of <see cref="TaskResponseDto"/> objects.</returns>
        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(TaskFilterDto filter)
        {
            var tasks = await _taskRepository.GetAllAsync(filter);
            return tasks.Select(MapToResponseDto);
        }

        /// <summary>
        /// Asynchronously retrieves a task by its unique identifier and maps it to a response DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>The matching <see cref="TaskResponseDto"/> if found; otherwise, <c>null</c>.</returns>
        public async Task<TaskResponseDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            return MapToResponseDto(task);
        }

        /// <summary>
        /// Asynchronously creates a new task entity, reloads details with relationships, and returns the response DTO.
        /// </summary>
        /// <param name="dto">The creation payload containing task details.</param>
        /// <param name="createdById">The unique identifier of the user creating the task.</param>
        /// <returns>The created <see cref="TaskResponseDto"/>.</returns>
        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, int createdById)
        {
            var taskEntity = new TaskItem
            {
                ProjectId = dto.ProjectId,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                AssignedTo = dto.AssignedTo,
                DueDate = dto.DueDate,
                CreatedBy = createdById,
                CreatedOn = DateTime.UtcNow
            };

            var createdTask = await _taskRepository.CreateAsync(taskEntity);
            var result = await _taskRepository.GetByIdAsync(createdTask.Id);

            return MapToResponseDto(result ?? createdTask);
        }

        /// <summary>
        /// Asynchronously updates an existing task, reloads navigation properties, and maps to a response DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="dto">The update payload containing updated values.</param>
        /// <returns>The updated <see cref="TaskResponseDto"/> if found; otherwise, <c>null</c>.</returns>
        public async Task<TaskResponseDto?> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var taskToUpdate = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                AssignedTo = dto.AssignedTo,
                DueDate = dto.DueDate
            };

            var updatedTask = await _taskRepository.UpdateAsync(id, taskToUpdate);
            if (updatedTask == null) return null;

            var result = await _taskRepository.GetByIdAsync(updatedTask.Id);
            return MapToResponseDto(result ?? updatedTask);
        }

        /// <summary>
        /// Asynchronously deletes a task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to delete.</param>
        /// <returns><c>true</c> if deletion succeeded; otherwise, <c>false</c>.</returns>
        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Maps a <see cref="TaskItem"/> entity model to a <see cref="TaskResponseDto"/>.
        /// </summary>
        /// <param name="task">The task entity to transform.</param>
        /// <returns>A mapped <see cref="TaskResponseDto"/> instance.</returns>
        private static TaskResponseDto MapToResponseDto(TaskItem task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                ProjectName = task.Project?.Name ?? string.Empty,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                AssignedTo = task.AssignedTo,
                AssignedUserName = task.AssignedUser?.Name,
                DueDate = task.DueDate,
                CreatedBy = task.CreatedBy,
                CreatedOn = task.CreatedOn
            };
        }
    }
}