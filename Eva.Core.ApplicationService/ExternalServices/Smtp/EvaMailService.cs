using Eva.Core.Domain.BaseModels;

namespace Eva.Core.ApplicationService.ExternalServices.Smtp
{
    public class EvaMailService : IEvaMailService
    {
        public async Task SendEmail(EmailItem emailItem)
        {
            await Task.Delay(500);
        }
    }
}
