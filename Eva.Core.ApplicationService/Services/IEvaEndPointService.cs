using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IEvaEndPointService : IEvaBaseService<EvaEndPoint, EvaEndPointViewModel>
    {
        Task<EvaEndPoint> GetEndPointByName(string currentEndpointName);
    }
}
