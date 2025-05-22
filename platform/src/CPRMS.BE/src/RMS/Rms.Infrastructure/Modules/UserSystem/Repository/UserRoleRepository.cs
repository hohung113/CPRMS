namespace Rms.Infrastructure.Modules.UserSystem.Repository
{
    public class UserRoleRepository : BaseRepository<RmsDbContext, UserRole> , IUserRoleRepository
    {
        private readonly RmsDbContext _rmsDbContext;
        public UserRoleRepository(RmsDbContext dbContext) : base(dbContext)
        {
            _rmsDbContext = dbContext;
        }
    }
}