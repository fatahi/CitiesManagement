using Challenge.Domain.CityAgg;
using Framework.Domain;
using System.Collections.Generic;

namespace Challenge.Domain.CountryAgg
{
    public class Country:EntityBase<int>
    {
        public string CountryName { get; set; }
        public List<City> Cities { get; set; }
    }
}
