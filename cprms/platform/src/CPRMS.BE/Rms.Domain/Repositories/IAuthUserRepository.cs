using Rms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Domain.Repositories
{
    public interface IAuthUserRepository
    {
        public Task<UserSystem?> GetUserByEmailAsync(string email);
    }
}
