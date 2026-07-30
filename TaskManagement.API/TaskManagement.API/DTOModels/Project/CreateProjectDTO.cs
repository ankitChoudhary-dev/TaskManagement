using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.DTOModels.Project
{
    /// <summary>
    /// Data transfer object containing details required to create a new project.
    /// </summary>
    public class CreateProjectDTO
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(150, ErrorMessage = "Project name cannot exceed 150 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets an optional description detailing the project goals or scope.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the optional planned start date for the project.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the optional target end date for the project.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the current operational status of the project.
        /// </summary>
        [Required(ErrorMessage = "Project status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } = string.Empty;
    }
}