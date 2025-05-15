using Core.Domain.Entities;
using Rms.Domain.Modules.UserSystem.Interface;

namespace Rms.Infrastructure.Modules.UserSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RmsDbContext _rmsDbContext;
        public UserRepository(RmsDbContext dbContext)
        {
            _rmsDbContext = dbContext;
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
