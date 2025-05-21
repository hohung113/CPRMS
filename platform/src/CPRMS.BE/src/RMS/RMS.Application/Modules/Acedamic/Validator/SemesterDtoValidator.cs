using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Application.Modules.Acedamic.Validator
{
    public class SemesterDtoValidator : AbstractValidator<SemesterDto>
    {
        public SemesterDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Semester name must not be empty.")
                .MaximumLength(100).WithMessage("Semester name must not exceed 100 characters.");

            RuleFor(dto => dto.StartDate)
                .NotEmpty().WithMessage("Start date must not be empty.");

            RuleFor(dto => dto.EndDate)
                .NotEmpty().WithMessage("End date must not be empty.")
                .GreaterThan(dto => dto.StartDate).WithMessage("End date must be after the start date.");
        }

    }
}
