namespace Eva.Core.Domain.ViewModels
{
    public class PostCreationViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int BlogId { get; set; }
    }
}
