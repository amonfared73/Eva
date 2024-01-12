namespace Eva.Core.Domain.BaseModels
{
    public class Pagination
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public long? TotalRecords { get; set; }
    }
}
