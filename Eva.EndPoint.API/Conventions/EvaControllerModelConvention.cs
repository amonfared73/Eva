using Eva.Core.Domain.Attributes;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Eva.EndPoint.API.Conventions
{
    public class EvaControllerModelConvention : IControllerModelConvention
    {
        private List<string> _baseOperations;
        public void Apply(ControllerModel controller)
        {
            _baseOperations = new List<string>()
            {
                "GetAll",
                "GetById",
                "Insert",
                "Update",
                "Delete",
                "ToByte"
            };
            bool disableCrud = controller.Attributes.Any(a => a.GetType() == typeof(DisableBaseOperationsAttribute));
            if (disableCrud)
            {
                var crudActions = controller.Actions.Where(a => _baseOperations.Contains(a.ActionName));
                foreach (var action in crudActions)
                {
                    action.ApiExplorer.IsVisible = false;
                }
            }
        }
    }
}
