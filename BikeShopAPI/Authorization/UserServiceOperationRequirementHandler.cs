using System.Security.Claims;
using BikeShopAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BikeShopAPI.Authorization
{
    public class UserServiceOperationRequirementHandler : AuthorizationHandler<OperationRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, User user)
        {
            var value = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (value != null && (int.Parse(value) == user.Id ||
                                  context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value == "Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
