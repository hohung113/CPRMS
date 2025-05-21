using Rms.Domain.Modules.Academic.Interface;

namespace Rms.Infrastructure.Modules.Academic.Repository
{
    public class SemesterRepository : BaseRepository<RmsDbContext, Semester>, ISemesterRepository
    {
        public SemesterRepository(RmsDbContext context) : base(context)
        {
        }
        // Need Can Override Here, Customize
        //public override async Task<UserSystem?> GetEntity(Guid id)
        //{
        //    return null;
        //}
    }
}
