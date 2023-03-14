using Challenge.Application.Contracts.Country;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Challenge.Presentation.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CountryController : BaseController
    {
        private readonly ICountryApplication _countryApplication;
        public CountryController(ICountryApplication countryApplication)
        {
            _countryApplication = countryApplication;
        }
        // GET: api/<CountryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result= await _countryApplication.GetAllAsync();
            return Ok(result);
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _countryApplication.GetDetailsAsync(id);
            return Ok(result);
        }

        // POST api/<CountryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCountry model)
        {
            var result = await _countryApplication.CreateAsync(model);
            return Ok(result.IsSucceeded);
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditCountry model)
        {
            model.Id = id;
            var result = await _countryApplication.EditAsync(model);
            return Ok(result.IsSucceeded);
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _countryApplication.DeleteAsync(id);
            return Ok(result.IsSucceeded);
        }
    }
}
