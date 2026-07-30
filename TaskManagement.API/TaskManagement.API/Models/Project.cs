namespace TaskManagement.API.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public User? CreatedByUser { get; set; }

        // Navigation property for EF Core relationship mapping
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}