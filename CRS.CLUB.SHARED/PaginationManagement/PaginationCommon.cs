namespace CRS.CLUB.SHARED.PaginationManagement
{
    public class PaginationFilterCommon
    {
        public string SearchFilter { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class PaginationResponseCommon
    {
        public int TotalRecords { get; set; }
    }
}
