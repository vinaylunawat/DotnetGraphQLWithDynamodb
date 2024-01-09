using Geography.Business.Country.Types;
using Geography.Business.State.Models;
using System;
using System.Collections.Generic;

namespace Geography.Business.Country.Models
{
    public class CountryReadModel 
    {
        public CountryReadModel()
        {
            States = new List<StateReadModel>();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string IsoCode { get; set; }

        public Continent Continent { get; set; }
        public List<StateReadModel> States { get; set; }
    }  

    
}
