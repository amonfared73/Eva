namespace Eva.Core.Domain.BaseViewModels
{
    public class BaseRequestViewModel
    {
        public PaginationRequestViewModel? PaginationRequest { get; set; }
        public SortingRequestViewModel? SortingRequest { get; set; }
        public SearchTermViewModel? SearchTermRequest { get; set; }
    }
}
