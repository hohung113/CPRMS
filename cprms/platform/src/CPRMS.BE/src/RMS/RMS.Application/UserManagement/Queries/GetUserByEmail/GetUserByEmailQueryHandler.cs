using MediatR;
using Rms.Application.UserManagement.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Application.UserManagement.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        Task<UserDto> IRequestHandler<GetUserByEmailQuery, UserDto>.Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
