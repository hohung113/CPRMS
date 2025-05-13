using Core.Infrastructure.Repositories;
using Rms.Domain.Entities;
using Rms.Domain.Repositories;
using Rms.Infrastructure.Persistence;

namespace Rms.Infrastructure.Repositories
{
    public class AuthUserRepository : BaseRepository<RmsDbContext, UserSystem> , IAuthUserRepository
    {
        public AuthUserRepository(RmsDbContext context) : base(context)
        {
        }
        // Need Can Override Here In Base
        public override async Task<UserSystem?> GetEntity(Guid id)
        {
            return null;
        }

        // Create New Method Defind In IAuthUserRepository
        public Task<UserSystem?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}