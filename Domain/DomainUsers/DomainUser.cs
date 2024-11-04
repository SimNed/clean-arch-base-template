namespace Domain.DomainUsers
{
    public class DomainUser
    {
        public DomainUserId UserId { get; private set; }

        public DomainUser(DomainUserId userId) 
        { 
            UserId = userId;
        }
    }
}
