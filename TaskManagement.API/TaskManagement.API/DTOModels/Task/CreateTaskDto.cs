using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.DTOModels.Task
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "ProjectId is required.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = "Pending";

        [Required(ErrorMessage = "Priority is required.")]
        public string Priority { get; set; } = "Medium";

        public int? AssignedTo { get; set; }

        public DateTime? DueDate { get; set; }
    }
}