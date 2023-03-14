using Challenge.Application.Contracts.City;
using Challenge.Domain.CityAgg;
using Challenge.Domain.UserAgg;
using Challenge.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.EFCore.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly DbSet<User> _user;
        public UserRepository(ChallengeContext context) : base(context)
        {
            _user = context.Set<User>();
        }
    }
}
