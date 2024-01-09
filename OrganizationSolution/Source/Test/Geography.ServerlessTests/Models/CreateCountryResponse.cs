namespace Geography.ServerlessTests.Models
{
    public class CreateCountryResponse
    {
        public CreateCountry createCountry { get; set; }
    }
    public class CreateCountry
    {
        public string id { get; set; }
        public string name { get; set; }
        public string isoCode { get; set; }
        public string continent { get; set; }
        
    }
}

