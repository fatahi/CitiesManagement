using Framework.Application;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Challenge.Application.Contracts.Country;
using Challenge.Domain.CountryAgg;
using Challenge.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Infrastructure.EfCore;

namespace Challenge.Infrastructure.EFCore.Repositories
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        private readonly DbSet<Country> _country;
        public CountryRepository(ChallengeContext context) : base(context)
        {
            _country = context.Set<Country>();
        }

        public async Task<List<CountryViewModel>> GetAllCountriesAsync()
        {
            return await _country.Select(x => new CountryViewModel
            {
                Id = x.Id,
                CountryName=x.CountryName,
            }).ToListAsync();
        }
        public async Task<IList<SelectList>> GetSelectListAsync()
        {
            return await _country.Select(x => new SelectList
            {
                Id = x.Id,
                Name = x.CountryName
            }).ToListAsync();
        }
    }
}
