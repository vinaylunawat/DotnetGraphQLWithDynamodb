namespace Geography.Business.Country.Models
{
    using AutoMapper;
    using Entity.Entities;
    using Geography.Business.Country.Types;
    using Microsoft.IdentityModel.Tokens;
    using System;

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
