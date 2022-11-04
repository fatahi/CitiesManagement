using Challenge.Domain.CityAgg;
using Challenge.Domain.CountryAgg;
using Challenge.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.EfCore
{
    public class ChallengeContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public ChallengeContext(DbContextOptions<ChallengeContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
