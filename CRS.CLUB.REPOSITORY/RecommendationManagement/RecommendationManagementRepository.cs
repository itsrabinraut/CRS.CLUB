using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.PaginationManagement;
using CRS.CLUB.SHARED.RecommendationManagement;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;

namespace CRS.CLUB.REPOSITORY.RecommendationManagement
{
    public class RecommendationManagementRepository : IRecommendationManagementRepository
    {
        private readonly RepositoryDao _dao;
        public RecommendationManagementRepository()
        {
            _dao = new RepositoryDao();
        }


        #region "DISPLAY PAGE"
        public List<DisplayPageListModelCommon> GetDisplayPage()
        {
            List<DisplayPageListModelCommon> responseInfo = new List<DisplayPageListModelCommon>();
            string sp_name = "EXEC dbo.sproc_admin_recommendation_location_page @Flag= 'Page'";
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new DisplayPageListModelCommon()
                    {
                        PageId = row["PageId"].ToString(),
                        PageLabel = row["PageLabel"].ToString()
                    });
                }
            }
            return responseInfo;
        }
        #endregion

        #region "CLUB SEARCH PAGE RECOMMENDATION REQUEST"
        public List<ClubSearchPageRecommendationReqListModelCommon> GetSearchPageRecommendationReqList(string agentId, PaginationFilterCommon request)
        {
            List<ClubSearchPageRecommendationReqListModelCommon> responseInfo = new List<ClubSearchPageRecommendationReqListModelCommon>();
            string sp_name = "EXEC sproc_club_searchpage_recommendation_management @Flag= 'gspac'";
            sp_name += ",@ClubId=" + _dao.FilterString(agentId);
            sp_name += ",@Skip=" + request.Skip;
            sp_name += ",@Take=" + request.Take;

            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new ClubSearchPageRecommendationReqListModelCommon()
                    {
                        ClubName = row["ClubName"].ToString(),
                        ClubId = row["ClubId"].ToString(),
                        ClubCategory = row["ClubCategory"].ToString(),
                        ClubLogo = row["ClubLogo"].ToString(),
                        DisplayId = row["DisplayId"].ToString(),
                        DisplayPageLabel = row["DisplayPageLabel"].ToString(),
                        HostName = row["HostName"].ToString(),
                        RecommendationHoldId = row["RecommendationHoldId"].ToString(),
                        RequestedDate = row["RequestedDate"].ToString(),
                        Status = row["Status"].ToString(),
                        UpdatedDate = row["UpdatedDate"].ToString(),
                        SNO = Convert.ToInt32(row["SNO"].ToString()),
                        TotalRecords = Convert.ToInt32(row["TotalRecords"].ToString())
                    });
                }
            }
            return responseInfo;
        }

        public CommonDbResponse ManageSearchPageRecommendationReq(ManageClubSearchPageRecommendationReqCommon commonModel)
        {
            string sp_name = "EXEC sproc_club_searchpage_recommendation_management @Flag = 'ccspr'";
            sp_name += ",@ClubId=" + _dao.FilterString(commonModel.ClubId);
            sp_name += ",@HostId=" + _dao.FilterString(commonModel.HostId);
            sp_name += ",@ActionUser=" + _dao.FilterString(commonModel.ActionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(commonModel.ActionIP);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(commonModel.ActionPlatform);
            return _dao.ParseCommonDbResponse(sp_name);
        }
        public CommonDbResponse DeleteSearchPageRequest(string clubid, string displayid, string recommendationholdid, Common commonRequest)
        {
            string sp_name = "EXEC sproc_club_searchpage_recommendation_management @Flag= 'dspr'";
            sp_name += ",@ClubId=" + _dao.FilterString(clubid);
            sp_name += ",@ActionUser=" + _dao.FilterString(commonRequest.ActionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(commonRequest.ActionIP);
            sp_name += ",@RecommendationHoldId=" + _dao.FilterString(recommendationholdid);
            sp_name += ",@DisplayId=" + _dao.FilterString(displayid);
            return _dao.ParseCommonDbResponse(sp_name);
        }

        #endregion

        #region "MANAGE HOME PAGE RECOMMENDATION REQUEST"
        public List<ClubHomePageRecommendationReqListModelCommon> GetClubHomePageRecommendationReqList(string agentId, PaginationFilterCommon request)
        {
            List<ClubHomePageRecommendationReqListModelCommon> responseInfo = new List<ClubHomePageRecommendationReqListModelCommon>();
            string sp_name = "EXEC sproc_club_homepage_recommendation_management @Flag = 'ghpac'";
            sp_name += ",@ClubId=" + _dao.FilterString(agentId);
            sp_name += ",@Skip=" + request.Skip;
            sp_name += ",@Take=" + request.Take;

            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new ClubHomePageRecommendationReqListModelCommon()
                    {
                        ClubName = row["ClubName"].ToString(),
                        ClubId = row["ClubId"].ToString(),
                        ClubCategory = row["ClubCategory"].ToString(),
                        ClubLogo = row["ClubLogo"].ToString(),
                        DisplayId = row["DisplayId"].ToString(),
                        DisplayPageLabel = row["DisplayPageLabel"].ToString(),
                        HostName = row["HostName"].ToString(),
                        RecommendationHoldId = row["RecommendationHoldId"].ToString(),
                        RequestedDate = row["RequestedDate"].ToString(),
                        Status = row["Status"].ToString(),
                        UpdatedDate = row["UpdatedDate"].ToString(),
                        SNO = Convert.ToInt32(row["SNO"].ToString()),
                        TotalRecords = Convert.ToInt32(row["TotalRecords"].ToString())
                    });
                }
            }
            return responseInfo;
        }

        public CommonDbResponse ManageHomePageRecommendationReq(ManageClubHomePageRecommendationReqCommon commonModel)
        {
            string sp_name = "EXEC sproc_club_homepage_recommendation_management @Flag= 'cchpr'";
            sp_name += ",@ClubId=" + _dao.FilterString(commonModel.ClubId);
            sp_name += ",@HostId=" + _dao.FilterString(commonModel.HostId);
            sp_name += ",@ActionUser=" + _dao.FilterString(commonModel.ActionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(commonModel.ActionIP);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(commonModel.ActionPlatform);
            return _dao.ParseCommonDbResponse(sp_name);
        }
        public CommonDbResponse DeleteHomePageRequest(string clubId, string displayid, string recommendationholdid, Common commonRequest)
        {
            string sp_name = "EXEC sproc_club_homepage_recommendation_management @Flag= 'dhpr'";
            sp_name += ",@ClubId=" + _dao.FilterString(clubId);
            sp_name += ",@ActionUser=" + _dao.FilterString(commonRequest.ActionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(commonRequest.ActionIP);
            sp_name += ",@RecommendationHoldId=" + _dao.FilterString(recommendationholdid);
            sp_name += ",@DisplayId=" + _dao.FilterString(displayid);
            return _dao.ParseCommonDbResponse(sp_name);
        }
        #endregion

        #region "MANAGE MAIN PAGE RECOMMENDATION REQUEST"
        public List<ClubMainPageRecommendationReqListModelCommon> GetMainPageRecommendationReqList(string agentid, PaginationFilterCommon request)
        {
            List<ClubMainPageRecommendationReqListModelCommon> responseInfo = new List<ClubMainPageRecommendationReqListModelCommon>();
            string sp_name = "EXEC sproc_club_mainpage_recommendation_management @Flag= 'ghpac'";
            sp_name += ",@ClubId=" + _dao.FilterString(agentid);
            sp_name += ",@Skip=" + request.Skip;
            sp_name += ",@Take=" + request.Take;

            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new ClubMainPageRecommendationReqListModelCommon()
                    {
                        ClubName = row["ClubName"].ToString(),
                        ClubId = row["ClubId"].ToString(),
                        ClubCategory = row["ClubCategory"].ToString(),
                        ClubLogo = row["ClubLogo"].ToString(),
                        DisplayId = row["DisplayId"].ToString(),
                        DisplayPageLabel = row["DisplayPageLabel"].ToString(),
                        RecommendationHoldId = row["RecommendationHoldId"].ToString(),
                        RequestedDate = row["RequestedDate"].ToString(),
                        Status = row["Status"].ToString(),
                        UpdatedDate = row["UpdatedDate"].ToString(),
                        TotalRecords = Convert.ToInt32(row["TotalRecords"].ToString()),
                        SNO = Convert.ToInt32(row["SNO"].ToString())
                    });
                }
            }
            return responseInfo;
        }

        public ManageClubMainPageRecommendationReqCommon GetClubMainPageRecommendationReqDetail(string recommmendationHoldId, string agentId, string displayId)
        {
            string sp_name = "EXEC sproc_club_mainpage_recommendation_management @Flag= 'gmprd'";
            sp_name += ",@RecommendationHoldId=" + _dao.FilterString(recommmendationHoldId);
            sp_name += ",@ClubId=" + _dao.FilterString(agentId);
            sp_name += ",@DisplayId=" + _dao.FilterString(displayId);
            var dbResponseInfo = _dao.ExecuteDataRow(sp_name);
            if (dbResponseInfo != null)
            {
                return new ManageClubMainPageRecommendationReqCommon()
                {
                    RecommendationHoldId = _dao.ParseColumnValue(dbResponseInfo, "RecommendationHoldId").ToString(),
                    ClubId = _dao.ParseColumnValue(dbResponseInfo, "ClubId").ToString(),
                    DisplayId = _dao.ParseColumnValue(dbResponseInfo, "DisplayId").ToString(),
                    OrderId = _dao.ParseColumnValue(dbResponseInfo, "DisplayOrderId").ToString(),
                };
            }
            return new ManageClubMainPageRecommendationReqCommon();
        }

        public List<ClubMainPageHostRecommendationReqModelCommon> GetClubMainPageHostRecommedationListDetail(string agentId, string recommendationHoldId, string displayId)
        {
            List<ClubMainPageHostRecommendationReqModelCommon> responseInfo = new List<ClubMainPageHostRecommendationReqModelCommon>();
            string sp_name = "EXEC sproc_club_mainpage_recommendation_management @Flag= 'gmphrd'";
            sp_name += ",@ClubId=" + _dao.FilterString(agentId);
            sp_name += ",@RecommendationHoldId=" + _dao.FilterString(recommendationHoldId);
            sp_name += ",@DisplayId=" + _dao.FilterString(displayId);
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new ClubMainPageHostRecommendationReqModelCommon()
                    {
                        HostDisplayOrderId = row["HostDisplayOrderId"].ToString(),
                        HostId = row["HostId"].ToString(),
                        RecommendationHoldId = row["RecommendationHoldId"].ToString(),
                        RecommendationHostHoldId = row["RecommendationHostHoldId"].ToString()
                    });
                }
            }
            return responseInfo;
        }

        public CommonDbResponse ManageMainPageRecommendationReq(ManageClubMainPageRecommendationReqCommon commonModel)
        {
            string sp_name = "";
            if (!string.IsNullOrEmpty(commonModel.RecommendationHoldId))
            {
                sp_name = "EXEC sproc_club_mainpage_recommendation_management @Flag= 'ucmpr'";
                sp_name += ",@RecommendationHoldId=" + _dao.FilterParameter(commonModel.RecommendationHoldId);
                sp_name += ",@DisplayId=" + _dao.FilterParameter(commonModel.DisplayId);
            }
            else
            {
                sp_name = "EXEC sproc_club_mainpage_recommendation_management @Flag= 'ccmpr'";
            }
            sp_name += ",@ClubId=" + _dao.FilterString(commonModel.ClubId);
            sp_name += ",@OrderId=" + _dao.FilterString(commonModel.OrderId);
            sp_name += ",@HostRecommendationCSValue=" + _dao.FilterString(commonModel.HostRecommendationCSValue);
            sp_name += ",@ActionUser=" + _dao.FilterString(commonModel.ActionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(commonModel.ActionIP);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(commonModel.ActionPlatform);
            return _dao.ParseCommonDbResponse(sp_name);
        }

        public List<HostListDetailModelCommon> GetHostListDetail(string recommendationHoldId, string agentId, string displayId)
        {
            List<HostListDetailModelCommon> responseInfo = new List<HostListDetailModelCommon>();
            string sp_name = "EXEC sproc_club_mainpage_recommendation_management @Flag = 'gmprh'";
            sp_name += ",@RecommendationHoldId=" + _dao.FilterString(recommendationHoldId);
            sp_name += ",@ClubId=" + _dao.FilterString(agentId);
            sp_name += ",@DisplayId=" + _dao.FilterString(displayId);
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new HostListDetailModelCommon()
                    {
                        ClubId = row["ClubId"].ToString(),
                        HostId = row["HostId"].ToString(),
                        RecommendationHoldId = row["RecommendationHoldId"].ToString(),
                        RecommendationHostHoldId = row["RecommendationHostHoldId"].ToString(),
                        DisplayOrder = row["DisplayOrder"].ToString(),
                        HostName = row["HostName"].ToString(),
                        HostImage = row["HostImage"].ToString(),
                        Status = row["Status"].ToString(),
                        CreatedOn = row["CreatedOn"].ToString(),
                        UpdatedOn = row["UpdatedOn"].ToString(),
                    });
                }
            }
            return responseInfo;
        }

        public CommonDbResponse DeletSelectedHost(string clubId, string recommendationHostId, string recommendationHostHoldId, string hostId, Common commonRequest)
        {
            string sp_name = "EXEC sproc_club_mainpage_recommendation_management @Flag = 'rmprh'";
            sp_name += ",@ClubId=" + _dao.FilterString(clubId);
            sp_name += ",@RecommendationHostId=" + _dao.FilterString(recommendationHostId);
            sp_name += ",@RecommendationHoldId=" + _dao.FilterString(recommendationHostHoldId);
            sp_name += ",@HostId=" + _dao.FilterString(hostId);
            sp_name += ",@ActionUser=" + _dao.FilterString(commonRequest.ActionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(commonRequest.ActionIP);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(commonRequest.ActionPlatform);
            return _dao.ParseCommonDbResponse(sp_name);
        }

        public CommonDbResponse DeleteMainPageRequest(string clubid, string displayid, string recommendationHoldId, Common commonRequest)
        {
            string sp_name = "EXEC sproc_club_mainpage_recommendation_management @Flag = 'dmpr'";
            sp_name += ",@ClubId=" + _dao.FilterString(clubid);
            sp_name += ",@DisplayId=" + _dao.FilterString(displayid);
            sp_name += ",@RecommendationHoldId=" + _dao.FilterString(recommendationHoldId);
            sp_name += ",@ActionUser=" + _dao.FilterString(commonRequest.ActionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(commonRequest.ActionIP);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(commonRequest.ActionPlatform);
            return _dao.ParseCommonDbResponse(sp_name);
        }
        #endregion
    }
}
