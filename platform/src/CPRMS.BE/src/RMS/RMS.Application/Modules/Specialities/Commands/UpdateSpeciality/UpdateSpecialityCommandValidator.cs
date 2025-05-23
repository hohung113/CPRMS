using FluentValidation;

namespace Rms.Application.Modules.Specialities.Commands.UpdateSpeciality
{
    public class UpdateSpecialityCommandValidator : AbstractValidator<UpdateSpecialityCommand>
    {
        public UpdateSpecialityCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Speciciality name must not be empty.")
                .MaximumLength(100).WithMessage("Speciciality name must not exceed 100 characters.");
            RuleFor(dto => dto.ProfessionId)
                .NotEmpty().WithMessage("Profession ID must not be empty.");

        }
    }
}
