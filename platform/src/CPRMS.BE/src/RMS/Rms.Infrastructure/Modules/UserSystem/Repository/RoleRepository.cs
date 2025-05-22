namespace Rms.Infrastructure.Modules.UserSystem.Repository
{
    public class RoleRepository : BaseRepository<RmsDbContext, Role>, IRoleRepository
    {
        private readonly RmsDbContext _rmsDbContext;
        public RoleRepository(RmsDbContext dbContext) : base(dbContext)
        {
            _rmsDbContext = dbContext;
        }
    }
}