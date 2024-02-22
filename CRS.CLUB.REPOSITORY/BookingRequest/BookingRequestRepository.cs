using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.BookingRequest;
using CRS.CLUB.SHARED.PaginationManagement;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRS.CLUB.REPOSITORY.BookingRequest
{
    public class BookingRequestRepository : IBookingRequestRepository
    {

        private readonly RepositoryDao _dao;
        public BookingRequestRepository()
        {
            _dao = new RepositoryDao();
        }

        public CommonDbResponse ApproveBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform)
        {
            string sp_name = "EXEC sproc_club_approveandrejectbookingrequest @Flag='abr'";
            sp_name += ",@Id=" + _dao.FilterString(newId);
            sp_name += ",@ActionUser=" + _dao.FilterString(actionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(actionIp);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(actionPlatform);

            var dbresponse = _dao.ParseCommonDbResponse(sp_name);
            return dbresponse;
        }

        public List<AllBookingRequestListCommon> GetAllBookingRequestList(string AgentId, SearchFilterCommon request, PaginationFilterCommon AllRequest)
        {
            List<AllBookingRequestListCommon> responseinfo = new List<AllBookingRequestListCommon>();
            string sp_name = "EXEC sproc_club_getbookingrequestlist @Flag='gabrl'";
            sp_name += ",@SearchFilter=" + _dao.FilterString(request.SearchFilter);
            sp_name += ",@AgentId=" + _dao.FilterString(AgentId);
            sp_name += ",@ToDate=" + _dao.FilterString(request.ToDate);
            sp_name += ",@FromDate=" + _dao.FilterString(request.FromDate);
            sp_name += ",@Today=" + _dao.FilterString(request.Today);
            sp_name += ",@Tomorrow=" + _dao.FilterString(request.Tomorrow);
            sp_name += ",@DayAfterTomorrow=" + _dao.FilterString(request.DayAfterTomorrow);
            sp_name += ",@Skip=" + AllRequest.Skip;
            sp_name += ",@Take=" + AllRequest.Take;
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseinfo.Add(new AllBookingRequestListCommon
                    {
                        Id = row["Id"].ToString(),
                        CustomerName = row["CustomerName"].ToString(),
                        PlanName = row["PlanName"].ToString(),
                        People = row["People"].ToString(),
                        NoOfHosts = row["NoOfHosts"].ToString(),
                        VisitedDate = row["VisitDate"].ToString(),
                        VisitedTime = row["VisitedTime"].ToString(),
                        Status = row["Status"].ToString(),
                        Price = row["Price"].ToString(),
                        CreatedDate = row["CreatedDate"].ToString(),
                        UpdatedDate = row["UpdatedDate"].ToString(),
                        SNO = Convert.ToInt32(row["SNO"].ToString()),
                        TotalRecords = Convert.ToInt32(row["TotalRecords"].ToString())
                    });
                }
            }
            return responseinfo;
        }

        public List<ApprovedBookingRequestListCommon> GetApprovedBookingList(string AgentId, SearchFilterCommon request, PaginationFilterCommon ApprovedRequest)
        {
            List<ApprovedBookingRequestListCommon> approvedlistResponse = new List<ApprovedBookingRequestListCommon>();
            string sp_name = "EXEC sproc_club_getbookingrequestlist @Flag='gapbrl'";
            sp_name += ",@SearchFilter=" + _dao.FilterString(request.SearchFilter);
            sp_name += ",@AgentId=" + _dao.FilterString(AgentId);
            sp_name += ",@ToDate=" + _dao.FilterString(request.ToDate);
            sp_name += ",@FromDate=" + _dao.FilterString(request.FromDate);
            sp_name += ",@Today=" + _dao.FilterString(request.Today);
            sp_name += ",@Tomorrow=" + _dao.FilterString(request.Tomorrow);
            sp_name += ",@DayAfterTomorrow=" + _dao.FilterString(request.DayAfterTomorrow);
            sp_name += ",@Skip=" + ApprovedRequest.Skip;
            sp_name += ",@Take=" + ApprovedRequest.Take;
            var dbApprovedResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbApprovedResponseInfo != null)
            {
                foreach (DataRow row in dbApprovedResponseInfo.Rows)
                {
                    approvedlistResponse.Add(new ApprovedBookingRequestListCommon
                    {
                        Id = row["Id"].ToString(),
                        CustomerName = row["CustomerName"].ToString(),
                        PlanName = row["PlanName"].ToString(),
                        People = row["People"].ToString(),
                        NoOfHosts = row["NoOfHosts"].ToString(),
                        VisitedDate = row["VisitDate"].ToString(),
                        VisitedTime = row["VisitedTime"].ToString(),
                        Status = row["Status"].ToString(),
                        Price = row["Price"].ToString(),
                        CreatedDate = row["CreatedDate"].ToString(),
                        UpdatedDate = row["UpdatedDate"].ToString(),
                        SNO = Convert.ToInt32(row["SNO"].ToString()),
                        TotalRecords = Convert.ToInt32(row["TotalRecords"].ToString())
                    });
                }
            }
            return approvedlistResponse;
        }

        public BookingRequestAnalyticsModelCommon GetBookingRequestAnalytics(string AgentId)
        {
            string sp_name = "EXEC sproc_club_getbookingrequestanalytics @Flag='gbra'";
            sp_name += ",@AgentId=" + _dao.FilterString(AgentId);
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null && dbResponseInfo.Rows.Count > 0)
            {
                return new BookingRequestAnalyticsModelCommon()
                {
                    TotalBooking = dbResponseInfo.Rows[0]["TotalBooking"]?.ToString(),
                    TodayBooking = dbResponseInfo.Rows[0]["TodayBooking"]?.ToString(),
                };
            }
            return new BookingRequestAnalyticsModelCommon();
        }

        public List<ClubTimeInfoModelCommon> GetClubTimeList(string agentId)
        {
            List<ClubTimeInfoModelCommon> responseInfo = new List<ClubTimeInfoModelCommon>();
            string sp_name = "EXEC sproc_club_getbookingrequestlist @Flag='gct'";
            sp_name += ",@AgentId=" + _dao.FilterString(agentId);
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new ClubTimeInfoModelCommon()
                    {
                        BookingCount = row["BookingCount"].ToString(),
                        TimeRange = row["TimeRange"].ToString()
                    });
                }
            }
            return responseInfo;
        }

        public List<PendingBookingRequestListCommon> GetPendingBookingList(string AgentId, SearchFilterCommon request, PaginationFilterCommon PendingRequest)
        {
            List<PendingBookingRequestListCommon> pendingListInfo = new List<PendingBookingRequestListCommon>();
            string sp_name = "EXEC sproc_club_getbookingrequestlist @Flag='gpbrl'";
            sp_name += ",@SearchFilter=" + _dao.FilterString(request.SearchFilter);
            sp_name += ",@AgentId=" + _dao.FilterString(AgentId);
            sp_name += ",@ToDate=" + _dao.FilterString(request.ToDate);
            sp_name += ",@FromDate=" + _dao.FilterString(request.FromDate);
            sp_name += ",@Today=" + _dao.FilterString(request.Today);
            sp_name += ",@Tomorrow=" + _dao.FilterString(request.Tomorrow);
            sp_name += ",@DayAfterTomorrow=" + _dao.FilterString(request.DayAfterTomorrow);
            sp_name += ",@Skip=" + PendingRequest.Skip;
            sp_name += ",@Take=" + PendingRequest.Take;
            var dbPendingResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbPendingResponseInfo != null)
            {
                foreach (DataRow row in dbPendingResponseInfo.Rows)
                {
                    pendingListInfo.Add(new PendingBookingRequestListCommon
                    {
                        Id = row["Id"].ToString(),
                        CustomerName = row["CustomerName"].ToString(),
                        PlanName = row["PlanName"].ToString(),
                        People = row["People"].ToString(),
                        NoOfHosts = row["NoOfHosts"].ToString(),
                        VisitedDate = row["VisitDate"].ToString(),
                        VisitedTime = row["VisitedTime"].ToString(),
                        Status = row["Status"].ToString(),
                        Price = row["Price"].ToString(),
                        CreatedDate = row["CreatedDate"].ToString(),
                        UpdatedDate = row["UpdatedDate"].ToString(),
                        SNO = Convert.ToInt32(row["SNO"].ToString()),
                        TotalRecords = Convert.ToInt32(row["TotalRecords"].ToString())
                    });
                }
            }
            return pendingListInfo;
        }

        public CommonDbResponse RejectBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform)
        {
            string sp_name = "EXEC sproc_club_approveandrejectbookingrequest @Flag='rbr'";
            sp_name += ",@Id=" + _dao.FilterString(newId);
            sp_name += ",@ActionUser=" + _dao.FilterString(actionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(actionIp);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(actionPlatform);

            var dbresponse = _dao.ParseCommonDbResponse(sp_name);
            return dbresponse;
        }
    }
}
