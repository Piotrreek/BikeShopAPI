using System.Security.Claims;
using BikeShopAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BikeShopAPI.Authorization
{
    public class ProductServiceOperationRequirementHandler : AuthorizationHandler<OperationRequirement, Product>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, Product resource)
        {
            if (requirement.Operation == Operation.Read)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            if (int.Parse(userId) == resource.CreatedById ||
                context.User?.FindFirst(u => u.Type == ClaimTypes.Role)?.Value == "Admin" ||
                resource.CreatedById == null)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
