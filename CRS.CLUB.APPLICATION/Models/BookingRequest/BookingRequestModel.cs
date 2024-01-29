using System.Collections.Generic;

namespace CRS.CLUB.APPLICATION.Models.BookingRequest
{
    public class CommonBookingRequestList
    {
        public string SearchFilter { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Today { get; set; }
        public string Tomorrow { get; set; }
        public string DayAfterTomorrow { get; set; }
        public List<PendingBookingRequestList> GetPendingBookingRequestLists { get; set; }
        public List<ApprovedBookingRequestList> GetApprovedBookingRequestLists { get; set; }
        public List<AllBookingRequestList> GetAllBookingRequestLists { get; set; }
        public BookingRequestAnalyticsModel GetBookingRequestAnalyticData { get; set; }
        public List<ClubTimeInfoModel> GetClubTimeList { get; set; }
    }
    public class PendingBookingRequestList
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
        public string Action { get; set; }
        public string UpdatedDate { get; set; }
        public int TotalRecords { get; set; }
        public int SNO { get; set; }
    }
    public class ApprovedBookingRequestList
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
        public string Action { get; set; }
        public string UpdatedDate { get; set; }
        public int TotalRecords { get; set; }
        public int SNO { get; set; }
    }
    public class AllBookingRequestList
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
        public string Action { get; set; }
        public string UpdatedDate { get; set; }
        public int TotalRecords { get; set; }
        public int SNO { get; set; }
    }

    public class BookingRequestAnalyticsModel
    {
        public string TotalBooking { get; set; }
        public string TodayBooking { get; set; }
    }
    public class ClubTimeInfoModel
    {
        public string TimeRange { get; set; }
        public string BookingCount { get; set; }
    }
}