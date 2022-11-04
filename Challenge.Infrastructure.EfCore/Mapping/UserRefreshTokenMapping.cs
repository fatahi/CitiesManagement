using Challenge.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Infrastructure.EfCore.Mapping
{
    public class UserRefreshTokenMapping : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshTokens");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired().HasMaxLength(64);
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.IsValid).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.UserRefreshTokens).HasForeignKey(x => x.UserId);
        }
    }
}
