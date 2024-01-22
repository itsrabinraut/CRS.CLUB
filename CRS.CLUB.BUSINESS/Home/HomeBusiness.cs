using CRS.CLUB.REPOSITORY.Home;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.Home;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.Home
{
    public class HomeBusiness : IHomeBusiness
    {
        IHomeRepository _REPO;
        public HomeBusiness(HomeRepository REPO)
        {
            _REPO = REPO;
        }
        #region"Dashboard Analytics Details"
        public DashboardInfoCommon GetAnalyticsDetails(string clubId)
        {
            return _REPO.GetAnalyticsDetails(clubId);
        }

        public List<ChartInfoModelCommon> GetChartInfo(string clubId)
        {
            return _REPO.GetChartInfo(clubId);
        }

        public List<HostListModelCommon> GetHostList(string clubId)
        {
            return _REPO.GetHostList(clubId);
        }
        #endregion

        public CommonDbResponse Login(LoginRequestCommon Request)
        {
            return _REPO.Login(Request);
        }

        public CommonDbResponse SetNewPassword(SetNewPasswordCommon setNewPassword)
        {
            return _REPO.SetNewPassword(setNewPassword);
        }
    }
}
