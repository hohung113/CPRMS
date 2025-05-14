using MediatR;
using Rms.Application.UserManagement.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Application.UserManagement.Queries.GetUserByEmail
{
    internal class GetUserByEmailQuery(string email) : IRequest<UserDto>
    {
        public string Email { get; } = email;
    }
}

