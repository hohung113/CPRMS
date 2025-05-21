using Rms.Domain.Entities;

namespace Rms.Domain.Modules.UserSystem.Interface
{
    public interface IUserRepository
    {
        Task<Rms.Domain.Entities.UserSystem> GetUserByEmailAsync(string email);
    }
}
