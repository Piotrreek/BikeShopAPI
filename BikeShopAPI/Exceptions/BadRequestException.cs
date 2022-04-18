namespace BikeShopAPI.Exceptions
{
    public class BadRequestException : BikeShopException
    {
        public BadRequestException(string message) : base(message) { }
    }
}
