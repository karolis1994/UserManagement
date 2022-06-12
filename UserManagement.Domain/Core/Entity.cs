namespace UserManagement.Domain.Core
{
    /// <summary>
    /// Base of an entity
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public virtual long Id { get; set; }
    }
}
