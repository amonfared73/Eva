namespace Eva.Core.Domain.BaseModels
{
    /// <summary>
    /// The base entity model for all Eva models
    /// </summary>
    public class ModelBase : Trackable
    {
        /// <summary>
        /// The unique integer key for all Eva entities
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Entity sate code for entity instances, default = 1
        /// </summary>
        public int StateCode { get; set; } = 1;
        /// <summary>
        /// IsDeleted property for soft delete implementation
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
