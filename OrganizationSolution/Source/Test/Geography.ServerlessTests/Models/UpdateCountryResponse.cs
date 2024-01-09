
namespace Geography.ServerlessTests.Models
{
    public class UpdateCountryResponse
    {
        public UpdateCountry updateCountry { get; set; }
    }

    public class UpdateCountry
    {
        public string id { get; set; }
        public string name { get; set; }
        public string isoCode { get; set; }
        public string continent { get; set; }
    }
}
