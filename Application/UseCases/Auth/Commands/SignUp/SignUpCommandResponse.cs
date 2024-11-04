namespace Application.UseCases.Auth.Commands.SignUp
{
    public class SignUpCommandResponse
    {
        public Guid UserId { get; }
        public string UserName { get; }
        public string Email { get; }

        public SignUpCommandResponse(Guid userId, string userName, string email)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
        }
    }
}
