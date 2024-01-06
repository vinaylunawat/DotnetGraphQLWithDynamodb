namespace Geography.Business.State.Models
{
    /// <summary>
    /// Defines the <see cref="StateCreateModel" />.
    /// </summary>
    public class StateCreateModel
    {       
        public string Name { get; set; }
        public long CountryId { get; set; }
    }
}
