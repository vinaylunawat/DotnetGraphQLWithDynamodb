using Geography.Business.Country.Models;
using GraphQL.Types;

namespace Geography.Business.Country
{
    public class CountryType : ObjectGraphType<CountryModel>
    {
        public CountryType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.IsoCode);
           // Field<ListGraphType<StateType>>("States");             
        }        
    }
}
