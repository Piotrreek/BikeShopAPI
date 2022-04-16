using System.Security.Claims;
using BikeShopAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BikeShopAPI.Authorization
{
    public class BasketServiceOperationRequirementHandler : AuthorizationHandler<OperationRequirement, Basket>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, Basket resource)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (resource.ShopCreatorId == int.Parse(userId) ||
                context.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value == "Admin" ||
                resource.ShopCreatorId == null)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
