namespace Application.Abstractions.AuthUser
{
    public class AuthUserDTO
    {
        public AuthUserDTO(Guid id, string username, string email)
        {
            Id = id;
            UserName = username;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
    }
}
