using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.PaymentManagement;
using System.Collections.Generic;

namespace CRS.CLUB.REPOSITORY.PaymentManagement
{
    public interface IPaymentManagementRepository
    {
        PaymentOverviewCommon GetPaymentOverview(string clubId);
        List<PaymentLogCommon> GetPaymentLog(string searchText, string clubId, string filterDate);
        List<PaymentLedgerCommon> GetPaymentLedgerDetail(string searchText, string cId, string date);
        CommonDbResponse UpdatePaymentLogs(string remarks, Common request,string ReservationId);
    }
}
