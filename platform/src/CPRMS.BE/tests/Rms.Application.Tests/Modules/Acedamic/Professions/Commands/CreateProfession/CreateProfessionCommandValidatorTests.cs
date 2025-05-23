using Xunit;
using Rms.Application.Modules.Acedamic.Professions.Commands.CreateProfession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rms.Application.Modules.Specialities.Commands.CreateSpeciality;
using FluentValidation.TestHelper;

namespace Rms.Application.Modules.Acedamic.Professions.Commands.CreateProfession.Tests
{
    public class CreateProfessionCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // arrange
            var command = new CreateProfessionCommand("Test", "IT");

            var validator = new CreateProfessionCommandValidator();

            // act 
            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
        {
            // arrange
            var command = new CreateProfessionCommand("", "");

            var validator = new CreateProfessionCommandValidator();

            // act 
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }
    }
}