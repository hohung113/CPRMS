using Core.Application.Dtos;
using Rms.Application.Modules.Acedamic.Professions.Commands.CreateProfession;
using Rms.Application.Modules.Acedamic.Professions.Dtos;
using Rms.Application.Modules.Specialities.Commands.CreateSpeciality;
using Rms.Application.Modules.Specialities.Commands.DeleteSpeciality;
using Rms.Application.Modules.Specialities.Commands.UpdateSpeciality;
using Rms.Application.Modules.Specialities.Dtos;
using Rms.Application.Modules.Specialities.Queries.GellAllSpecialities;

namespace Rms.API.Controllers.Modules.Academic
{
    public class SpecialityController : BaseControllerV1
    {
        private readonly GetAllSpecialitiesQueryHandler _getAllSpecialitiesQueryHandler;
        public SpecialityController(GetAllSpecialitiesQueryHandler getAllSpecialitiesQueryHandler)
        {
            _getAllSpecialitiesQueryHandler = getAllSpecialitiesQueryHandler;
        }

        [HttpPost]
        public async Task<Guid?> CreateSpeciality([FromBody] ProfessionDto request)
        {
            var command = request.Adapt<CreateProfessionCommand>();
            var result = await Dispatcher.Send(command);
            return result.Id;
        }

        [HttpGet("GetAllSpecialities")]
        public async Task<BasePagedResultDto<SpecialityDto>> GetAllSpecialities()
        {
            var result = await _getAllSpecialitiesQueryHandler.GetAllSpecialities();
            var listResults = result.ToList();
            return new BasePagedResultDto<SpecialityDto>
            {
                Items = listResults,
                TotalItems = listResults.Count
            }; ;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpeciality([FromRoute] Guid id, [FromBody] UpdateSpecialityCommand command)
        {
            command.Id = id;
            var result = await Dispatcher.Send(command);
            return Ok(new { Id = result.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeciality([FromRoute] Guid id)
        {
            var result = await Dispatcher.Send(new DeleteSpecialityCommand { Id = id });
            return Ok(new { Success = result });
        }


    }
}
