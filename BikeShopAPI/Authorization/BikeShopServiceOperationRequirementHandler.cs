using System.Security.Claims;
using BikeShopAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BikeShopAPI.Authorization
{
    public class BikeShopServiceOperationRequirementHandler : AuthorizationHandler<OperationRequirement, BikeShop>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, BikeShop shop)
        {
            if (requirement.Operation == Operation.Read)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (shop.CreatedById == int.Parse(userId) || 
                context.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value == "Admin" || 
                shop.CreatedById == null)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
