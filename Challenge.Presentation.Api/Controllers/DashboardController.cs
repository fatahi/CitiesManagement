using Challenge.Application.Contracts.City;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Challenge.Presentation.Api.Controllers
{

    public class DashboardController : BaseController
    {
        private readonly ICityApplication _cityApplication;
        public DashboardController(ICityApplication cityApplication)
        {
            _cityApplication = cityApplication;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _cityApplication.GetDashboardDataAsync();
            return Ok(result);
        }
    }
}
