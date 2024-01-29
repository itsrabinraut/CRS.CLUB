using CRS.CLUB.REPOSITORY.BookingRequest;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.BookingRequest;
using CRS.CLUB.SHARED.PaginationManagement;
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

        public List<AllBookingRequestListCommon> GetAllBookingRequestList(string AgentId, SearchFilterCommon request,PaginationFilterCommon AllRequest)
        {
            return _repo.GetAllBookingRequestList(AgentId, request, AllRequest);
        }

        public List<ApprovedBookingRequestListCommon> GetApprovedBookingList(string AgentId, SearchFilterCommon request, PaginationFilterCommon ApprovedRequest)
        {
            return _repo.GetApprovedBookingList(AgentId, request, ApprovedRequest);
        }

        public BookingRequestAnalyticsModelCommon GetBookingRequestAnalytics(string AgentId)
        {
            return _repo.GetBookingRequestAnalytics(AgentId);
        }

        public List<ClubTimeInfoModelCommon> GetClubTimeList(string agentId)
        {
            return _repo.GetClubTimeList(agentId);
        }

        public List<PendingBookingRequestListCommon> GetPendingBookingList(string AgentId, SearchFilterCommon request, PaginationFilterCommon PendingRequest)
        {
            return _repo.GetPendingBookingList(AgentId, request, PendingRequest);
        }

        public CommonDbResponse RejectBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform)
        {
            return _repo.RejectBookingRequest(newId, actionUser, actionIp, actionPlatform);
        }
    }
}
