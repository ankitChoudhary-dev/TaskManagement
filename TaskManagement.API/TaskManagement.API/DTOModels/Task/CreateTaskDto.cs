namespace TaskManagement.API.DTOModels.Task
{
    /// <summary>
    /// Data transfer object containing the necessary details to create a new task.
    /// </summary>
    public class CreateTaskDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the project to which this task belongs.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the title or brief summary of the task.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets an optional detailed explanation or requirements for the task.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the initial progress status of the task. Defaults to "Pending".
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Gets or sets the importance level of the task. Defaults to "Medium".
        /// </summary>
        public string Priority { get; set; } = "Medium";

        /// <summary>
        /// Gets or sets the optional unique identifier of the user assigned to this task.
        /// </summary>
        public int? AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the optional completion deadline for the task.
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}