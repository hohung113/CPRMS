using Rms.Application.Modules.Specialities.Dtos;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Application.Modules.Specialities.Queries.GellAllSpecialities
{
    public class GetAllSpecialitiesQueryHandler
    {
        private readonly ISpecialityRepository _specialityRepository;
        public GetAllSpecialitiesQueryHandler(ISpecialityRepository specialityRepository)
        {
            _specialityRepository = specialityRepository;
        }
        public async Task<IEnumerable<SpecialityDto>> GetAllSpecialities(CancellationToken cancellationToken = default)
        {
            var result = await _specialityRepository.GetEntities();
            var specialityDto = result.Adapt<List<SpecialityDto>>();
            return specialityDto;
        }
    }
}
