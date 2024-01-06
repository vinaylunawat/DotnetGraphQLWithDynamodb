using System;

namespace Geography.Business.Country.Models
{

    /// <summary>
    /// Defines the <see cref="CountryCreateModel" />.
    /// </summary>
    public class CountryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
    }
}
