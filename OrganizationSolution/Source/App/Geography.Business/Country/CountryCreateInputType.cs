using Geography.Business.State;
using GraphQL.Types;

namespace Geography.Business.Country
{
    public class CountryCreateInputType : InputObjectGraphType
    {
        public CountryCreateInputType()
        {
            Name = "CountryCreateInput";            
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("isoCode");
            //Field<ListGraphType<StateCreateInputType>>("states");
        }
    }

    public class CountryUpdateInputType : InputObjectGraphType
    {
        public CountryUpdateInputType()
        {
            Name = "CountryUpdateInput";
            Field<NonNullGraphType<IdGraphType>>("Id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("isoCode");
            Field<ListGraphType<StateUpdateInputType>>("states");
        }
    }
}
