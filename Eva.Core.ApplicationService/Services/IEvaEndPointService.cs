﻿using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IEvaEndPointService : IBaseService<EvaEndPoint, EvaEndPointViewModel>
    {
        Task<EvaEndPoint> GetEndPointByName(string currentEndpointName);
    }
}
