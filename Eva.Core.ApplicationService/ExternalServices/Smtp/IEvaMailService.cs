using Eva.Core.Domain.BaseModels;

namespace Eva.Core.ApplicationService.ExternalServices.Smtp
{
    public interface IEvaMailService
    {
        Task SendEmail(EmailItem emailItem);
    }
}
