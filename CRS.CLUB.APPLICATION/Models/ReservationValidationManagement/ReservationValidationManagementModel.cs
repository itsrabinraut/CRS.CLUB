using System.Collections.Generic;

namespace CRS.CLUB.APPLICATION.Models.ReservationValidationManagement
{
    public class ReservationDetailViaOTPModel
    {
        public string OTPCode { get; set; }
        public string ReservationId { get; set; }
        public string InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string LocationVerificationStatus { get; set; }
        public string OTPVerificationStatus { get; set; }
        public string PaymentType { get; set; }
        public string ReservedDate { get; set; }
        public string VisitDateTime { get; set; }
        public string TransactionStatus { get; set; }
        public string LocationName { get; set; }
        public string StoreName { get; set; }
        public string PlanName { get; set; }
        public List<ReservationHostDetail> ReservationHostListModel { get; set; }
    }

    public class ReservationHostDetail
    {
        public string HostId { get; set; }
        public string HostName { get; set; }
        public string HostLogo { get; set; }
    }

    public class ManageReservationOTPStatusModel
    {
        public string ReservationId { get; set; }
        public string OTPCode { get; set; }
    }
}