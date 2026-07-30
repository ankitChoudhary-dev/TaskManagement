namespace TaskManagement.API.DTOModels.Project
{
    /// <summary>
    /// Data transfer object representing the detailed information of a project returned to clients.
    /// </summary>
    public class ProjectResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the project.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the optional detailed description of the project.
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
        /// Gets or sets the current operational status of the project.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the user who created the project.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user who created the project.
        /// </summary>
        public string CreatedByName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the UTC timestamp when the project was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}