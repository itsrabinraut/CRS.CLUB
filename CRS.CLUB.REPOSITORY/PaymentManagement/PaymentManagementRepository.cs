using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.PaginationManagement;
using CRS.CLUB.SHARED.PaymentManagement;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRS.CLUB.REPOSITORY.PaymentManagement
{
    public class PaymentManagementRepository : IPaymentManagementRepository
    {
        private readonly RepositoryDao _dao;
        public PaymentManagementRepository() => _dao = new RepositoryDao();

        public List<PaymentLedgerCommon> GetPaymentLedgerDetail(string searchText, string cId, string date, PaginationFilterCommon request)
        {
            var paymentLogs = new List<PaymentLedgerCommon>();

            string sql = "sproc_club_payment_management @Flag='gpledd'";
            sql += " ,@ClubId =" + _dao.FilterString(cId);

            if (!string.IsNullOrWhiteSpace(searchText))
                sql += " ,@SearchField =N" + _dao.FilterString(searchText);
            if (!string.IsNullOrEmpty(date))
                sql += " ,@Date=" + _dao.FilterString(date);
            sql += ",@Skip=" + request.Skip;
            sql += ",@Take=" + request.Take;
            var dbResp = _dao.ExecuteDataTable(sql);
            if (dbResp != null && dbResp.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dbResp.Rows)
                {
                    if (dataRow["Code"].ToString() == "0")
                    {
                        paymentLogs.Add(new PaymentLedgerCommon()
                        {
                            CustomerName = dataRow["CustomerName"].ToString(),
                            CustomerNickName = dataRow["CustomerNickName"].ToString(),
                            CustomerImage = dataRow["CustomerImage"].ToString(),
                            PlanName = dataRow["PlanName"].ToString(),
                            NoOfPeople = dataRow["NoOfPeople"].ToString(),
                            VisitDate = dataRow["VisitDate"].ToString(),
                            VisitTime = dataRow["VisitTime"].ToString(),
                            PaymentType = dataRow["PaymentType"].ToString(),
                            PlanAmount = dataRow["PlanAmount"].ToString(),
                            TotalAmount = dataRow["TotalAmount"].ToString(),
                            CommissionAmount = dataRow["CommissionAmount"].ToString(),
                            TotalCommissionAmount = dataRow["TotalCommissionAmount"].ToString(),
                            AdminPaymentAmount = dataRow["AdminPaymentAmount"].ToString(),
                            ReservationType = dataRow["ReservationType"].ToString(),
                            TotalRecords = Convert.ToInt32(dataRow["TotalRecords"].ToString()),
                            SNO = Convert.ToInt32(dataRow["SNO"].ToString())
                        });
                    }
                }
            }
            return paymentLogs;
        }

        public List<PaymentLogCommon> GetPaymentLog(string searchText, string clubId, string filterDate, PaginationFilterCommon request)
        {
            var paymentLogs = new List<PaymentLogCommon>();

            string sql = "sproc_club_payment_management @flag='gpld'";
            sql += " ,@ClubId=" + _dao.FilterString(clubId);
            if (filterDate != null)
                sql += " ,@Date=" + _dao.FilterString(filterDate);
            if (searchText != null)
                sql += " ,@SearchField=N" + _dao.FilterString(searchText);
            sql += ",@Skip=" + request.Skip;
            sql += ",@Take=" + request.Take;
            var dbResp = _dao.ExecuteDataTable(sql);
            if (dbResp != null && dbResp.Rows.Count > 0)
            {
                foreach (DataRow dr in dbResp.Rows)
                {
                    if (dr["Code"].ToString() == "0")
                    {
                        paymentLogs.Add(new PaymentLogCommon
                        {
                            ClubId = dr["ClubId"].ToString(),
                            ClubAmount = dr["ClubAmount"].ToString(),
                            GrandTotal = dr["GrandTotal"].ToString(),
                            PaymentStatus = dr["PaymentStatus"].ToString(),
                            Remarks = dr["Remarks"].ToString(),
                            TotalCommissionAmount = dr["TotalCommissionAmount"].ToString(),
                            TransactionDate = dr["TransactionDate"].ToString(),
                            ReservationId = dr["ReservationId"].ToString(),
                            TotalRecords = Convert.ToInt32(dr["TotalRecords"].ToString()),
                            SNO = Convert.ToInt32(dr["SNO"].ToString())
                        });
                    }
                }
            }
            return paymentLogs;
        }

        public PaymentOverviewCommon GetPaymentOverview(string clubId)
        {
            var paymentOverview = new PaymentOverviewCommon();
            string sql = "sproc_club_payment_management @Flag='gpmo'";
            sql += " ,@ClubId=" + _dao.FilterString(clubId);
            var dbResp = _dao.ExecuteDataTable(sql);
            if (dbResp != null && dbResp.Rows.Count > 0)
            {
                paymentOverview.ApprovedBookings = dbResp.Rows[0]["ApprovedBookings"].ToString();
                paymentOverview.PendingBookings = dbResp.Rows[0]["PendingBookings"].ToString();
                paymentOverview.TotalBookings = dbResp.Rows[0]["TotalBookings"].ToString();
                paymentOverview.PendingPayments = dbResp.Rows[0]["PendingPayments"].ToString();
            }
            return paymentOverview;
        }

        public CommonDbResponse UpdatePaymentLogs(string remarks, Common request, string ReservationId)
        {
            string sp_name = "sproc_club_payment_management @Flag='upl'";
            sp_name += ",@Remarks=" + _dao.FilterString(remarks);
            sp_name += ",@ClubId=" + _dao.FilterString(request.AgentId);
            sp_name += ",@ReservationId=" + _dao.FilterString(ReservationId);
            sp_name += ",@ActionUser=" + _dao.FilterString(request.ActionUser);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(request.ActionPlatform);
            sp_name += ",@ActionIP=" + _dao.FilterString(request.ActionIP);
            return _dao.ParseCommonDbResponse(sp_name);
        }
    }
}