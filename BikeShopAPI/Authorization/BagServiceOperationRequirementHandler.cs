using System.Security.Claims;
using BikeShopAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BikeShopAPI.Authorization
{
    public class BagServiceOperationRequirementHandler : AuthorizationHandler<OperationRequirement, Bag>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, Bag resource)
        {
            if (requirement.Operation == Operation.Read)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            if (resource.CreatedById == int.Parse(userId) ||
                context.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value == "Admin" ||
                resource.CreatedById == null)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
