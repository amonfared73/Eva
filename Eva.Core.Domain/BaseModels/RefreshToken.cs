namespace Eva.Core.Domain.BaseModels
{
    public class RefreshToken : ModelBase
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
