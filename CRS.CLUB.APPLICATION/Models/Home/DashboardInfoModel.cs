using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace CRS.CLUB.APPLICATION.Models.Home
{
    public class CommonDashboardModel
    {
        public DashboardInfoModel GetDashboardInfoList {  get; set; }
        public List<HostListModel> GetHostList {  get; set; }
        public List<ChartInfoModel> GetChartInfo {  get; set; }
    }
    public class DashboardInfoModel
    {
        public string TotalHost { get; set; }
        public string TotalBooking { get; set; }
        public string TotalVisitor { get; set; }
        public string TotalSales { get; set; }
    }
    public class HostListModel
    {
        public string HostName { get; set; }
        public string HostPosition { get; set; }
        public string BookingCount { get; set; }
        public string HostImage { get; set; }
    }
    public class ChartInfoModel
    {
        public string Month { get; set; }
        public string TotalSales { get; set; }
    }
}