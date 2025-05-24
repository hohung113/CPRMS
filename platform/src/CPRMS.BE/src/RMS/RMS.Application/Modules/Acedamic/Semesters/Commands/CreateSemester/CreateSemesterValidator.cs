using FluentValidation.Results;
using Rms.Application.Modules.Acedamic.Semesters.Dtos;

namespace Rms.Application.Modules.Acedamic.Semesters.Commands.CreateSemester
{
    public class CreateSemesterValidator : AbstractValidator<SemesterDto>
    {
        public CreateSemesterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must be provided.")
                .MaximumLength(250).WithMessage("Name must not exceed 250 characters.");
        }
    }
}
