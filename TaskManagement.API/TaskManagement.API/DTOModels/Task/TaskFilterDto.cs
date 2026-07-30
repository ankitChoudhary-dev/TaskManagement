namespace TaskManagement.API.DTOModels.Task
{
    public class TaskFilterDto
    {
        public string? Title { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public int? ProjectId { get; set; }
        public int? AssignedTo { get; set; }
    }
}