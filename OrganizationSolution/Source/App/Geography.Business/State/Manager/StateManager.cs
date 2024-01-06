using AutoMapper;
using Framework.Business.Manager;
using Geography.Business.State.Models;
using Geography.DataAccess.Repository;
using Microsoft.Extensions.Logging;

namespace Geography.Business.State.Manager
{
    public class StateManager : Manager<Entity.Entities.State, StateModel>, IStateManager
    {
        /// <summary>
        /// Defines the _stateRepository.
        /// </summary>
        private readonly IStateRepository _stateRepository;

        public StateManager(IStateRepository stateRepository, ILogger<StateManager> logger, IMapper mapper)
            : base(stateRepository, logger, mapper)
        {
            _stateRepository = stateRepository;
        }
       
    }
}
