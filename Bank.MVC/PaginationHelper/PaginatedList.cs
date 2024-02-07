namespace Bank.MVC.PaginationHelper
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> datas, int page, int count, int pageSize)
        {
            this.AddRange(datas);
            ActivePage = page;
            TotalPageCount = (int)Math.Ceiling(count / (double)pageSize);

        }
        public int ActivePage { get; set; }
        public int TotalPageCount { get; set; }

        public bool HasPrev
        {
            get
            {
                return ActivePage > 1;
            }
        }

        public bool HasNext
        {
            get
            {
                return ActivePage < TotalPageCount;

            }
        }
    }
}