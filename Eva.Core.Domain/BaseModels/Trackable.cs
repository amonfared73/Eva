
namespace Eva.Core.Domain.BaseModels
{
    /// <summary>
    /// Tracking properties applied on all Eva entities
    /// </summary>
    public abstract class Trackable
    {
        /// <summary>
        /// The corresponding company in which the user has logged in the application
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// The corresponding user creating the current instance of entity
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// The creation time of the corresponding record
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// The corresponding user modifying the current instance of entity
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// The modification time of the corresponding record
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
