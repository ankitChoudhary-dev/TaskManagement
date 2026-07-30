namespace TaskManagement.API.Models
{
    /// <summary>
    /// Represents an individual task entity within a project in the task management system.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the task.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key identifier of the project to which this task belongs.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the title or concise summary of the task.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the optional detailed description or requirements for the task.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the current progress status of the task. Defaults to "Pending".
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Gets or sets the priority level of the task. Defaults to "Medium".
        /// </summary>
        public string Priority { get; set; } = "Medium";

        /// <summary>
        /// Gets or sets the optional foreign key identifier of the user assigned to complete the task.
        /// </summary>
        public int? AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the optional target completion date for the task.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the foreign key identifier of the user who created the task.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp when the task was created.
        /// </summary>
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        #region Navigation Properties

        /// <summary>
        /// Gets or sets the navigation property for the associated project.
        /// </summary>
        public Project? Project { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the user assigned to this task.
        /// </summary>
        public User? AssignedUser { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the user who created this task.
        /// </summary>
        public User? CreatedByUser { get; set; }

        #endregion
    }
}