using Challenge.Application.Contracts.City;
using Challenge.Application.Contracts.Country;
using Challenge.Application.Country;
using Challenge.Domain.CityAgg;
using Challenge.Domain.CountryAgg;
using Challenge.Domain.Mapper.AutoMapper;
using Challenge.Infrastructure.EfCore;
using Challenge.Infrastructure.EFCore.Repositories;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace Challenge.Infrastructure.Configuration
{
    public class ChallengeBootstrapper
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChallengeContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("ChallengeDbConnection")));
            services.AddScoped<ICountryApplication, CountryApplication>();
            services.AddScoped<ICountryRepository, CountryRepository>();

            services.AddScoped<ICityApplication, CityApplication>();
            services.AddScoped<ICityRepository, CityRepository>();

            services.AddAutoMapper(new Type[] { typeof(AutoMapping) });
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

        }
    }

}
