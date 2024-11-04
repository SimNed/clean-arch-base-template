using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.DomainUsers;

namespace Infrastructure.Configurations
{
    public class DomainUserConfiguration : IEntityTypeConfiguration<DomainUser>
    {
        public void Configure(EntityTypeBuilder<DomainUser> builder)
        {
            builder.HasKey(p => p.UserId);

            builder.Property(p => p.UserId)
                .HasConversion(
                    userId => userId.value,
                    value => new DomainUserId(value)
                );
        }
    }
}
