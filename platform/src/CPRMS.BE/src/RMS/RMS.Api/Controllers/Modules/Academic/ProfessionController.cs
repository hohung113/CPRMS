using Rms.Application.Modules.Acedamic.Profession.Commands.CreateProfession;
using Rms.Application.Modules.Acedamic.Profession.Commands.DeleteProfession;
using Rms.Application.Modules.Acedamic.Profession.Commands.UpdateProfession;
using Rms.Application.Modules.Acedamic.Profession.Queries.GellAllProfessions;

namespace Rms.API.Controllers.Modules.Academic
{
    public class ProfessionController : BaseControllerV1
    {
        private readonly GetAllProfessionsQueryHandler _getAllProfessionsQueryHandler;
        public ProfessionController(GetAllProfessionsQueryHandler getAllProfessionsQueryHandler)
        {
            _getAllProfessionsQueryHandler = getAllProfessionsQueryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfession([FromBody] CreateProfessionCommand command)
        {

            var result = await Dispatcher.Send(command);
            return Ok(result);
        }
        [HttpGet("GetAllProfessions")]
        public async Task<IActionResult> GetAllProfessions()
        {
            var result = await _getAllProfessionsQueryHandler.GetAllProfessions();
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfession([FromRoute] Guid id, [FromBody] UpdateProfessionCommand command)
        {
            command.Id = id;
            command.LastModifiedBy = Guid.Parse(CurrentUserId);
            await Dispatcher.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfession([FromRoute] Guid id)
        {
            await Dispatcher.Send(new DeleteProfessionCommand { Id = id });
            return NoContent();
        }


    }
}
