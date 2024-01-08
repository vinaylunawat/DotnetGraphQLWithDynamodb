using Geography.Business.Country.Types;
using System;

namespace Geography.Business.Country.Models
{
    public class CountryUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public Continent Continent { get; set; }
    }
}
