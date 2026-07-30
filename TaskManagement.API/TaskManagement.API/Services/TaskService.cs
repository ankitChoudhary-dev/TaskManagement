using TaskManagement.API.DTOModels.Task;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(TaskFilterDto filter)
        {
            var tasks = await _taskRepository.GetAllAsync(filter);
            return tasks.Select(MapToResponseDto);
        }

        public async Task<TaskResponseDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            return MapToResponseDto(task);
        }

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

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }

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