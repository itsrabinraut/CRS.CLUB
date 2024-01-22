using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.Home;
using CRS.CLUB.BUSINESS.Home;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.Home;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHomeBusiness _BUSS;
        public HomeController(IHomeBusiness BUSS)
        {
            _BUSS = BUSS;
        }
        #region Login Management
        [OverrideActionFilters]
        public ActionResult Index()
        {
            var Username = ApplicationUtilities.GetSessionValue("Username").ToString();
            if (string.IsNullOrEmpty(Username))
            {
                this.ClearSessionData();
                var ResponseModel = new LoginRequestModel();
                HttpCookie cookie = Request.Cookies["CRS-CLUB-LOGINID"];
                if (cookie != null) ResponseModel.LoginId = cookie.Value.StaticDecryptData() ?? null;
                return View(ResponseModel);
            }
            else
                return RedirectToAction("Dashboard", "Home");
        }

        [OverrideActionFilters]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(LoginRequestModel Model, bool RememberMe = false)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = Login(Model);
                if (loginResponse.Item3 && !string.IsNullOrEmpty(Model.LoginId) && RememberMe)
                {
                    HttpContext.Response.Cookies.Add(new HttpCookie("CRS-CLUB-LOGINID", Model.LoginId.StaticEncryptData())
                    {
                        Expires = DateTime.Now.AddMonths(1)
                    });
                }
                else
                {
                    var LoginId = string.Empty;
                    HttpCookie cookie = Request.Cookies["CRS-CLUB-LOGINID"];
                    if (cookie != null) LoginId = cookie.Value.StaticDecryptData() ?? null;
                    HttpContext.Response.Cookies.Add(new HttpCookie("CRS-CLUB-LOGINID", "")
                    {
                        Expires = DateTime.Now.AddMonths(-1)
                    });
                }
                return RedirectToAction(loginResponse.Item1, loginResponse.Item2);
            }
            var errorMessages = ModelState.Where(x => x.Value.Errors.Count > 0)
                                    .SelectMany(x => x.Value.Errors.Select(e => $"{x.Key}: {e.ErrorMessage}"))
                                    .ToList();

            var notificationModels = errorMessages.Select(errorMessage => new NotificationModel
            {
                NotificationType = NotificationMessage.ERROR,
                Message = errorMessage,
                Title = NotificationMessage.ERROR.ToString(),
            }).ToArray();
            AddNotificationMessage(notificationModels);
            return View(Model);
        }

        public Tuple<string, string, bool> Login(LoginRequestModel Model)
        {
            LoginRequestCommon commonRequest = Model.MapObject<LoginRequestCommon>();
            commonRequest.SessionId = Session.SessionID;
            var dbResponse = _BUSS.Login(commonRequest);
            try
            {
                if (dbResponse.Code == 0)
                {
                    var response = dbResponse.Data.MapObject<LoginResponseModel>();
                    string FileLocationPath = "";
                    if (ConfigurationManager.AppSettings["Phase"] != null
                       && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                        FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
                    response.Logo = FileLocationPath + response.Logo;
                    if (!response.ChangePassword)
                    {
                        Session["SessionGuid"] = commonRequest.SessionId;
                        Session["Username"] = response.Username;
                        Session["UserId"] = response.UserId.EncryptParameter();
                        Session["AgentId"] = response.AgentId.EncryptParameter();
                        Session["ClubName"] = response.ClubName;
                        Session["ClubNameJp"] = response.ClubNameJp;
                        Session["EmailAddress"] = response.EmailAddress;
                        Session["LogoImage"] = response.Logo;
                        Session["Menus"] = response.Menus;
                        Session["Functions"] = response.Functions;
                        Session["Notifications"] = response.Notifications;
                        return new Tuple<string, string, bool>("BookingRequestList", "BookingRequest", true);
                    }
                    TempData["AgentId"] = response.AgentId.EncryptParameter();
                    return new Tuple<string, string, bool>("SetNewPassword", "Home", true);
                }
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = dbResponse.Message,
                    Title = NotificationMessage.INFORMATION.ToString(),
                });
                return new Tuple<string, string, bool>("Index", "Home", false);
            }
            catch (Exception)
            {
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Something went wrong",
                    Title = NotificationMessage.INFORMATION.ToString(),
                });
                return new Tuple<string, string, bool>("Index", "Home", false);
            }
        }

        [OverrideActionFilters]
        public ActionResult LogOff()
        {
            Session["Username"] = null;
            return RedirectToAction("Index", "Home");
        }
        #endregion

        [HttpGet]
        public ActionResult Dashboard()
        {
            Session["CurrentURL"] = "/Home/Dashboard";
            string clubId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            string FileLocationPath = "";
            if (ConfigurationManager.AppSettings["Phase"] != null
               && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;

            CommonDashboardModel responseInfo = new CommonDashboardModel();
            var dashboardDbResponse = _BUSS.GetAnalyticsDetails(clubId);
            responseInfo.GetDashboardInfoList = dashboardDbResponse.MapObject<DashboardInfoModel>();
            var hostDbResponseInfo = _BUSS.GetHostList(clubId);
            responseInfo.GetHostList = hostDbResponseInfo.MapObjects<HostListModel>();
            responseInfo.GetHostList.ForEach(x => x.HostImage = FileLocationPath + x.HostImage);

            #region "Chart Info"
            var dbChartResponse = _BUSS.GetChartInfo(clubId);
            responseInfo.GetChartInfo = dbChartResponse.MapObjects<ChartInfoModel>();
            #endregion
            return View(responseInfo);
        }

        [HttpGet, OverrideActionFilters]
        public ActionResult SetNewPassword()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, OverrideActionFilters]

        public ActionResult SetNewPassword(SetNewPasswordModel setNewPassword)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Where(x => x.Value.Errors.Count > 0)
                                   .SelectMany(x => x.Value.Errors.Select(e => $"{e.ErrorMessage}"))
                                   .ToList();
                var notificationModels = errorMessages.Select(errorMessage => new NotificationModel
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = errorMessage,
                    Title = NotificationMessage.ERROR.ToString(),
                }).ToArray();
                AddNotificationMessage(notificationModels);
                return View(setNewPassword);
            }

            string agentId = TempData.ContainsKey("AgentId") ? TempData["AgentId"].ToString() : "";
            if (string.IsNullOrWhiteSpace(agentId))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "Something went wrong",
                    Title = NotificationMessage.ERROR.ToString(),
                });
                return View(setNewPassword);
            }
            agentId = agentId.DecryptParameter();
            var setpassCommon = setNewPassword.MapObject<SetNewPasswordCommon>();
            setpassCommon.AgentId = agentId;
            setpassCommon.ActionIP = ApplicationUtilities.GetIP();
            var bussResp = _BUSS.SetNewPassword(setpassCommon);
            if (bussResp.Code == ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = Session["culture"]?.ToString()?.ToLower() == "en" ? bussResp.Message : bussResp.Extra1,
                    Title = NotificationMessage.SUCCESS.ToString(),
                });
                return RedirectToAction("Dashboard", "Home");
            }

            AddNotificationMessage(new NotificationModel()
            {
                NotificationType = NotificationMessage.ERROR,
                Message = Session["culture"]?.ToString()?.ToLower() == "en" ? bussResp.Message : bussResp.Extra1,
                Title = NotificationMessage.ERROR.ToString(),
            });
            return View(setNewPassword);
        }
    }
}