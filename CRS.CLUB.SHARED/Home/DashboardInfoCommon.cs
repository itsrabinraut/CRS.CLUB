namespace CRS.CLUB.SHARED.Home
{
    public class DashboardInfoCommon : Common
    {
        public string TotalHost { get; set; }
        public string TotalBooking { get; set; }
        public string TotalVisitor { get; set; }
        public string TotalSales { get; set; }
    }
    public class HostListModelCommon : Common
    {
        public string HostName { get; set; }
        public string HostPosition { get; set; }
        public string BookingCount { get; set; }
        public string HostImage { get; set; }
    }
    public class ChartInfoModelCommon
    {
        public string Month { get; set; }
        public string TotalSales { get; set; }
    }
}
