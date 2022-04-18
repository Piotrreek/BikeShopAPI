namespace BikeShopAPI.Exceptions
{
    public class ForbidException : BikeShopException
    {
        public ForbidException(string message) : base(message)
        {}
        public ForbidException()
        {}
    }
}
