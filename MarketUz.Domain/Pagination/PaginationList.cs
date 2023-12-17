namespace MarketUz.Domain.Pagination
{
    public class PaginationList<T> : List<T>
    {
        public const int MaxPagesSize = 25;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > MaxPagesSize)
                {
                    PageSize = MaxPagesSize;
                }
                else
                {
                    PageSize = value;
                }
            }
        }
        private int _pageSize = 15;
        public int TotalCount { get; set; }

        public int CurrentPage { get; set; }
        public int PreviosPage { get; set; }
        public int NextPage { get; set; }
        public int TotalPage { get; set; }
    }
}
