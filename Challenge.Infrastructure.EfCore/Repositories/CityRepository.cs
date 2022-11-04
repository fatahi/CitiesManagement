using Challenge.Application.Contracts.City;
using Challenge.Application.Contracts.Dashboard;
using Challenge.Domain.CityAgg;
using Challenge.Domain.CountryAgg;
using Challenge.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.EFCore.Repositories
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        private readonly DbSet<City> _city;
        private readonly DbSet<Country> _country;
        public CityRepository(ChallengeContext context) : base(context)
        {
            _city = context.Set<City>();
            _country = context.Set<Country>();
        }

        public async Task<List<CityViewModel>> GetAllCitiesAsync()
        {
            return await _city.Include(x => x.Country).Select(x => new CityViewModel
            {
                Id = x.Id,
                CityName = x.CityName,
                Country = x.Country.CountryName,
                AttractionFactor = x.AttractionFactor,
                Comment = x.Comment,
                ImageUrl = x.ImageUrl
            }).ToListAsync();
        }
        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            return new DashboardViewModel
            {
                CityCount = await _city.CountAsync(),
                CountryCount = await _country.CountAsync()
            };
        }
    }
}
