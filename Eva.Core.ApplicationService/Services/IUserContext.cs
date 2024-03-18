namespace Eva.Core.ApplicationService.Services
{
    public interface IUserContext
    {
        bool IsAuthenticated { get; }
        int UserId { get; }
    }
}
