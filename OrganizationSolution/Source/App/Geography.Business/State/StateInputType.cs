using GraphQL.Types;

namespace Geography.Business.State
{
    public class StateCreateInputType : InputObjectGraphType
    {
        public StateCreateInputType()
        {
            Name = "stateCreateInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<LongGraphType>("countryId");
        }
    }

    public class StateUpdateInputType : InputObjectGraphType
    {
        public StateUpdateInputType()
        {
            Name = "stateUpdateInput";
            Field<IdGraphType>("Id");
            Field<StringGraphType>("name");
            Field<LongGraphType>("countryId");
        }
    }
}
