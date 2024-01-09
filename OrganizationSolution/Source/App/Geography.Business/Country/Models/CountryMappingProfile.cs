namespace Geography.Business.Country.Models
{
    using Amazon.DynamoDBv2.Model;
    using AutoMapper;
    using Entity.Entities;
    using Framework.DataAccess.Model;
    using Geography.Business.Country.Types;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="CountryMappingProfile" />.
    /// </summary>
    public class CountryMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryMappingProfile"/> class.
        /// </summary>
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryReadModel>()
            .ForMember(x => x.Continent, opt => opt.MapFrom(a => GetContinent(a.Continent)));

            CreateMap<CountryCreateModel, Country>()
                .ForMember(x => x.Continent, opt => opt.MapFrom(a => a.Continent.ToString()))
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CountryUpdateModel, Country>()
                .ForMember(x => x.Continent, opt => opt.MapFrom(a => a.Continent.ToString()))
                .ForMember(x => x.Continent, opt => opt.MapFrom(a => a.Continent.ToString()));

            CreateMap<PagedResultModel<Country>, PagedResultModel<CountryReadModel>>();

            CreateMap<Dictionary<string, AttributeValue>, Country>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src["Id"].S))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src["Name"].S))
                .ForMember(x => x.IsoCode, opt => opt.MapFrom(src => src["IsoCode"] != null ? src["IsoCode"].S : null))
                .ForMember(x => x.Continent, opt => opt.MapFrom(src => src["Continent"].S))
                .ForMember(x => x.UpdatedDate, opt => opt.MapFrom(src => GetUpdateDate(src["UpdatedDate"] != null ? src["UpdatedDate"].S : null)));
        }

        private DateTime GetUpdateDate(string strUpdatedDate)
        {
            
            if(DateTime.TryParse(strUpdatedDate, out var updatedDate))
            {
                return updatedDate;
            }
            else
            {
                throw new Exception("Invalid datetime format");
            }
        }

        private static Continent GetContinent(string strContinent)
        {
            if (strContinent.IsNullOrEmpty())
                return default(Continent);

            Continent continent;
            if (Enum.TryParse(strContinent, out continent))
            {
                return continent;
            }
            else
            {
                throw new Exception($"Wrong value for Continent: {strContinent}");
            }
        }
    }
}
