using Challenge.Application.Contracts.Dashboard;
using Framework.Application;
using Framework.Domain;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Application.Contracts.City
{
    public interface ICityApplication
    {
        Task<OperationResult> CreateAsync(CreateCity command);
        Task<OperationResult> EditAsync(EditCity command);
        Task<EditCity> GetDetailsAsync(int id,HttpContext context);
        Task<List<CityViewModel>> GetAllAsync(HttpContext context);
        Task<OperationResult> DeleteAsync(int id);
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}
