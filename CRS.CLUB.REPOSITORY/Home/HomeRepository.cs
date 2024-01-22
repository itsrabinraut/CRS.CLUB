using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.Home;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CRS.CLUB.REPOSITORY.Home
{
    public class HomeRepository : IHomeRepository
    {
        private readonly RepositoryDao _dao;
        public HomeRepository() => _dao = new RepositoryDao();
        #region"Dashboard Analytic Details"
        public DashboardInfoCommon GetAnalyticsDetails(string clubId)
        {
            string sp_name = "EXEC sproc_club_getdashboardanalyticdetail @Flag='gdad'";
            sp_name += ",@ClubId=" + _dao.FilterString(clubId);
            var dbResponseInfo = _dao.ExecuteDataRow(sp_name);
            if (dbResponseInfo != null)
            {
                return new DashboardInfoCommon()
                {
                    TotalBooking = _dao.ParseColumnValue(dbResponseInfo, "TotalBooking").ToString(),
                    TotalHost = _dao.ParseColumnValue(dbResponseInfo, "TotalHost").ToString(),
                    TotalVisitor = _dao.ParseColumnValue(dbResponseInfo, "TotalVisitors").ToString(),
                    TotalSales = _dao.ParseColumnValue(dbResponseInfo, "TotalSales").ToString()
                };

            }
            return new DashboardInfoCommon();
        }

        public List<ChartInfoModelCommon> GetChartInfo(string clubId)
        {
            List<ChartInfoModelCommon> responseInfo = new List<ChartInfoModelCommon>();
            string sp_name = "sproc_club_getdashboardanalyticdetail @Flag='gci'";
            sp_name += ",@ClubId=" + _dao.FilterString(clubId);
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new ChartInfoModelCommon()
                    {
                        Month = row["Month"].ToString(),
                        TotalSales = row["TotalSales"].ToString()
                    });
                }
            }
            return responseInfo;
        }

        public List<HostListModelCommon> GetHostList(string clubId)
        {
            List<HostListModelCommon> responseInfo = new List<HostListModelCommon>();
            string sp_name = "EXEC sproc_club_getdashboardanalyticdetail @Flag='ghl'";
            sp_name += ",@ClubId=" + _dao.FilterString(clubId);
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new HostListModelCommon
                    {
                        HostImage = row["HostImage"].ToString(),
                        HostName = row["HostName"].ToString(),
                        HostPosition = row["HostPosition"].ToString(),
                        BookingCount = row["BookingCount"].ToString()
                    });
                }
            }
            return responseInfo;
        }
        #endregion
        #region Login
        public CommonDbResponse Login(LoginRequestCommon Request)
        {
            string SQL = "EXEC sproc_club_login_management @flag='Login'";
            SQL += ",@LoginId=" + _dao.FilterString(Request.LoginId);
            SQL += ",@Password=" + _dao.FilterString(Request.Password);
            SQL += ",@ActionIP=" + _dao.FilterString(Request.ActionIP);
            SQL += ",@ActionPlatform=" + _dao.FilterString(Request.ActionPlatform);
            var dbResponse = _dao.ExecuteDataRow(SQL);
            if (dbResponse != null)
            {
                string code = _dao.ParseColumnValue(dbResponse, "Code").ToString();
                string message = _dao.ParseColumnValue(dbResponse, "Message").ToString();
                if (string.IsNullOrEmpty(code) || code.Trim() != "0")
                {
                    return new CommonDbResponse()
                    {
                        Code = ResponseCode.Failed,
                        Message = string.IsNullOrEmpty(message) ? "Failed" : message
                    };
                }
                else
                {
                    var loginResponse = new LoginResponseCommon()
                    {
                        Username = _dao.ParseColumnValue(dbResponse, "Username").ToString(),
                        ClubName = _dao.ParseColumnValue(dbResponse, "ClubName").ToString(),
                        ClubNameJp = _dao.ParseColumnValue(dbResponse, "ClubNameJp").ToString(),
                        AgentId = _dao.ParseColumnValue(dbResponse, "AgentId").ToString(),
                        UserId = _dao.ParseColumnValue(dbResponse, "UserId").ToString(),
                        EmailAddress = _dao.ParseColumnValue(dbResponse, "EmailAddress").ToString(),
                        Logo = _dao.ParseColumnValue(dbResponse, "Logo").ToString(),
                        SessionId = _dao.ParseColumnValue(dbResponse, "SessionId").ToString(),
                        RoleId = _dao.ParseColumnValue(dbResponse, "RoleId").ToString(),
                        ChangePassword = _dao.ParseColumnValue(dbResponse, "IsPasswordForceful").ToString().Trim().ToLower() == "y"
                    };

                    string menuSQL = "EXEC sproc_get_menus @Flag='gcm'";
                    menuSQL += ",@Username=" + _dao.FilterString(Request.LoginId);
                    var menuDBResponse = _dao.ExecuteDataTable(menuSQL);
                    if (menuDBResponse != null)
                        loginResponse.Menus = _dao.DataTableToListObject<MenuCommon>(menuDBResponse).ToList();
                    else loginResponse.Menus = null;

                    string functionSQL = "EXEC sproc_get_function @Flag='gcf'";
                    functionSQL += ",@RoleId=" + _dao.FilterString(loginResponse.RoleId);
                    var functionDBResponse = _dao.ExecuteDataTable(functionSQL);
                    loginResponse.Functions = new List<string>();
                    if (functionDBResponse != null)
                    {
                        foreach (DataRow item in functionDBResponse.Rows)
                        {
                            loginResponse.Functions.Add(item["FunctionURL"].ToString());
                        }
                    }

                    var notificationSQL = "sproc_club_notification_management @Flag='s'";
                    notificationSQL += ",@ActionUser=" + _dao.FilterString(Request.LoginId);
                    notificationSQL += ",@AgentId=" + _dao.FilterString(loginResponse.AgentId);
                    var notiDBResp = _dao.ExecuteDataTable(notificationSQL);
                    if (notiDBResp != null)
                    {
                        loginResponse.Notifications = _dao.DataTableToListObject<NotificationCommon>(notiDBResp).ToList();
                        loginResponse.Notifications.ForEach(x => x.TotalCount = loginResponse.Notifications.Count.ToString());
                    }

                    return new CommonDbResponse()
                    {
                        Code = ResponseCode.Success,
                        Message = "Success",
                        Data = loginResponse
                    };
                }
            }
            return new CommonDbResponse()
            {
                Code = ResponseCode.Failed,
                Message = "Login failed"
            };
        }

        public CommonDbResponse SetNewPassword(SetNewPasswordCommon setNewPassword)
        {
            string SQL = "sproc_club_login_management @Flag='setpass'";
            SQL += ",@Password=" + _dao.FilterString(setNewPassword.Password);
            SQL += ",@ConfirmPassword=" + _dao.FilterString(setNewPassword.ConfirmPassword);
            SQL += ",@ActionIP=" + _dao.FilterString(setNewPassword.ActionIP);
            SQL += ",@ActionPlatform=" + _dao.FilterString(setNewPassword.ActionPlatform);
            SQL += ",@AgentId=" + _dao.FilterString(setNewPassword.AgentId);
            return _dao.ParseCommonDbResponse(SQL);
        }
        #endregion
    }
}