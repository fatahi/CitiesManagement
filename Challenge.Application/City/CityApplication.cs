using AutoMapper;
using Challenge.Application.Contracts.Dashboard;
using Challenge.Domain.CityAgg;
using Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Application.Contracts.City
{
    public class CityApplication : ICityApplication
    {
        private readonly ICityRepository _cityRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IMapper _mapper;
        public CityApplication(ICityRepository cityRepository,
            IFileUploader fileUploader,IMapper mapper)
        {
            _cityRepository = cityRepository;
            _fileUploader = fileUploader;
            _mapper = mapper;
        }
        public async Task<OperationResult> CreateAsync(CreateCity command)
        {
            var operation = new OperationResult();
            if (await _cityRepository.ExistsAsync(x => x.CityName == command.CityName && x.CountryId==command.CountryId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var path = "/City";
            string pictureName = "";
            if (command.ImageUrl != null)
            {
                pictureName = _fileUploader.Upload(command.ImageUrl, path);
            }
            
            var model = _mapper.Map<CreateCity, Domain.CityAgg.City>(command);
            model.ImageUrl = pictureName;
            try
            {
                await _cityRepository.AddAsync(model,true);
                return operation.Succeeded();
            }
            catch (Exception ex)
            {
                return operation.Failed("خطا در ثبت");
            }
            
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            var operation = new OperationResult();
            try
            {
                await _cityRepository.DeleteAsync(id, true);
                operation.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                operation.IsSucceeded = false;
            }
            return operation;
        }
        public async Task<OperationResult> EditAsync(EditCity command)
        {
            var operation = new OperationResult();
            var city = await _cityRepository.GetByIdAsync(command.Id);

            if (city == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (await _cityRepository.ExistsAsync(x => x.CityName == command.CityName && x.CountryId!=command.CountryId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var path = $"City/";
            var pictureName = "";

            if (command.ImageUrl != null)
            {
                pictureName = _fileUploader.Upload(command.ImageUrl, path);
                city.ImageUrl = pictureName;
            }
            else
            {
                city.ImageUrl = command.EditImage;
            }
            await _cityRepository.UpdateAsync(city, true);

            return operation.Succeeded();
        }

        public async Task<List<CityViewModel>> GetAllAsync(HttpContext context)
        {
            var result=await _cityRepository.GetAllCitiesAsync();
            foreach (var item in result)
            {
                item.ImageUrl = Tools.GetFileUrl(context, item.ImageUrl);
            }
            return result;
        }

        public async Task<EditCity> GetDetailsAsync(int id,HttpContext context)
        {
            var city= await _cityRepository.GetByIdAsync(id);
            var mapped = _mapper.Map<Domain.CityAgg.City, EditCity>(city);
            mapped.EditImage = Tools.GetFileUrl(context, city.ImageUrl);
            return mapped;
        }
        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            return await _cityRepository.GetDashboardDataAsync();
        }

    }
    
}
