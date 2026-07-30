namespace TaskManagement.API.DTOModels.Task
{
    /// <summary>
    /// Data transfer object containing query filter parameters for searching and narrowing down task lists.
    /// </summary>
    public class TaskFilterDto
    {
        /// <summary>
        /// Gets or sets an optional search string to filter tasks by title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets an optional status filter (e.g., Pending, InProgress, Completed).
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets an optional priority level filter (e.g., Low, Medium, High).
        /// </summary>
        public string? Priority { get; set; }

        /// <summary>
        /// Gets or sets an optional project identifier to retrieve tasks belonging to a specific project.
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets an optional user identifier to filter tasks assigned to a specific user.
        /// </summary>
        public int? AssignedTo { get; set; }
    }
}