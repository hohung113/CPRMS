using Core.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Rms.Application.Modules.Acedamic.Professions.Commands.CreateProfession;
using Rms.Application.Modules.Acedamic.Professions.Commands.DeleteProfession;
using Rms.Application.Modules.Acedamic.Professions.Commands.UpdateProfession;
using Rms.Application.Modules.Acedamic.Professions.Dtos;
using Rms.Application.Modules.Acedamic.Professions.Queries.GellAllProfessions;
using Rms.Application.Modules.Acedamic.Semesters.Dtos;
using Rms.Domain.Constants;

namespace Rms.API.Controllers.Modules.Academic
{
    public class ProfessionController : BaseControllerV1
    {
        private readonly GetAllProfessionsQueryHandler _getAllProfessionsQueryHandler;
        public ProfessionController(GetAllProfessionsQueryHandler getAllProfessionsQueryHandler)
        {
            _getAllProfessionsQueryHandler = getAllProfessionsQueryHandler;
        }

        [Authorize(Roles = CprmsRoles.Student)]
        [HttpPost]
        public async Task<Guid?> CreateProfession(ProfessionDto request)
        {
            var command = request.Adapt<CreateProfessionCommand>();
            var result = await Dispatcher.Send(command);
            return result.Id;
        }

        [Authorize(Roles = CprmsRoles.Admin)]
        [HttpGet("GetAllProfessions")]
        public async Task<BasePagedResultDto<ProfessionDto>> GetAllProfessions()
        {
            var result = await _getAllProfessionsQueryHandler.GetAllProfessions();
            var listResults = result.ToList();
            return new BasePagedResultDto<ProfessionDto>
            {
                Items = listResults,
                TotalItems = listResults.Count
            };
        }
        [HttpPut("UpdateProfessions")]
        public async Task<IActionResult> UpdateProfession(ProfessionDto request)
        {
            var command = request.Adapt<UpdateProfessionCommand>();
            var result = await Dispatcher.Send(command);
            return Ok(new { Id = result.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfession([FromRoute] Guid id)
        {
            var result = await Dispatcher.Send(new DeleteProfessionCommand { Id = id });
            return Ok(new { Success = result });
        }


    }
}
