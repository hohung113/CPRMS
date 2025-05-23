using FluentValidation;

namespace Rms.Application.Modules.Acedamic.Professions.Commands.UpdateProfession
{
    public class UpdateProfessionCommandValidator : AbstractValidator<UpdateProfessionCommand>
    {
        public UpdateProfessionCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Profession name must not be empty.")
                .MaximumLength(100).WithMessage("Profession name must not exceed 100 characters.");
            RuleFor(dto => dto.Code)
                .NotEmpty().WithMessage("Profession name must not be empty.")
                .MaximumLength(100).WithMessage("Profession name must not exceed 100 characters.");

        }
    }
}
