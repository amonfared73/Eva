namespace Eva.Core.Domain.BaseModels
{
    public class RefreshToken : DomainObject
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
