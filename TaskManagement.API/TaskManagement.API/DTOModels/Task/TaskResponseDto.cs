namespace TaskManagement.API.DTOModels.Task
{
    /// <summary>
    /// Data transfer object representing task information returned to API clients.
    /// </summary>
    public class TaskResponseDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the task.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated project.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the name of the associated project.
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the title or brief summary of the task.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description or instructions for the task.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the current status of the task.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the priority level assigned to the task.
        /// </summary>
        public string Priority { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the assigned user, if any.
        /// </summary>
        public int? AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the name of the assigned user, if any.
        /// </summary>
        public string? AssignedUserName { get; set; }

        /// <summary>
        /// Gets or sets the target completion date for the task.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who created the task.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp when the task was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}