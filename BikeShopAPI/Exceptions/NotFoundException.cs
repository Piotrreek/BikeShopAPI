using BikeShopAPI.Exceptions;

namespace BikeShopAPI
{
    public class NotFoundException : BikeShopException
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
