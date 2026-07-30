using Microsoft.EntityFrameworkCore;
using TaskManagement.API;
using TaskManagement.API.DTOModels.Task;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;

namespace TaskManagement.API.Repositories
{
    /// <summary>
    /// Implements repository operations for managing task items in the database.
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for task data operations.</param>
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves a collection of tasks based on provided filtering criteria.
        /// </summary>
        /// <param name="filter">The filter DTO containing title, status, priority, project, and assignee filters.</param>
        /// <returns>A collection of matching <see cref="TaskItem"/> entities including navigation properties.</returns>
        public async Task<IEnumerable<TaskItem>> GetAllAsync(TaskFilterDto filter)
        {
            var query = _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.AssignedUser)
                .AsQueryable();

            // Search by Title
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(t => t.Title.Contains(filter.Title));
            }

            // Filter by Status
            if (!string.IsNullOrWhiteSpace(filter.Status))
            {
                query = query.Where(t => t.Status == filter.Status);
            }

            // Filter by Priority
            if (!string.IsNullOrWhiteSpace(filter.Priority))
            {
                query = query.Where(t => t.Priority == filter.Priority);
            }

            // Filter by Project
            if (filter.ProjectId.HasValue)
            {
                query = query.Where(t => t.ProjectId == filter.ProjectId.Value);
            }

            // Filter by Assigned User
            if (filter.AssignedTo.HasValue)
            {
                query = query.Where(t => t.AssignedTo == filter.AssignedTo.Value);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a task item by its unique identifier along with its navigation properties.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>The matching <see cref="TaskItem"/> entity if found; otherwise, <c>null</c>.</returns>
        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.AssignedUser)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Asynchronously creates and persists a new task item in the database.
        /// </summary>
        /// <param name="task">The task item entity to create.</param>
        /// <returns>The created <see cref="TaskItem"/> entity with database-generated identifiers.</returns>
        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        /// <summary>
        /// Asynchronously updates an existing task item matching the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="updatedTask">The task object containing modified property values.</param>
        /// <returns>The updated <see cref="TaskItem"/> entity if found; otherwise, <c>null</c>.</returns>
        public async Task<TaskItem?> UpdateAsync(int id, TaskItem updatedTask)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null) return null;

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;
            existingTask.Priority = updatedTask.Priority;
            existingTask.AssignedTo = updatedTask.AssignedTo;
            existingTask.DueDate = updatedTask.DueDate;

            await _context.SaveChangesAsync();
            return existingTask;
        }

        /// <summary>
        /// Asynchronously deletes a task item from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to delete.</param>
        /// <returns><c>true</c> if deletion succeeded; otherwise, <c>false</c>.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}