using MediatR;
using Rms.Application.Helper;
using Rms.Application.Modules.UserManagement.Command;
using Rms.Domain.Entities;

namespace Rms.Application.Modules.UserManagement.CommandHandler
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand , UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly AccountSettings _accountSettings;
        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IOptions<AccountSettings> accountSettings,
            IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _accountSettings = accountSettings.Value;
            _userRoleRepository = userRoleRepository;
        }
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Adapt<UserSystem>();
            var isAdmin = PermissionChecker.IsAdmin(request.Email, _accountSettings.Admin.Email);
            var accountExisted = await _userRepository.GetUserByEmailAsync(request.Email);
            if (accountExisted != null)
            {
                return request.Adapt<UserDto>();
            }
            var createdUser = await _userRepository.AddEntity(entity);
            if (createdUser == null)
            {
                throw new Exception("Failed when create user in the repository.");
            }
            // Add Role 
            if (isAdmin) {
                var roleUserExisted = await _roleRepository.FirstOrDefaultAsync(x => x.RoleName == CprmsRoles.Admin);
                if (roleUserExisted != null)
                {
                    UserRole userRole = new UserRole
                    {
                        UserId = createdUser.Id,
                        RoleId = roleUserExisted.Id
                    };
                    await _userRoleRepository.AddEntity(userRole);
                }
            }
            else
            {
                var roleUserExisted = await _roleRepository.FirstOrDefaultAsync(x => x.RoleName == CprmsRoles.Student);
                if (roleUserExisted != null)
                {
                    UserRole userRole = new UserRole
                    {
                        UserId = createdUser.Id,
                        RoleId = roleUserExisted.Id
                    };
                    await _userRoleRepository.AddEntity(userRole);
                }
            }
            var userDto = createdUser.Adapt<UserDto>();
            return await Task.FromResult(userDto);
        }
    }
}