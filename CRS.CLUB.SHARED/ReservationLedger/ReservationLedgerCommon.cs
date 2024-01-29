using CRS.CLUB.SHARED.PaginationManagement;

namespace CRS.CLUB.SHARED.ReservationLedger
{
    public class ReservationLedgerCommon : Common
    {
        public int SNO { get; set; }
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string NickName { get; set; }
        public string PlanName { get; set; }
        public string People { get; set; }
        public string NoOfHosts { get; set; }
        public string VisitedDate { get; set; }
        public string VisitedTime { get; set; }
        public string PaymentOption { get; set; }
        public string PlanAmount { get; set; }
        public string AdminPayment { get; set; }
        public string CustomerProfileImage { get; set; }
        public int TotalRecords { get; set; }
    }
    public class ReservationLedgerAnalyticDetailModelCommon : Common
    {
        public string TotalBooking { get; set; }
        public string ApprovedBooking { get; set; }
        public string PendingBooking { get; set; }
        public string TotalVisitor { get; set; }
        public string Premium { get; set; }
    }
    public class SearchFilterModel : PaginationFilterCommon
    {
        public string SearchFilter { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

}
