using Challenge.Application.Contracts.Country;
using Challenge.Domain.CountryAgg;
using Framework.Application;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Test
{
	public class CountryServiceFake : ICountryApplication
    {
        private readonly List<CountryViewModel> _country;

        public CountryServiceFake()
        {
            _country = new List<CountryViewModel>()
            {
                new CountryViewModel() {Id=1, CountryName = "Iran" },
                new CountryViewModel() {Id=2, CountryName = "USA" },
            };
        }

        public async Task<List<CountryViewModel>> GetAllAsync()
        {
            return _country;
        }

        public async Task<OperationResult> CreateAsync(CreateCountry newItem)
        {
            var countryVM = new CountryViewModel() { Id=1,CountryName=newItem.countryName};
            _country.Add(countryVM);
            return SetOperationResult();
        }
        private OperationResult SetOperationResult()
        {
            return new OperationResult() { IsSucceeded = true, Message = "", ReturnValue = 0 };
        }

        public void Remove(int id)
        {
            var existing = _country.First(a => a.Id == id);
            _country.Remove(existing);
        }


        public Task<OperationResult> EditAsync(EditCountry command)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EditCountry> GetDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<SelectList>> GetListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
