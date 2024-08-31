using DynamicODataRouting.Controllers;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.OData.Edm;

namespace DynamicODataRouting.Routing
{
    public class CommonControllerModelConvention : IControllerModelConvention
    {
        private readonly IEdmModel myEdmModel;

        public CommonControllerModelConvention(IEdmModel edmModel)
        {
            myEdmModel = edmModel;
        }

        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType || controller.ControllerType.GetGenericTypeDefinition() != typeof(CommonController<>))
            {
                return;
            }

            Type entityType = controller.ControllerType.GenericTypeArguments[0];

            IEdmEntitySet entitySet = myEdmModel.EntityContainer
                                                .EntitySets()
                                                .FirstOrDefault(e => string.Equals(e.Name, entityType.Name, StringComparison.OrdinalIgnoreCase));

            SelectorModel selector = new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel
                {
                    Template = $"api/{entitySet.Name}",
                },
            };

            if (entitySet == null)
            {
                throw new NotSupportedException($"Unable to find entity set in EDM model. Entity Set Name: {entitySet.Name}");
            }

            ODataPathTemplate template = new ODataPathTemplate(new EntitySetSegmentTemplate(entitySet));
            selector.EndpointMetadata.Add(new ODataRoutingMetadata($"api", myEdmModel, template));

            controller.Selectors.Add(selector);
        }
    }
}
