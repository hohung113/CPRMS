using System;
using FluentValidation;
using Rms.Application.Modules.Acedamic.Profession.Dtos;

namespace Rms.Application.Modules.Acedamic.Profession.Commands.CreateProfession
{
    public class CreateProfessionCommandValidator : AbstractValidator<CreateProfessionCommand>
    {
        public CreateProfessionCommandValidator()
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
