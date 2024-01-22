using CRS.CLUB.SHARED.ReservationLedger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.REPOSITORY.ReservationLedger
{
    public class ReservationLedgerRepository : IReservationLedgerRepository
    {
        private readonly RepositoryDao _dao;
        public ReservationLedgerRepository()
        {
            _dao = new RepositoryDao();
        }

        public ReservationLedgerAnalyticDetailModelCommon GetLedgerAnalyticDetail(string ClubId)
        {
            string sp_name = "EXEC sproc_club_reservationledgerdetaillist @Flag='grlad'";
            sp_name += ",@ClubId=" + _dao.FilterString(ClubId);
            var dbResponseInfo = _dao.ExecuteDataRow(sp_name);
            if (dbResponseInfo != null)
            {
                return new ReservationLedgerAnalyticDetailModelCommon()
                {
                    TotalBooking = _dao.ParseColumnValue(dbResponseInfo, "TotalBooking").ToString(),
                    ApprovedBooking = _dao.ParseColumnValue(dbResponseInfo, "ApprovedBooking").ToString(),
                    PendingBooking = _dao.ParseColumnValue(dbResponseInfo, "PendingBooking").ToString(),
                    TotalVisitor = _dao.ParseColumnValue(dbResponseInfo, "TotalVisitor").ToString(),
                    Premium = _dao.ParseColumnValue(dbResponseInfo, "Premium").ToString()
                };
            }
            return new ReservationLedgerAnalyticDetailModelCommon();
        }

        public List<ReservationLedgerCommon> GetReservationLedgerListDetail(string ClubId, SearchFilterModel request)
        {
            List<ReservationLedgerCommon> responseInfo = new List<ReservationLedgerCommon>();
            string sp_name = "EXEC sproc_club_reservationledgerdetaillist @Flag='rlld'";
            sp_name += ",@ClubId=" + _dao.FilterString(ClubId);
            sp_name += ",@SearchFilter=" + _dao.FilterString(request.SearchFilter);
            sp_name += ",@FromDate=" + _dao.FilterString(request.FromDate);
            sp_name += ",@ToDate=" + _dao.FilterString(request.ToDate);
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow dataRow in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new ReservationLedgerCommon
                    {
                        Id = dataRow["Id"].ToString(),
                        CustomerName = dataRow["CustomerName"].ToString(),
                        NickName = dataRow["NickName"].ToString(),
                        PlanName = dataRow["PlanName"].ToString(),
                        People = dataRow["People"].ToString(),
                        VisitedDate = dataRow["VisitedDate"].ToString(),
                        VisitedTime = dataRow["VisitedTime"].ToString(),
                        PaymentOption = dataRow["PaymentOption"].ToString(),
                        AdminPayment = dataRow["AdminPayment"].ToString(),
                        CustomerProfileImage = dataRow["CustomerProfileImage"].ToString(),
                        PlanAmount = dataRow["PlanAmount"].ToString(),
                    });
                }
            }
            return responseInfo;
        }
    }
}
