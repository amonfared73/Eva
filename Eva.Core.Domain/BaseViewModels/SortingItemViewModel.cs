using Eva.Core.Domain.Enums;

namespace Eva.Core.Domain.BaseViewModels
{
    public class SortingItemViewModel
    {
        public string Field { get; set; } = "Id";
        public SortingType SortingType { get; set; } = SortingType.Ascending;
    }
}
