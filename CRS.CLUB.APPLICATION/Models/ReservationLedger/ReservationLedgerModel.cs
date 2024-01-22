using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.CLUB.APPLICATION.Models.ReservationLedger
{
    public class CommonReservationLedgerModel
    {
        public string SearchFilter { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<ReservationLedgerModel> GetReservationLedgerList { get; set; }
        public ReservationLedgerAnalyticDetailModel GetReservationLedgerAnalyticData { get; set; }
    }
    public class ReservationLedgerModel
    {
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
    }

    public class ReservationLedgerAnalyticDetailModel
    {
        public string TotalBooking { get; set; }
        public string ApprovedBooking { get; set; }
        public string PendingBooking { get; set; }
        public string TotalVisitor { get; set; }
        public string Premium { get; set; }
    }
}
