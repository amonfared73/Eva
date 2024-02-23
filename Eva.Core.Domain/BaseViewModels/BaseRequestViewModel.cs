namespace Eva.Core.Domain.BaseViewModels
{
    public class BaseRequestViewModel
    {
        public PaginationRequestViewModel? PaginationRequest { get; set; }
        public SortingRequestViewModel? SortingRequest { get; set; }
        public SearchTermViewModel? SearchTermRequest { get; set; }
        public static BaseRequestViewModel SampleBaseRequestViewModel()
        {
            return new BaseRequestViewModel()
            {
                PaginationRequest = new PaginationRequestViewModel()
                {
                    PageNumber = 1,
                    RecordsPerPage = 5
                },
                SortingRequest = new SortingRequestViewModel()
                {
                    SortingItem = new SortingItemViewModel()
                    {
                        Field = "Id",
                        SortingType = Enums.SortingType.Ascending
                    }
                },
                SearchTermRequest = new SearchTermViewModel()
                {
                    SearchTerm = ""
                }
            };
        }
    }
}
