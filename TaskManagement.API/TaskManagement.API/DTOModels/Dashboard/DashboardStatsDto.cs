namespace TaskManagement.API.DTOModels.Dashboard
{
    /// <summary>
    /// Data transfer object summarizing system-wide dashboard statistics and task counts.
    /// </summary>
    public class DashboardStatsDto
    {
        /// <summary>
        /// Gets or sets the total number of projects in the system.
        /// </summary>
        public int TotalProjects { get; set; }

        /// <summary>
        /// Gets or sets the overall count of tasks across all projects.
        /// </summary>
        public int TotalTasks { get; set; }

        /// <summary>
        /// Gets or sets the total count of tasks that have not yet started.
        /// </summary>
        public int PendingTasks { get; set; }

        /// <summary>
        /// Gets or sets the total count of tasks currently in progress.
        /// </summary>
        public int InProgressTasks { get; set; }

        /// <summary>
        /// Gets or sets the total count of completed tasks.
        /// </summary>
        public int CompletedTasks { get; set; }

        /// <summary>
        /// Gets or sets the count of tasks marked with high priority.
        /// </summary>
        public int HighPriorityTasks { get; set; }
    }
}