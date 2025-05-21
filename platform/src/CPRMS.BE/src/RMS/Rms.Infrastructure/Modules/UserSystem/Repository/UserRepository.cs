namespace Rms.Infrastructure.Modules.UserSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RmsDbContext _rmsDbContext;
        public UserRepository(RmsDbContext dbContext)
        {
            _rmsDbContext = dbContext;
        }

        public Task<Rms.Domain.Entities.UserSystem> GetUserByEmailAsync(string email)
        {
            var userDetail = _rmsDbContext.UserSystems.FirstOrDefault(x => x.Email == email  && !x.IsDeleted && !x.IsBlock);
            return Task.FromResult(userDetail);
        }
    }
}
