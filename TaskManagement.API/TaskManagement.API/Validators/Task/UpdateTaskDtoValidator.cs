using FluentValidation;

namespace TaskManagement.API.Validators.Task
{
    public class UpdateTaskDtoValidator : AbstractValidator<DTOModels.Task.UpdateTaskDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task Title is required.")
                .Length(3, 150).WithMessage("Title must be between 3 and 150 characters.");

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