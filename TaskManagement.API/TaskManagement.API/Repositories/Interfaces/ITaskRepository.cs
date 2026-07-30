using TaskManagement.API.DTOModels.Task;
using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories.Interfaces
{
    /// <summary>
    /// Defines repository contract for data access operations related to task items.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Retrieves a filtered collection of tasks based on specified criteria.
        /// </summary>
        /// <param name="filter">The filter criteria for title, status, priority, project, or user assignment.</param>
        /// <returns>A collection of matching <see cref="TaskItem"/> entities.</returns>
        Task<IEnumerable<TaskItem>> GetAllAsync(TaskFilterDto filter);

        /// <summary>
        /// Retrieves a single task item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>The matching <see cref="TaskItem"/> entity if found; otherwise, <c>null</c>.</returns>
        Task<TaskItem?> GetByIdAsync(int id);

        /// <summary>
        /// Persists a new task item entity into the database.
        /// </summary>
        /// <param name="task">The task item entity to create.</param>
        /// <returns>The created <see cref="TaskItem"/> entity with generated identifiers.</returns>
        Task<TaskItem> CreateAsync(TaskItem task);

        /// <summary>
        /// Updates an existing task item matching the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="task">The task item entity containing updated values.</param>
        /// <returns>The updated <see cref="TaskItem"/> entity if found; otherwise, <c>null</c>.</returns>
        Task<TaskItem?> UpdateAsync(int id, TaskItem task);

        /// <summary>
        /// Removes a task item entity from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to delete.</param>
        /// <returns><c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteAsync(int id);
    }
}