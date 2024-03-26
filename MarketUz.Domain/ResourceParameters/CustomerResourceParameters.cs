namespace MarketUz.Domain.ResourceParameters
{
    public  class CustomerResourceParameters
    {
        private const int MaxPageSize = 25;

        public string? SearchString { get; set; }
        public string OrderBy { get; set; } = "firstname";

        public int PageNumber { get; set; } = 1;
        private int _pageSize = 15;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
