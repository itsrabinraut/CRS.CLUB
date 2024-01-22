using CRS.CLUB.REPOSITORY.BookingRequest;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.BookingRequest;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.BookingRequest
{
    public class BookingRequestBusiness : IBookingRequestBusiness
    {
        private readonly IBookingRequestRepository _repo;
        public BookingRequestBusiness(BookingRequestRepository repo)
        {
            _repo = repo;
        }

        public CommonDbResponse ApproveBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform)
        {
            return _repo.ApproveBookingRequest(newId, actionUser, actionIp, actionPlatform);
        }

        public List<AllBookingRequestListCommon> GetAllBookingRequestList(string AgentId, SearchFilterCommon request)
        {
            return _repo.GetAllBookingRequestList(AgentId, request);
        }

        public List<ApprovedBookingRequestListCommon> GetApprovedBookingList(string AgentId, SearchFilterCommon request)
        {
            return _repo.GetApprovedBookingList(AgentId, request);
        }

        public BookingRequestAnalyticsModelCommon GetBookingRequestAnalytics(string AgentId)
        {
            return _repo.GetBookingRequestAnalytics(AgentId);
        }

        public List<ClubTimeInfoModelCommon> GetClubTimeList(string agentId)
        {
            return _repo.GetClubTimeList(agentId);
        }

        public List<PendingBookingRequestListCommon> GetPendingBookingList(string AgentId, SearchFilterCommon request)
        {
            return _repo.GetPendingBookingList(AgentId, request);
        }

        public CommonDbResponse RejectBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform)
        {
            return _repo.RejectBookingRequest(newId, actionUser, actionIp, actionPlatform);
        }
    }
}
