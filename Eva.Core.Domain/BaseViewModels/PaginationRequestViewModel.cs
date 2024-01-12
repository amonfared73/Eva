namespace Eva.Core.Domain.BaseViewModels
{
    public class PaginationRequestViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 5;
    }
}
