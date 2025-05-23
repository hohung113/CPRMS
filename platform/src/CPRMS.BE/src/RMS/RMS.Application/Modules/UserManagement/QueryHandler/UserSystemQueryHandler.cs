namespace Rms.Application.Modules.UserManagement.QueryHandler
{
    public class UserSystemQueryHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly AccountSettings _accountSettings;
        public UserSystemQueryHandler(IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository,
            IOptions<AccountSettings> accountSettings)
        {
            _userRepository = userRepository;
            _accountSettings = accountSettings.Value;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }
        public async Task<GoogleLoginResponseDto> GetUserSystemByEmail(GetGoogleUserDetailsQuery request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentNullException(nameof(request.Email), "Email must not be null or empty.");
            }
            GoogleLoginResponseDto loginModelResponse = new GoogleLoginResponseDto();
            // Check Email Admin 
            if (request.Email == _accountSettings.Admin.Email)
            {
                loginModelResponse.Email = request.Email;
                loginModelResponse.RoleNames = new List<String> { CprmsConstants.CprmsAdmin };
                loginModelResponse.FullName = CprmsConstants.CprmsAdminDisplayName;
            }
            else
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new InvalidOperationException("User not found with the provided email.");
                }

                loginModelResponse.Email = user.Email;
                loginModelResponse.Id = user.Id;
                loginModelResponse.FullName = user.FullName;
                var roleNames = new List<string>();
                var listRoleId = await _userRoleRepository.FindAsync(x => x.UserId == loginModelResponse.Id);
                foreach (var item in listRoleId)
                {
                    var role = await _roleRepository.GetEntity(item.RoleId);
                    roleNames.Add(role.RoleName);
                }
                loginModelResponse.RoleNames = roleNames;
            }
            return loginModelResponse;
        }
    }
}