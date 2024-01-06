using System;

namespace Geography.Business.Country.Models
{

    /// <summary>
    /// Defines the <see cref="CountryCreateModel" />.
    /// </summary>
    public class CountryCreateModel
    {
        public CountryCreateModel()
        {
            Id = Guid.NewGuid();
            //States = new List<StateCreateModel>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }

        //public IEnumerable<StateCreateModel> States { get; set; }
    }
}
