using Core.Domain.Interfaces;

namespace Rms.Domain.Modules.UserSystem.Interface
{
    public interface IUserRepository : IBaseRepository<Rms.Domain.Entities.UserSystem>
    {
        Task<Rms.Domain.Entities.UserSystem> GetUserByEmailAsync(string email);
    }
}
