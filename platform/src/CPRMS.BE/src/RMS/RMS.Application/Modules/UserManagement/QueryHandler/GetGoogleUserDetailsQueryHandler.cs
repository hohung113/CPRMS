using Core.Domain.Entities;
using Rms.Application.Modules.UserManagement.Dto;
using Rms.Domain.Modules.UserSystem.Interface;

namespace Rms.Application.Modules.UserManagement.QueryHandler
{
    public class GetGoogleUserDetailsQueryHandler
    {
        private readonly IUserRepository _userRepository;
        public GetGoogleUserDetailsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<GoogleLoginResponseDto> Handle(GetGoogleUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            return new GoogleLoginResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
            };
        }
    }
}
