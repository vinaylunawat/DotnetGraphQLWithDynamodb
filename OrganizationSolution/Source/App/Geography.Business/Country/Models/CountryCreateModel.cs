using Geography.Business.Country.Types;
using Geography.Business.State.Models;
using System;
using System.Collections.Generic;

namespace Geography.Business.Country.Models
{

    /// <summary>
    /// Defines the <see cref="CountryCreateModel" />.
    /// </summary>
    public class CountryCreateModel
    {
        public CountryCreateModel()
        {
            States = new List<StateCreateModel>();            
        }

        public string Name { get; set; }
        public string IsoCode { get; set; }

        public Continent Continent { get; set; }

        public IEnumerable<StateCreateModel> States { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
