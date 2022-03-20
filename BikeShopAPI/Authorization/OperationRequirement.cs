using Microsoft.AspNetCore.Authorization;

namespace BikeShopAPI.Authorization
{
    public enum Operation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class OperationRequirement : IAuthorizationRequirement
    {
        public Operation Operation { get; }
        public OperationRequirement(Operation operation)
        {
            Operation = operation;
        }
    }
    
}
