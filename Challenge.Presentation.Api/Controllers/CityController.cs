using Challenge.Application.Contracts.City;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Challenge.Presentation.Api.Controllers
{
  
    public class CityController : BaseController
    {
        private readonly ICityApplication _cityApplication;
        public CityController(ICityApplication cityApplication)
        {
            _cityApplication = cityApplication;
        }
        // GET: api/<CityController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _cityApplication.GetAllAsync(HttpContext);
            return Ok(result);
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _cityApplication.GetDetailsAsync(id,HttpContext);
            return Ok(result);
        }
        
        // POST api/<CityController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateCity model)
        {
            var result = await _cityApplication.CreateAsync(model);
            return Ok(result.IsSucceeded);
        }
        
        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditCity model)
        {
            model.Id = id;
            var result = await _cityApplication.EditAsync(model);
            return Ok(result.IsSucceeded);
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cityApplication.DeleteAsync(id);
            return Ok(result.IsSucceeded);
        }
    }
    
}
