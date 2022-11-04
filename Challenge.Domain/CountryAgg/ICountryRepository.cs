using Challenge.Application.Contracts.Country;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Domain.CountryAgg
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<List<CountryViewModel>> GetAllCountriesAsync();
        Task<IList<SelectList>> GetSelectListAsync();
    }
}
