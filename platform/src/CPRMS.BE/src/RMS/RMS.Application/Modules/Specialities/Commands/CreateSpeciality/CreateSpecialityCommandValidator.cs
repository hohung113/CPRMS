using FluentValidation;

namespace Rms.Application.Modules.Specialities.Commands.CreateSpeciality
{
    public class CreateSpecialityCommandValidator : AbstractValidator<CreateSpecialityCommand>
    {
        public CreateSpecialityCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Profession name must not be empty.")
                .MaximumLength(100).WithMessage("Profession name must not exceed 100 characters.");
            RuleFor(dto => dto.ProfessionId)
                .NotEmpty().WithMessage("Profession name must not be empty.");
        }
    }
}
