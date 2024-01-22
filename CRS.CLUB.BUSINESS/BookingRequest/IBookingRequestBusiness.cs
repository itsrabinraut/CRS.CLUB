using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.BookingRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.BUSINESS.BookingRequest
{
    public interface IBookingRequestBusiness
    {
        CommonDbResponse ApproveBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform);
        List<AllBookingRequestListCommon> GetAllBookingRequestList(string AgentId, SearchFilterCommon request);
        List<ApprovedBookingRequestListCommon> GetApprovedBookingList(string AgentId, SearchFilterCommon request);
        BookingRequestAnalyticsModelCommon GetBookingRequestAnalytics(string AgentId);
        List<ClubTimeInfoModelCommon> GetClubTimeList(string agentId);
        List<PendingBookingRequestListCommon> GetPendingBookingList( string AgentId, SearchFilterCommon request);
        CommonDbResponse RejectBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform);
    }
}
