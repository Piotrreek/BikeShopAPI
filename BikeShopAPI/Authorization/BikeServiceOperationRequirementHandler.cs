using System.Security.Claims;
using BikeShopAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BikeShopAPI.Authorization
{
    public class BikeServiceOperationRequirementHandler : AuthorizationHandler<OperationRequirement, Bike>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, Bike bike)
        {
            if (requirement.Operation == Operation.Read)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (bike.CreatedById == int.Parse(userId) ||
                context.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value == "Admin" ||
                bike.CreatedById == null)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
