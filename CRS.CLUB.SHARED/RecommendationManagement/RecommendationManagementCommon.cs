namespace CRS.CLUB.SHARED.RecommendationManagement
{
    #region "DISPLAY PAGE MODEL"
    public class DisplayPageListModelCommon
    {
        public string PageId { get; set; }
        public string PageLabel { get; set; }
    }

    #endregion

    #region "CLUB SEARCH PAGE RECOMMENDATION REQUEST MODEL"
    public class ClubSearchPageRecommendationReqListModelCommon
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
    }
    public class ManageClubSearchPageRecommendationReqCommon : Common
    {
        public string RecommendationId { get; set; }
        public string ClubId { get; set; }
        public string HostId { get; set; }
    }
    #endregion

    #region "HOME PAGE RECOMMENDATION REQUEST"
    public class ClubHomePageRecommendationReqListModelCommon
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
    }
    public class ManageClubHomePageRecommendationReqCommon : Common
    {
        public string RecommendationId { get; set; }
        public string ClubId { get; set; }
        public string HostId { get; set; }
    }
    #endregion

    #region "Main PAGE RECOMMENDATION REQUEST"
    public class ClubMainPageRecommendationReqListModelCommon
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
    }
    public class ManageClubMainPageRecommendationReqCommon : Common
    {
        public string RecommendationHoldId { get; set; }
        public string ClubId { get; set; }
        public string OrderId { get; set; }
        public string HostRecommendationCSValue { get; set; }
        public string HostId { get; set; }
        public string DisplayId { get; set; }
    }
    public class ClubMainPageHostRecommendationReqModelCommon
    {
        public string RecommendationHoldId { get; set; }
        public string RecommendationHostHoldId { get; set; }
        public string HostId { get; set; }
        public string HostDisplayOrderId { get; set; }
    }
    #endregion

    #region "HOST LIST PAGE MODEL"
    public class HostListDetailModelCommon
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
