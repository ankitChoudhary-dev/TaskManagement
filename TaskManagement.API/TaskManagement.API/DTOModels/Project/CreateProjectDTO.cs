using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.DTOModels.Project
{
    public class CreateProjectDTO
    {
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(150, ErrorMessage = "Project name cannot exceed 150 characters.")]
        public string Name { get; set; } = string.Empty;


        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }


        public DateTime? StartDate { get; set; }


        public DateTime? EndDate { get; set; }


        [Required(ErrorMessage = "Project status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } = string.Empty;
    }
}