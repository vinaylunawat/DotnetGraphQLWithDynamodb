using Framework.Service.Utilities.Criteria;
using Geography.Business.Country;
using GraphQL;
using GraphQL.Types;
using System.Threading.Tasks;

namespace Geography.Business.State
{
    public class StateType : ObjectGraphType<Entity.Entities.State>
    {
        public StateType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name, type: typeof(StringGraphType));
            //Field<CountryType>("Country");
            //.ResolveAsync(async context => await ResolveStatesAsync(context));

        }

        //private async Task<object> ResolveStatesAsync(IResolveFieldContext<Entity.Entities.State> context)
        //{
        //    var countryId = context.Source.CountryId;
        //    FilterCriteria<Entity.Entities.State> filterCriteria = new FilterCriteria<Entity.Entities.State>();
        //    filterCriteria.Predicate = a => a.CountryId == countryId;
        //    filterCriteria.Includes.Add(a => a.Country);
        //    return await _countryRepository.FetchByIdAsync(countryId);
        //}
    }
}
