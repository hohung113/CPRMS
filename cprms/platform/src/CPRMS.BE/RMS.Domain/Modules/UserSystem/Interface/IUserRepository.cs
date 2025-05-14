using Core.Domain.Entities;

namespace Rms.Domain.Modules.UserSystem.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
