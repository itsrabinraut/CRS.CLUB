using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.PaginationManagement;
using CRS.CLUB.SHARED.RecommendationManagement;
using System.Collections.Generic;

namespace CRS.CLUB.REPOSITORY.RecommendationManagement
{
    public interface IRecommendationManagementRepository
    {
        CommonDbResponse DeleteHomePageRequest(string clubId, string displayid, string recommendationholdid, Common commonRequest);
        CommonDbResponse DeleteMainPageRequest(string clubid, string displayid, string recommendationHoldId, Common commonRequest);
        CommonDbResponse DeleteSearchPageRequest(string clubid, string displayid, string recommendationholdid, Common commonRequest);
        CommonDbResponse DeletSelectedHost(string clubId, string recommendationHostId, string recommendationHostHoldId, string hostId, Common commonRequest);
        List<ClubHomePageRecommendationReqListModelCommon> GetClubHomePageRecommendationReqList(string agentId, PaginationFilterCommon request);
        List<ClubMainPageHostRecommendationReqModelCommon> GetClubMainPageHostRecommedationListDetail(string agentId, string recommendationHoldId, string displayId);
        ManageClubMainPageRecommendationReqCommon GetClubMainPageRecommendationReqDetail(string recommmendationHoldId, string agentId, string displayId);
        List<DisplayPageListModelCommon> GetDisplayPage();
        List<HostListDetailModelCommon> GetHostListDetail(string recommendationHoldId, string agentId, string displayId);
        List<ClubMainPageRecommendationReqListModelCommon> GetMainPageRecommendationReqList(string agentid, PaginationFilterCommon request);
        List<ClubSearchPageRecommendationReqListModelCommon> GetSearchPageRecommendationReqList(string agentId, PaginationFilterCommon request);
        CommonDbResponse ManageHomePageRecommendationReq(ManageClubHomePageRecommendationReqCommon commonModel);
        CommonDbResponse ManageMainPageRecommendationReq(ManageClubMainPageRecommendationReqCommon commonModel);
        CommonDbResponse ManageSearchPageRecommendationReq(ManageClubSearchPageRecommendationReqCommon commonModel);
    }
}
