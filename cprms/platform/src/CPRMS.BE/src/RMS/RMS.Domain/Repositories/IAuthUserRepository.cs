using Rms.Domain.Entities;

namespace Rms.Domain.Repositories
{
    public interface IAuthUserRepository
    {
        public Task<UserSystem?> GetUserByEmailAsync(string email);
    }
}
