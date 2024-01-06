using GraphQL.Types;

namespace Geography.Business.GraphQL
{
    public interface ITopLevelMutation
    {
        void RegisterField(ObjectGraphType graphType);
    }
}