namespace BoilerplateService.Models.Entities
{
    public class AddressDetail
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Postal { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }
}