namespace MarketUz.Domain.Exceptions
{
    public class LowStockException : Exception
    {
        public LowStockException() { }
        public LowStockException(string message) : base(message) { }
    }
}
