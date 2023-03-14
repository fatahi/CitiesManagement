using Framework.Application;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Application.Contracts.Country
{
    public interface ICountryApplication
    {
        Task<OperationResult> CreateAsync(CreateCountry command);
        Task<OperationResult> EditAsync(EditCountry command);
        Task<OperationResult> DeleteAsync(int id);
        Task<EditCountry> GetDetailsAsync(int id);
        Task<List<CountryViewModel>> GetAllAsync();
        Task<IList<SelectList>> GetListAsync();
    }
}
