using FluentValidation;

namespace TaskManagement.API.Validators.Task
{
    public class TaskFilterDtoValidator : AbstractValidator<DTOModels.Task.TaskFilterDto>
    {
        public TaskFilterDtoValidator()
        {
            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId must be a valid ID.")
                .When(x => x.ProjectId.HasValue);

            RuleFor(x => x.AssignedTo)
                .GreaterThan(0).WithMessage("AssignedTo must be a valid user ID.")
                .When(x => x.AssignedTo.HasValue);
        }
    }
}