using AutoMapper;
using FluentValidation.Results;
using Framework.Configuration.Models;
using Geography.Business.Country.Models;
using Geography.Business.Country.Types;
using Geography.Business.Country.Validator;
using Geography.Business.GraphQL;
using Geography.Business.GraphQL.Model;
using Geography.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using GraphQL.Upload.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Geography.Business.Country.Manager
{
    public class CountryMutation : ITopLevelMutation
    {
        private readonly ICountryRepository _countryRepository;
        private readonly CountryCreateModelValidator _countryCreateValidator;
        private readonly CountryUpdateModelValidator _countryUpdateValidator;
        private readonly AmazonS3ConfigurationOptions _amazonS3Configuration;
        private readonly IMapper _mapper;
        public CountryMutation(ICountryRepository countryRepository, CountryCreateModelValidator countryCreateValidator,
                        CountryUpdateModelValidator countryUpdateValidator, ApplicationOptions options, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _countryCreateValidator = countryCreateValidator;
            _countryUpdateValidator = countryUpdateValidator;
            _amazonS3Configuration = options.amazons3ConfigurationOptions;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<CountryType>("CreateCountry")
                .Argument<NonNullGraphType<CountryCreateInputType>>("country", "object of country")
                .Argument<UploadGraphType>("file", "file to upload")
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
            var file = context.GetArgument<IFormFile>("file");
            var uploadedFiles = new List<string>();
            using (Stream fileContent = file.OpenReadStream())
            {
                var fileName = await _countryRepository.UploadFileAsync(_amazonS3Configuration, file.FileName, fileContent).ConfigureAwait(false);
                uploadedFiles.Add(fileName);
            }
            string attributeName = "Name";
            var isExist = await _countryRepository.GetDetailsByAttributeName(attributeName, country.Name).ConfigureAwait(false);
            if (isExist)
            {
                context.Errors.Add(new ExecutionError($"Country {attributeName} can not be duplicate."));
                return null;
            }

            var validationResult = _countryCreateValidator.Validate(country);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }
            
            var countryEntity = _mapper.Map<Entity.Entities.Country>(country);
            countryEntity.Id = Guid.NewGuid();

            if (country.States.Any())
            {
                if (uploadedFiles.Any())
                {
                    countryEntity.Files = uploadedFiles;
                }
                var isSuccess = await _countryRepository.SaveTransactionData(countryEntity);
                if (!isSuccess)
                {
                    context.Errors.Add(new ExecutionError("Transaction failed to save data."));
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
