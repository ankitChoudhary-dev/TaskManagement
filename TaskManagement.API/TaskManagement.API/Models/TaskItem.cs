namespace TaskManagement.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "Pending"; 
        public string Priority { get; set; } = "Medium"; 
        public int? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Project? Project { get; set; }
        public User? AssignedUser { get; set; }
        public User? CreatedByUser { get; set; }
    }
}