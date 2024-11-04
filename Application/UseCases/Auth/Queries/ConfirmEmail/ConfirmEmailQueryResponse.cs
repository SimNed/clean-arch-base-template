namespace Application.UseCases.Auth.Queries.ConfirmEmail
{
    public class ConfirmEmailQueryResponse
    {
        public string Email { get; }

        public ConfirmEmailQueryResponse(string email)
        {
            Email = email;
        }
    }
}
