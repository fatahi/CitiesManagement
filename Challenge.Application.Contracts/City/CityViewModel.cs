namespace Challenge.Application.Contracts.City
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string ImageUrl { get; set; }
        public string Comment { get; set; }
        public string Country { get; set; }
        public byte AttractionFactor { get; set; }
    }
}
