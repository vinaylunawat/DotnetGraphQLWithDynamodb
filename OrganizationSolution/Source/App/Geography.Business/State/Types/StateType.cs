using Geography.Business.State.Models;
using GraphQL.Types;
namespace Geography.Business.State.Types
{
    public class StateType : ObjectGraphType<StateReadModel>
    {
        public StateType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name, type: typeof(StringGraphType));
            Field(x => x.CountryId, type: typeof(IdGraphType));
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
