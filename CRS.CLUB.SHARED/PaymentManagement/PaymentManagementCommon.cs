namespace CRS.CLUB.SHARED.PaymentManagement
{
    public class PaymentOverviewCommon
    {
        public string ApprovedBookings { get; set; }
        public string PendingBookings { get; set; }
        public string TotalBookings { get; set; }
        public string PendingPayments { get; set; }
    }
    public class PaymentLogCommon
    {
        public string ClubId { get; set; }
        public string ReservationId { get; set; }
        public string ClubAmount { get; set; }
        public string TotalCommissionAmount { get; set; }
        public string GrandTotal { get; set; }
        public string TransactionDate { get; set; }
        public string PaymentStatus { get; set; }
        public string Remarks { get; set; }
    }
    public class PaymentLedgerCommon
    {
        public string CustomerName { get; set; }
        public string CustomerNickName { get; set; }
        public string CustomerImage { get; set; }
        public string PlanName { get; set; }
        public string NoOfPeople { get; set; }
        public string VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string PaymentType { get; set; }
        public string PlanAmount { get; set; }
        public string TotalAmount { get; set; }
        public string CommissionAmount { get; set; }
        public string TotalCommissionAmount { get; set; }
        public string AdminPaymentAmount { get; set; }
        public string ReservationType { get; set; }
    }
}