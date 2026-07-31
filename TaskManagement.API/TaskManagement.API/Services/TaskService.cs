using AutoMapper;
using TaskManagement.API.DTOModels.Task;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API.Services
{
    /// <summary>
    /// Implements service operations for managing task business logic, repository interactions, and DTO mappings using AutoMapper.
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="taskRepository">The repository used for task data operations.</param>
        /// <param name="mapper">The AutoMapper instance for object transformations.</param>
        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Asynchronously retrieves a collection of filtered tasks mapped to response DTOs.
        /// </summary>
        /// <param name="filter">The filtering options for querying tasks.</param>
        /// <returns>A collection of <see cref="TaskResponseDto"/> objects.</returns>
        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(TaskFilterDto filter)
        {
            var tasks = await _taskRepository.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
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

            return _mapper.Map<TaskResponseDto>(task);
        }

        /// <summary>
        /// Asynchronously creates a new task entity, reloads details with relationships, and returns the response DTO.
        /// </summary>
        /// <param name="dto">The creation payload containing task details.</param>
        /// <param name="createdById">The unique identifier of the user creating the task.</param>
        /// <returns>The created <see cref="TaskResponseDto"/>.</returns>
        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, int createdById)
        {
            var taskEntity = _mapper.Map<TaskItem>(dto);
            taskEntity.CreatedBy = createdById;
            taskEntity.CreatedOn = DateTime.UtcNow;

            var createdTask = await _taskRepository.CreateAsync(taskEntity);
            var result = await _taskRepository.GetByIdAsync(createdTask.Id);

            return _mapper.Map<TaskResponseDto>(result ?? createdTask);
        }

        /// <summary>
        /// Asynchronously updates an existing task, reloads navigation properties, and maps to a response DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="dto">The update payload containing updated values.</param>
        /// <returns>The updated <see cref="TaskResponseDto"/> if found; otherwise, <c>null</c>.</returns>
        public async Task<TaskResponseDto?> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var taskToUpdate = _mapper.Map<TaskItem>(dto);

            var updatedTask = await _taskRepository.UpdateAsync(id, taskToUpdate);
            if (updatedTask == null) return null;

            var result = await _taskRepository.GetByIdAsync(updatedTask.Id);
            return _mapper.Map<TaskResponseDto>(result ?? updatedTask);
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
    }
}