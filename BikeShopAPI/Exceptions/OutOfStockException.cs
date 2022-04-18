namespace BikeShopAPI.Exceptions
{
    public class OutOfStockException : BikeShopException
    {
        public OutOfStockException(string message) : base(message)
        {

        }
    }
}
