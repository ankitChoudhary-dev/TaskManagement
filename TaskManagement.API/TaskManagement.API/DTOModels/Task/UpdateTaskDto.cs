using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.DTOModels.Task
{
    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "Task Title is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 150 characters.")]
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