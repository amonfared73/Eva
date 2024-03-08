using Eva.Core.Domain.Attributes;

namespace Eva.Core.Domain.BaseModels
{
    public abstract class Trackable
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
