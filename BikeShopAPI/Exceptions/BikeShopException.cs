namespace BikeShopAPI.Exceptions
{
    public abstract class BikeShopException : Exception
    {
        protected BikeShopException(string message) : base(message) {}
        protected BikeShopException(){}
    }
}
