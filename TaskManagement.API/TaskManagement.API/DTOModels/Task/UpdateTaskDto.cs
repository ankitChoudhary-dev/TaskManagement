namespace TaskManagement.API.DTOModels.Task
{
    /// <summary>
    /// Data transfer object containing parameters for updating an existing task's details.
    /// </summary>
    public class UpdateTaskDto
    {
        /// <summary>
        /// Gets or sets the updated title of the task.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated detailed description of the task.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the updated progress status of the task.
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Gets or sets the updated priority level of the task.
        /// </summary>
        public string Priority { get; set; } = "Medium";

        /// <summary>
        /// Gets or sets the optional unique identifier of the assigned user.
        /// </summary>
        public int? AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the updated completion deadline for the task.
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}