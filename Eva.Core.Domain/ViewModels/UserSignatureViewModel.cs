namespace Eva.Core.Domain.ViewModels
{
    public class UserSignatureViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SignatureBase { get; set; }
    }
}
