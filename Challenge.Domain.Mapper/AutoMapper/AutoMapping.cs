using AutoMapper;
using Challenge.Application.Contracts.City;
using Challenge.Application.Contracts.Country;
using Challenge.Application.Contracts.User;
using Challenge.Domain.CityAgg;
using Challenge.Domain.CountryAgg;
using Challenge.Domain.UserAgg;
using Framework.Application;
using Microsoft.AspNetCore.Http;

namespace Challenge.Domain.Mapper.AutoMapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            //_context = context;
            CreateMap<Country, EditCountry>();
            CreateMap<EditCountry, Country>();
            CreateMap<CreateCountry, Country>();

            CreateMap<City, EditCity>()
                .ForMember(dest => dest.ImageUrl, src => src.Ignore())
                .ForMember(dest => dest.EditImage, opt => opt.MapFrom(src =>src.ImageUrl));
            CreateMap<EditCity, City>();
            CreateMap<CreateCity, City>();

            CreateMap<User, UserViewModel>();

        }
    }
}
