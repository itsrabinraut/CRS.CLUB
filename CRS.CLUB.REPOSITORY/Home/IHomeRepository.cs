using CRS.CLUB.SHARED.Home;
using CRS.CLUB.SHARED;
using System.Collections.Generic;

namespace CRS.CLUB.REPOSITORY.Home
{
    public interface IHomeRepository
    {
        #region "Dashboard Analytic Details"
        DashboardInfoCommon GetAnalyticsDetails(string clubId);
        List<ChartInfoModelCommon> GetChartInfo(string clubId);
        List<HostListModelCommon> GetHostList(string clubId);
        #endregion
        CommonDbResponse Login(LoginRequestCommon Request);

        CommonDbResponse SetNewPassword(SetNewPasswordCommon setNewPassword);
    }
}
