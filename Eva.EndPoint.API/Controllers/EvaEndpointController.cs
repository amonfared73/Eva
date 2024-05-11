using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.Tools.Reflections;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class EvaEndpointController : EvaControllerBase<EvaEndPoint, EvaEndPointViewModel>
    {
        private readonly IEvaEndPointService _evaEndPoint;
        public EvaEndpointController(IEvaEndPointService evaEndPoint) : base(evaEndPoint)
        {
            _evaEndPoint = evaEndPoint;
        }
        [HttpPost]
        public async Task<IActionResult> AddMissingEndpointsAsync()
        {
            var addedEndpoints = new List<string>();
            var types = Assemblies.GetEvaTypes(typeof(EvaControllerBase<,>)).Where(t => t != typeof(EvaControllerBase<,>) && t.IsDefined(typeof(ApiControllerAttribute)));
            foreach (var type in types)
            {
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    EvaEndPoint currentEndpoint = await _evaEndPoint.GetEndPointByName($"{type.Name}/{method.Name}");
                    if (currentEndpoint == null)
                    {
                        addedEndpoints.Add($"{type.Name}/{method.Name}");
                        await _evaEndPoint.InsertAsync(new EvaEndPoint()
                        {
                            Url = $"{type.Name}/{method.Name}"
                        });
                    }
                }
            }
            return Ok(addedEndpoints);
        }
    }
}
