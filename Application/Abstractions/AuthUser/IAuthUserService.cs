namespace Application.Abstractions.AuthUser
{
    public interface IAuthUserService
    {
        Task<AuthUserDTO?> FindByIdAsync(Guid userId);
        Task<AuthUserDTO?> FindByUserNameAsync(string username);
        Task<AuthUserDTO?> FindByEmailAsync(string email);
        Task<bool> IsUsernameAvailable(string username);
        Task<bool> IsEmailConfirmedAsync(string email);
    }
}
