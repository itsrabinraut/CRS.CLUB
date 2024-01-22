namespace CRS.CLUB.SHARED.ReservationValidationManagement
{
    public class ReservationDetailViaOTPCommon
    {
        public ResponseCode Code { get; set; }
        public string Message { get; set; }
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
    }

    public class ReservationHostDetailCommon
    {
        public string HostId { get; set; }
        public string HostName { get; set; }
        public string HostLogo { get; set; }
    }

    public class ManageReservationOTPStatusCommon : Common
    {
        public string ReservationId { get; set; }
        public string OTPCode { get; set; }
    }
}
