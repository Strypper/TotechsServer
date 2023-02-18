using Intranet.Authorization.Requirements;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;

namespace Intranet.Authorization.Handlers
{
    public class IntranetPermissionHanlder : AuthorizationHandler<IntranetPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IntranetPermissionRequirement requirement)
        {
            var resource = context.Resource;
            if (resource is RouteEndpoint endpoint)
            {
                var metadata = endpoint.Metadata;
                if (metadata != null)
                {
                    var controllerAD = (ControllerActionDescriptor)metadata.FirstOrDefault(controller => controller is ControllerActionDescriptor)!;

                    var controller = controllerAD!.ControllerTypeInfo;
                    var action = controllerAD.MethodInfo;
                }

            }
            return Task.CompletedTask;
        }
    }
}
