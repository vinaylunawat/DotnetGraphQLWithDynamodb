using GraphQL.Types;

namespace Geography.Business.State.Types
{
    public class StateCreateInputType : InputObjectGraphType
    {
        public StateCreateInputType()
        {
            Name = "stateCreateInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            //Field<IdGraphType>("countryId");
        }
    }

    public class StateUpdateInputType : InputObjectGraphType
    {
        public StateUpdateInputType()
        {
            Name = "stateUpdateInput";
            Field<IdGraphType>("Id");
            Field<StringGraphType>("name");
            Field<IdGraphType>("countryId");
        }
    }
}
