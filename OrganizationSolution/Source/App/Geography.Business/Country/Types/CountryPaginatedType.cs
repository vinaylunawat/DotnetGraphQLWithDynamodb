using Framework.DataAccess.Model;
using Geography.Business.Country.Models;
using GraphQL.Types;

namespace Geography.Business.Country.Types
{
    public class CountryPaginatedType : ObjectGraphType<PagedResultModel<CountryReadModel>>
    {
        public CountryPaginatedType()
        {
            Field(x => x.Items, type: typeof(ListGraphType<CountryType>));
            Field(x => x.HasMorePages, type: typeof(BooleanGraphType));
            Field(x => x.LastEvaluatedKey, type: typeof(StringGraphType));
        }
    }    
}
