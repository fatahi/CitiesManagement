using AutoMapper;
using Challenge.Application.Contracts.Country;
using Challenge.Domain.CountryAgg;
using Framework.Application;
using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Country
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        
        public CountryApplication(ICountryRepository countryRepository,
            IFileUploader fileUploader,IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult> CreateAsync(CreateCountry command)
        {
            var operation = new OperationResult();
            if (await _countryRepository.ExistsAsync(x => x.CountryName == command.countryName))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            
            var model = _mapper.Map<CreateCountry, Domain.CountryAgg.Country>(command);
            try
            {
                await _countryRepository.AddAsync(model,true);
                return operation.Succeeded();
            }
            catch (Exception ex)
            {
                return operation.Failed("Register Failed");
            }
            
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            var operation = new OperationResult();
            try
            {
                await _countryRepository.DeleteAsync(id, true);
                operation.IsSucceeded=true;
            }
            catch (Exception ex)
            {
                operation.IsSucceeded = false;
            }
            return operation;
        }

        public async Task<OperationResult> EditAsync(EditCountry command)
        {
            var operation = new OperationResult();
            var country = await _countryRepository.FindAsync(x=>x.Id==command.Id);

            if (country == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (await _countryRepository.ExistsAsync(x => x.CountryName == command.countryName && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var model = _mapper.Map<EditCountry, Domain.CountryAgg.Country>(command);
            await _countryRepository.UpdateAsync(model, true);

            return operation.Succeeded();
        }

        public async Task<List<CountryViewModel>> GetAllAsync()
        {
            return await _countryRepository.GetAllCountriesAsync();
        }

        public async Task<EditCountry> GetDetailsAsync(int id)
        {
            var country= await _countryRepository.GetByIdAsync(id);
            var mapped = _mapper.Map<Domain.CountryAgg.Country, EditCountry>(country);
            return mapped;
        }

        public async Task<IList<SelectList>> GetListAsync()
        {
            return await _countryRepository.GetSelectListAsync();
        }

    }
}
