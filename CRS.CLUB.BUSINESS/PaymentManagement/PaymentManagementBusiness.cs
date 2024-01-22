using CRS.CLUB.REPOSITORY.PaymentManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.PaymentManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.PaymentManagement
{
    public class PaymentManagementBusiness : IPaymentManagementBusiness
    {
        private readonly IPaymentManagementRepository _repo;
        public PaymentManagementBusiness(PaymentManagementRepository repo) => _repo = repo;

        public List<PaymentLedgerCommon> GetPaymentLedgerDetail(string searchText, string cId, string date)
        {
            return _repo.GetPaymentLedgerDetail(searchText, cId, date);
        }

        public List<PaymentLogCommon> GetPaymentLog(string searchText, string clubId, string filterDate)
        {
            return _repo.GetPaymentLog(searchText, clubId, filterDate);
        }

        public PaymentOverviewCommon GetPaymentOverview(string clubId)
        {
            return _repo.GetPaymentOverview(clubId);
        }

        public CommonDbResponse UpdatePaymentLogs(string remarks, Common request, string ReservationId)
        {
            return _repo.UpdatePaymentLogs(remarks, request, ReservationId);
        }
    }
}