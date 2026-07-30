namespace TaskManagement.API.Models
{
    /// <summary>
    /// Represents a project entity within the task management system.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the unique identifier for the project.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the optional description detailing project goals or scope.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the optional planned start date of the project.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the optional target end date of the project.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the operational status of the project.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the foreign key identifier of the user who created the project.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the project was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the user who created the project.
        /// </summary>
        public User? CreatedByUser { get; set; }

        /// <summary>
        /// Gets or sets the collection of task items associated with this project.
        /// </summary>
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}