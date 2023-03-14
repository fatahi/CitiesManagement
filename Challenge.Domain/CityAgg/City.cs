using Challenge.Domain.CountryAgg;
using Framework.Domain;

namespace Challenge.Domain.CityAgg
{
    public class City:EntityBase<int>
    {
        public string CityName { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public byte AttractionFactor { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
