namespace Eva.Core.Domain.BaseInterfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        DateTime DeletedOn { get; set; }
    }
}
