using Geography.DataAccess;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic; 

namespace Geography.Business.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider)
                : base(provider)
        {
            Query = provider.GetRequiredService<GraphQLQuery>();
            Mutation = provider.GetRequiredService<GraphQLMutation>();
        }        
    }
}
