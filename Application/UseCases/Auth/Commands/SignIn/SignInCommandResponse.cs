namespace Application.UseCases.Auth.Commands.SignIn
{
    public class SignInCommandResponse
    {
        public string UserName { get; }

        public SignInCommandResponse(string userName)
        {
            UserName = userName;
        }
    }
}
