namespace FitnessGym.Domain.Filters
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int Offset { get; set; }
        public int PageSize { get; set; }

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 20;
            Offset = (PageNumber - 1) * PageSize;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 5 ? 5 : pageSize;
            Offset = (PageNumber - 1) * PageSize;
        }
    }
}
