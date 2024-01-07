using Geography.Business.Country.Models;
using GraphQL.Types;
using System.ComponentModel;

namespace Geography.Business.Country.Types
{
    public class CountryType : ObjectGraphType<CountryReadModel>
    {
        public CountryType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.IsoCode, type: typeof(StringGraphType));
            Field(x => x.Continent, type: typeof(ContinentType));
            // Field<ListGraphType<StateType>>("States");             
        }
    }

    public class ContinentType : EnumerationGraphType<Continent>
    {
    }

    public enum Continent
    {
        Unknown = 0,
        Asia = 1,
        Africa = 2,
        Europe = 3
    }
}
