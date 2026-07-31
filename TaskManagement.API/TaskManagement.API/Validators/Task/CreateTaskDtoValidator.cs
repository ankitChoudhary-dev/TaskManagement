using FluentValidation;

namespace TaskManagement.API.Validators.Task
{
    public class CreateTaskDtoValidator : AbstractValidator<DTOModels.Task.CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required.");

            RuleFor(x => x.AssignedTo)
                .GreaterThan(0).WithMessage("AssignedTo must be a valid user ID.")
                .When(x => x.AssignedTo.HasValue);
        }
    }
}