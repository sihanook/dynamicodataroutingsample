using System.Reflection;

using DynamicODataRouting.Controllers;
using DynamicODataRouting.Models;

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DynamicODataRouting.Routing
{
    public class CommonControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            Type interfaceType = typeof(ICommonEntity);

            IEnumerable<Type> implementingTypes = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (Type entityType in implementingTypes)
            {
                var controllerType = typeof(CommonController<>)
                    .MakeGenericType(entityType)
                    .GetTypeInfo();

                feature.Controllers.Add(controllerType);
            }
        }
    }
}
