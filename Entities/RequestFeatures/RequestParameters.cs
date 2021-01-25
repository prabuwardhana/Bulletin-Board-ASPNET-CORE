namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        // Maximum 50 rows per page
        const int maxPageSize = 50;
        // Set default page size
        private int _pageSize = 5;
        // Set default page number to 1 (first page)
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

        public string SearchTerm { get; set; }

        public string OrderBy { get; set; }
    }
}