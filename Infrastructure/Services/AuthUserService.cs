using Application.Abstractions.AuthUser;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AuthUserService : IAuthUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthUserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthUserDTO?> FindByUserNameAsync(string username)
        {
            IdentityUser? user = await _userManager.FindByNameAsync(username);
            return user != null ? new AuthUserDTO(Guid.Parse(user.Id), user.UserName, user.Email) : null;
        }

        public async Task<AuthUserDTO?> FindByEmailAsync(string email)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(email);
            return user != null ? new AuthUserDTO(Guid.Parse(user.Id), user.UserName, user.Email) : null;
        }

        public async Task<AuthUserDTO?> FindByIdAsync(Guid id)
        {
            IdentityUser? user = await _userManager.FindByIdAsync(id.ToString());
            return user != null ? new AuthUserDTO(Guid.Parse(user.Id), user.UserName, user.Email) : null;
        }

        public async Task<bool> IsUsernameAvailable(string username)
        {
            return await _userManager.FindByNameAsync(username) == null;
        }

        public async Task<bool> IsEmailConfirmedAsync(string email)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(email);
            return user != null && await _userManager.IsEmailConfirmedAsync(user);
        }
    }
}
