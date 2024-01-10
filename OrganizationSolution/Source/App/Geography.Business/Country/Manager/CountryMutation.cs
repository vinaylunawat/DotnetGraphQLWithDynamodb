using AutoMapper;
using FluentValidation.Results;
using Geography.Business.Country.Models;
using Geography.Business.Country.Types;
using Geography.Business.Country.Validator;
using Geography.Business.GraphQL;
using Geography.Business.GraphQL.Model;
using Geography.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geography.Business.Country.Manager
{
    public class CountryMutation : ITopLevelMutation
    {
        private readonly ICountryRepository _countryRepository;
        private readonly CountryCreateModelValidator _countryCreateValidator;
        private readonly CountryUpdateModelValidator _countryUpdateValidator;
        private readonly IMapper _mapper;
        public CountryMutation(ICountryRepository countryRepository, CountryCreateModelValidator countryCreateValidator, CountryUpdateModelValidator countryUpdateValidator, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _countryCreateValidator = countryCreateValidator;
            _countryUpdateValidator = countryUpdateValidator;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<CountryType>("CreateCountry")
                .Argument<NonNullGraphType<CountryCreateInputType>>("country", "object of country")
                .ResolveAsync(async context => await ResolveCreateCountry(context).ConfigureAwait(false));

            graphType.Field<CountryType>("UpdateCountry")
                .Argument<NonNullGraphType<CountryUpdateInputType>>("country", "object of country")
                .ResolveAsync(async context => await ResolveUpdateCountry(context).ConfigureAwait(false));

            graphType.Field<StringGraphType>("deleteCountry")
            .Argument<NonNullGraphType<IdGraphType>>("countryId", "id of country")
            .ResolveAsync(async context => await ResolveDeleteCountry(context).ConfigureAwait(false));
        }

        private async Task<CountryReadModel> ResolveCreateCountry(IResolveFieldContext<object> context)
        {
            var country = context.GetArgument<CountryCreateModel>("country");
            string attibutename = "Name";
            var isExist = await _countryRepository.GetDetailsbyAttributeName(attibutename, country.Name).ConfigureAwait(false);
            if (isExist)
            {
                context.Errors.Add(new ExecutionError($"Country {attibutename} can not be duplicate."));
                return null;
            }

            var validationResult = _countryCreateValidator.Validate(country);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }
            
            var dbEntity = await _countryRepository.GetAll(default).ConfigureAwait(false);
            if (dbEntity.Any(x=> x.Name == country.Name))
            {
                context.Errors.Add(new ExecutionError("Country name can not be duplicate."));
                return null;
            }

            var countryEntity = _mapper.Map<Entity.Entities.Country>(country);
            countryEntity.Id = Guid.NewGuid();

            if (country.States.Any())
            {

                var isSuccess = await _countryRepository.SaveTransactionData(countryEntity);
                if (!isSuccess)
                {
                    context.Errors.Add(new ExecutionError("Transacation failed to save data."));
                }
                else
                {
                    return _mapper.Map<CountryReadModel>(countryEntity);
                }
            }

            var addedCountry = await _countryRepository.CreateAsync(countryEntity, default).ConfigureAwait(false);
            var result = _mapper.Map<CountryReadModel>(addedCountry);
            return result;

        }

        private async Task<CountryReadModel> ResolveUpdateCountry(IResolveFieldContext<object> context)
        {

            var countryUpdateModel = context.GetArgument<CountryUpdateModel>("country");

            var validationResult = _countryUpdateValidator.Validate(countryUpdateModel);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }

            var dbEntity = await _countryRepository.GetByKey(countryUpdateModel.Id, default).ConfigureAwait(false);
            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find country in db."));
                return null;
            }
            var dbCountry = _mapper.Map<Entity.Entities.Country>(countryUpdateModel);

            if (countryUpdateModel.States.Any())
            {
                var isSuccess = await _countryRepository.UpdateTransactionData(dbCountry).ConfigureAwait(false);
                if (!isSuccess)
                {
                    context.Errors.Add(new ExecutionError("Transacation failed to update data."));
                }
                else
                {
                    return _mapper.Map<CountryReadModel>(dbCountry);

                }
            }

            var updatedCountry = await _countryRepository.UpdateAsync(dbCountry, default).ConfigureAwait(false);
            var result = _mapper.Map<CountryReadModel>(updatedCountry);
            return result;

        }

        private async Task<object> ResolveDeleteCountry(IResolveFieldContext<object> context)
        {

            var countryId = context.GetArgument<Guid>("countryId");

            var dbEntity = await _countryRepository.GetByKey(countryId, default).ConfigureAwait(false);

            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find country in db."));
                return null;
            }
            //var dbStateIds = await _stateRepository.FetchByAsync(item => item.CountryId == countryId, data => data.Id);
            //await _countryRepository.RemoveCountryWithDependency(dbCountryId.First(), dbStateIds);

            await _countryRepository.DeleteAsync(countryId, default).ConfigureAwait(false);

            var res = new MutationResponse()
            {
                Message = $"The country with the id: {countryId} has been successfully deleted from db."
            };
            return res.Message;
        }

        private static void LoadErrors(IResolveFieldContext<object> context, ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                context.Errors.Add(new ExecutionError(error.ErrorMessage));
            }
        }


    }
}
