using Core.Infrastructure.Repositories;
using Rms.Domain.Entities;
using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Infrastructure.Modules.Academic.Repository
{
    public class ProfessionRepository : BaseRepository<RmsDbContext, Profession>, IProfessionRepository
    {
        public ProfessionRepository(RmsDbContext context) : base(context)
        {
        }
        // Need Can Override Here, Customize
        //public override async Task<UserSystem?> GetEntity(Guid id)
        //{
        //    return null;
        //}
    }
}
