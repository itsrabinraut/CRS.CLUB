using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.Home;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.Home
{
    public interface IHomeBusiness
    {
        #region "Dashboard Analytics Details"
        DashboardInfoCommon GetAnalyticsDetails(string clubId);
        List<ChartInfoModelCommon> GetChartInfo(string clubId);
        List<HostListModelCommon> GetHostList(string clubId);
        #endregion
        CommonDbResponse Login(LoginRequestCommon Request);
        CommonDbResponse SetNewPassword(SetNewPasswordCommon setNewPassword);
    }
}
