namespace Eva.Core.Domain.BaseInterfaces
{
    /// <summary>
    /// Interface to implement the soft delete mechanism
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Represents if a row is deleted or not
        /// </summary>
        bool IsDeleted { get; set; }
        /// <summary>
        /// Represents the percise time the entity is deleted
        /// </summary>
        DateTime? DeletedOn { get; set; }
    }
}
