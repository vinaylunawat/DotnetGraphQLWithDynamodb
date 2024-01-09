using Geography.Business.Country.Types;
using Geography.Business.State.Models;
using System;
using System.Collections.Generic;

namespace Geography.Business.Country.Models
{
    public class CountryUpdateModel
    {
        public CountryUpdateModel()
        {
            States = new List<StateUpdateModel>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public Continent Continent { get; set; }
        public List<StateUpdateModel> States {get;set;}
    }
}
