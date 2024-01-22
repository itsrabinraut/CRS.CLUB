using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.SHARED.BookingRequest
{
    public class PendingBookingRequestListCommon : Common
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string PlanName { get; set; }
        public string People { get; set; }
        public string NoOfHosts { get; set; }
        public string VisitedDate { get; set; }
        public string VisitedTime { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string RowTotal { get; set; }
        public string RowNum { get; set; }
    }
    public class ApprovedBookingRequestListCommon : Common
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string PlanName { get; set; }
        public string People { get; set; }
        public string NoOfHosts { get; set; }
        public string VisitedDate { get; set; }
        public string VisitedTime { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string RowTotal { get; set; }
        public string RowNum { get; set; }
    }
    public class AllBookingRequestListCommon : Common
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string PlanName { get; set; }
        public string People { get; set; }
        public string NoOfHosts { get; set; }
        public string VisitedDate { get; set; }
        public string VisitedTime { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string RowTotal { get; set; }
        public string RowNum { get; set; }
    }
    public class BookingRequestAnalyticsModelCommon : Common
    {
        public string TotalBooking { get; set; }
        public string TodayBooking { get; set; }
    }
    public class ClubTimeInfoModelCommon
    {
        public string TimeRange { get; set; }
        public string BookingCount { get; set; }
    }

    public class SearchFilterCommon
    {
        public string SearchFilter { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Today { get; set; }
        public string Tomorrow { get; set; }
        public string DayAfterTomorrow { get; set; }
        public string Offset { get; set; } ="1";
        public string Limit { get; set; } = "10";

    }
}
