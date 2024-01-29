using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.CLUB.APPLICATION.Models.RecommendationManagement
{
    public class RecommendationReqCommonModel
    {
        public List<DisplayPageListModel> GetDisplayPage { get; set; }
        public List<ClubSearchPageRecommendationReqListModel> GetClubSearchPageRecommendationReqList { get; set; }
        public List<ClubHomePageRecommendationReqListModel> GetClubHomePageRecommendationReqList { get; set; }
        public List<ClubMainPageRecommendationReqListModel> GetClubMainPageRecommendationReqList { get; set; }
        public List<ClubMainPageHostRecommendationReqModel> GetClubMainPageHostRecommedationListDetail { get; set; }
        public List<HostListDetailModel> GetHostListDetail { get; set; }
        public ManageClubMainPageRecommendationReq GetClubMainPageRecommendationReqDetail { get; set; } = new ManageClubMainPageRecommendationReq();
    }
    #region "DISPLAY PAGE MODEL"
    public class DisplayPageListModel
    {
        public string PageId { get; set; }
        public string PageLabel { get; set; }
    }
    #endregion

    #region "SEARCH PAGE RECOMMENDATION REQUEST"
    public class ClubSearchPageRecommendationReqListModel
    {
        public string RecommendationHoldId { get; set; }
        public string ClubId { get; set; }
        public string ClubLogo { get; set; }
        public string ClubName { get; set; }
        public string ClubCategory { get; set; }
        public string DisplayId { get; set; }
        public string DisplayPageLabel { get; set; }
        public string Status { get; set; }
        public string RequestedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string HostName { get; set; }
        public int SNO { get; set; }
        public int TotalRecords { get; set; }
    }
    public class ManageClubSearchPageRecommendationReq
    {
        public string RecommendationId { get; set; }
        public string ClubId { get; set; }
        public string HostId { get; set; }
    }
    #endregion

    #region "HOME PAGE RECOMMENDATION REQUEST"
    public class ClubHomePageRecommendationReqListModel
    {
        public string RecommendationHoldId { get; set; }
        public string ClubId { get; set; }
        public string ClubLogo { get; set; }
        public string ClubName { get; set; }
        public string ClubCategory { get; set; }
        public string DisplayId { get; set; }
        public string DisplayPageLabel { get; set; }
        public string Status { get; set; }
        public string RequestedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string HostName { get; set; }
        public int SNO { get; set; }
        public int TotalRecords { get; set; }
    }
    public class ManageClubHomePageRecommendationReq
    {
        public string RecommendationId { get; set; }
        public string ClubId { get; set; }
        public string HostId { get; set; }
    }
    #endregion

    #region "Main PAGE RECOMMENDATION REQUEST"
    public class ClubMainPageRecommendationReqListModel
    {
        public string RecommendationHoldId { get; set; }
        public string ClubId { get; set; }
        public string ClubLogo { get; set; }
        public string ClubName { get; set; }
        public string ClubCategory { get; set; }
        public string DisplayId { get; set; }
        public string DisplayPageLabel { get; set; }
        public string Status { get; set; }
        public string RequestedDate { get; set; }
        public string UpdatedDate { get; set; }
        public int TotalRecords { get; set; }
        public int SNO { get; set; }
    }
    public class ManageClubMainPageRecommendationReq
    {
        public string RecommendationHoldId { get; set; }
        public string ClubId { get; set; }
        public string OrderId { get; set; }
        public string HostRecommendationCSValue { get; set; }
        public string HostId { get; set; }
        public string DisplayId { get; set; }
    }

    public class ClubMainPageHostRecommendationReqModel
    {
        public string RecommendationHoldId { get; set; }
        public string RecommendationHostHoldId { get; set; }
        public string HostId { get; set; }
        public string HostDisplayOrderId { get; set; }
    }
    #endregion

    #region "HOST LIST PAGE MODEL"
    public class HostListDetailModel
    {
        public string RecommendationHoldId { get; set; }
        public string HostId { get; set; }
        public string RecommendationHostHoldId { get; set; }
        public string ClubId { get; set; }
        public string HostName { get; set; }
        public string DisplayOrder { get; set; }
        public string HostImage { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string Status { get; set; }
    }
    #endregion
}