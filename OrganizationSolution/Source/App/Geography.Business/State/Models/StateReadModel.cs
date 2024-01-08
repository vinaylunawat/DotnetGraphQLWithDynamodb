using System;

namespace Geography.Business.State.Models
{
    /// <summary>
    /// Defines the <see cref="StateReadModel" />.
    /// </summary>
    public class StateReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
