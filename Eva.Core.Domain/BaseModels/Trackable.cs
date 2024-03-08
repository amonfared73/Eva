using Eva.Core.Domain.Attributes;

namespace Eva.Core.Domain.BaseModels
{
    public abstract class Trackable
    {
        [IgnoreUpdate]
        public int CreatedBy { get; set; }
        [IgnoreUpdate]
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
