using Challenge.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.EfCore.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(64);
            builder.Property(x => x.PasswordSalt).HasMaxLength(128);
            builder.Property(x => x.Fullname).HasMaxLength(64);
            builder.HasMany(x => x.UserRefreshTokens).WithOne(x => x.User).HasForeignKey(x => x.Id);
        }
    }
}
