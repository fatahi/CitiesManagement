using Challenge.Application.Contracts.City;
using Challenge.Application.Contracts.Dashboard;
using Framework.Application;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Domain.CityAgg
{
    public interface ICityRepository: IRepository<City>
    {
        Task<List<CityViewModel>> GetAllCitiesAsync();
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}
