namespace Eva.Core.Domain.BaseModels
{
    public class DomainObject
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
