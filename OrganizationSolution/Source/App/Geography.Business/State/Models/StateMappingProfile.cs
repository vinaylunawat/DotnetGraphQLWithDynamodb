﻿namespace Geography.Business.State.Models
{
    using AutoMapper;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="StateMappingProfile" />.
    /// </summary>
    public class StateMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateMappingProfile"/> class.
        /// </summary>
        public StateMappingProfile()
        {
            CreateMap<State, StateReadModel>();

            CreateMap<StateCreateModel, State>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<StateUpdateModel, State>();
        }
    }
}
