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
            // In a real app, you'd use the access token to call Google's API to get user details.
            // For this example, we'll simulate fetching user data.
            //var user = await _userRepository.GetUserByGoogleToken(request.AccessToken);
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            return new GoogleLoginResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
            };
        }
    }
}
