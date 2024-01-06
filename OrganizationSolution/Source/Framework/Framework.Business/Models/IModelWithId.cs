namespace Framework.Business.Models
{
    /// <summary>
    /// Defines the <see cref="IModelWithId" />.
    /// </summary>
    public interface IModelWithId : IAuditableModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        long Id { get; set; }
    }

    public interface IModelWithKey<Key> : IAuditableModel
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        Key Id { get; set; }
    }
}
