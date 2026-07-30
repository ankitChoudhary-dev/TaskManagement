namespace TaskManagement.API.DTOModels.Project
{
    public class ProjectResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public int CreatedBy { get; set; }

        public string CreatedByName { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }
    }
}
