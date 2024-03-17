namespace Eva.Core.Domain.BaseModels
{
    public class Pagination
    {
        public const int DefaultCurrentPage = 1;
        public const int DefaultRecordsPerPage = 5;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public long? TotalRecords { get; set; }
    }
}
