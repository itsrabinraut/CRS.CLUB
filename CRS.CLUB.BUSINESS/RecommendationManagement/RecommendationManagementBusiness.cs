using CRS.CLUB.REPOSITORY.RecommendationManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.PaginationManagement;
using CRS.CLUB.SHARED.RecommendationManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.RecommendationManagement
{
    public class RecommendationManagementBusiness : IRecommendationManagementBusiness
    {
        private readonly IRecommendationManagementRepository _repo;
        public RecommendationManagementBusiness(RecommendationManagementRepository repo)
        {
            _repo = repo;
        }

        public CommonDbResponse DeleteHomePageRequest(string clubId, string displayid, string recommendationholdid, Common commonRequest)
        {
            return _repo.DeleteHomePageRequest(clubId, displayid, recommendationholdid, commonRequest);
        }

        public CommonDbResponse DeleteMainPageRequest(string clubid, string displayid, string recommendationHoldId, Common commonRequest)
        {
            return _repo.DeleteMainPageRequest(clubid, displayid, recommendationHoldId, commonRequest);
        }

        public CommonDbResponse DeleteSearchPageRequest(string clubid, string displayid, string recommendationholdid, Common commonRequest)
        {
            return _repo.DeleteSearchPageRequest(clubid, displayid, recommendationholdid, commonRequest);
        }

        public CommonDbResponse DeleteSelectedHost(string clubId, string recommendationHostId, string recommendationHostHoldId, string hostId, Common commonRequest)
        {
            return _repo.DeletSelectedHost(clubId, recommendationHostId, recommendationHostHoldId, hostId, commonRequest);
        }

        public List<ClubHomePageRecommendationReqListModelCommon> GetClubHomePageRecommendationReqList(string agentId, PaginationFilterCommon request)
        {
            return _repo.GetClubHomePageRecommendationReqList(agentId, request);
        }

        public List<ClubMainPageHostRecommendationReqModelCommon> GetClubMainPageHostRecommedationListDetail(string agentId, string recommendationHoldId, string displayId)
        {
            return _repo.GetClubMainPageHostRecommedationListDetail(agentId, recommendationHoldId, displayId);
        }

        public ManageClubMainPageRecommendationReqCommon GetClubMainPageRecommendationReqDetail(string recommmendationHoldId, string agentId, string displayId)
        {
            return _repo.GetClubMainPageRecommendationReqDetail(recommmendationHoldId, agentId, displayId);
        }

        public List<DisplayPageListModelCommon> GetDisplayPage()
        {
            return _repo.GetDisplayPage();
        }

        public List<HostListDetailModelCommon> GetHostListDetail(string recommendationHoldId, string agentId, string displayId)
        {
            return _repo.GetHostListDetail(recommendationHoldId, agentId, displayId);
        }

        public List<ClubMainPageRecommendationReqListModelCommon> GetMainPageRecommendationReqList(string agentid, PaginationFilterCommon request)
        {
            return _repo.GetMainPageRecommendationReqList(agentid, request);
        }

        public List<ClubSearchPageRecommendationReqListModelCommon> GetSearchPageRecommendationReqList(string agentId, PaginationFilterCommon request)
        {
            return _repo.GetSearchPageRecommendationReqList(agentId, request);
        }

        public CommonDbResponse ManageHomePageRecommendationReq(ManageClubHomePageRecommendationReqCommon commonModel)
        {
            return _repo.ManageHomePageRecommendationReq(commonModel);
        }

        public CommonDbResponse ManageMainPageRecommendationReq(ManageClubMainPageRecommendationReqCommon commonModel)
        {
            return _repo.ManageMainPageRecommendationReq(commonModel);
        }

        public CommonDbResponse ManageSearchPageRecommendationReq(ManageClubSearchPageRecommendationReqCommon commonModel)
        {
            return _repo.ManageSearchPageRecommendationReq(commonModel);
        }
    }
}
