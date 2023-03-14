using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Application.Contracts.City
{
    public class CreateCity
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string CityName { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public IFormFile ImageUrl { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public byte AttractionFactor { get; set; }
        public string Comment { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public int CountryId { get; set; }

    }
}
