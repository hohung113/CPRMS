using Rms.Application.Modules.Acedamic.Profession.Dtos;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Acedamic.Profession.Queries.GellAllProfessions
{
    public class GetAllProfessionsQueryHandler
    {
        private readonly IProfessionRepository _professionRepository;
        public GetAllProfessionsQueryHandler(IProfessionRepository professionRepository)
        {
            _professionRepository = professionRepository;
        }
        public async Task<IEnumerable<ProfessionDto>> GetAllProfessions(CancellationToken cancellationToken = default)
        {
            var result = await _professionRepository.GetEntities();
            var prodessionDto = result.Adapt<List<ProfessionDto>>();
            return prodessionDto;
        }
    }
}
