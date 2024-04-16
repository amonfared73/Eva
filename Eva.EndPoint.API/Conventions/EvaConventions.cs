using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Eva.EndPoint.API.Conventions
{
    public class EvaConventions
    {
        public IApplicationModelConvention ApplicationModelConvention { get; set; }
        public IControllerModelConvention ControllerModelConvention { get; set; }
        public IActionModelConvention ActionModelConvention { get; set; }
        public IParameterModelConvention ParameterModelConvention { get; set; }
    }
}
