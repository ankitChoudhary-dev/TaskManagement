using FluentValidation;

namespace TaskManagement.API.Validators.Project
{
    public class CreateProjectDtoValidator : AbstractValidator<DTOModels.Project.CreateProjectDTO>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project name is required.")
                .MaximumLength(150).WithMessage("Project name cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Project status is required.")
                .MaximumLength(50).WithMessage("Status cannot exceed 50 characters.");

            // Added cross-property validation rule to ensure EndDate is after StartDate if both are provided
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate.Value)
                .WithMessage("End date must be on or after the start date.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);
        }
    }
}