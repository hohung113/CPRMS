using MediatR;
using Rms.Application.Modules.UserManagement.Command;
using Rms.Domain.Modules.UserSystem.Interface;

namespace Rms.Application.Modules.UserManagement.CommandHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public LoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Implement Here 
            throw new NotImplementedException();
        }
    }
}
