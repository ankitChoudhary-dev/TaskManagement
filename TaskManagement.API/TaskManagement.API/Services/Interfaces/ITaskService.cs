using TaskManagement.API.DTOModels.Task;

namespace TaskManagement.API.Services.Interfaces
{
    /// <summary>
    /// Defines service contract for managing task business logic and data transformations.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Retrieves a filtered collection of tasks mapped to response DTOs.
        /// </summary>
        /// <param name="filter">The filtering criteria for search query, status, priority, project, and assignee.</param>
        /// <returns>A collection of matching <see cref="TaskResponseDto"/> objects.</returns>
        Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(TaskFilterDto filter);

        /// <summary>
        /// Retrieves a single task by its unique identifier mapped to a response DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>The matching <see cref="TaskResponseDto"/> if found; otherwise, <c>null</c>.</returns>
        Task<TaskResponseDto?> GetTaskByIdAsync(int id);

        /// <summary>
        /// Creates a new task associated with the creator's user identifier.
        /// </summary>
        /// <param name="dto">The creation payload containing task details.</param>
        /// <param name="createdById">The unique identifier of the user creating the task.</param>
        /// <returns>The created <see cref="TaskResponseDto"/>.</returns>
        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, int createdById);

        /// <summary>
        /// Updates an existing task matching the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="dto">The update payload containing modified task values.</param>
        /// <returns>The updated <see cref="TaskResponseDto"/> if found; otherwise, <c>null</c>.</returns>
        Task<TaskResponseDto?> UpdateTaskAsync(int id, UpdateTaskDto dto);

        /// <summary>
        /// Deletes a task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to delete.</param>
        /// <returns><c>true</c> if deletion succeeded; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteTaskAsync(int id);
    }
}