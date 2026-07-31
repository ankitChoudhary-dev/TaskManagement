namespace TaskManagement.API.DTOModels.Project
{
    /// <summary>
    /// Data transfer object containing the updated parameters for an existing project.
    /// </summary>
    public class UpdateProjectDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the project to update.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the updated name of the project.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated description of the project.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the updated start date for the project.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the updated target end date for the project.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the updated status of the project.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}