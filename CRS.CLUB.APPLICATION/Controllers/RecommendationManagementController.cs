using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.RecommendationManagement;
using CRS.CLUB.BUSINESS.RecommendationManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.PaginationManagement;
using CRS.CLUB.SHARED.RecommendationManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{

    public class RecommendationManagementController : BaseController
    {
        private readonly IRecommendationManagementBusiness _business;
        public RecommendationManagementController(IRecommendationManagementBusiness business)
        {
            _business = business;
        }
        #region "DISPLAY PAGES"
        [HttpGet]
        public ActionResult Index()
        {
            Session["CurrentURL"] = "/RecommendationManagement/Index";
            RecommendationReqCommonModel responseInfo = new RecommendationReqCommonModel();
            var dbDisplayPage = _business.GetDisplayPage();
            responseInfo.GetDisplayPage = dbDisplayPage.MapObjects<DisplayPageListModel>();
            foreach (var item in responseInfo.GetDisplayPage)
            {
                item.PageId = item.PageId.EncryptParameter();
            }
            return View(responseInfo);
        }
        #endregion

        #region "MANAGE HOME PAGE RECOMMENDATION REQUEST"
        [HttpGet]
        public ActionResult HomePageRecommendationReqList(string pageid = "", int StartIndex = 0, int PageSize = 10)
        {
            RecommendationReqCommonModel responseInfo = new RecommendationReqCommonModel();
            string agentId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            PaginationFilterCommon request = new PaginationFilterCommon();
            request.Skip = StartIndex;
            request.Take = PageSize;
            var dbResponseInfo = _business.GetClubHomePageRecommendationReqList(agentId, request);
            responseInfo.GetClubHomePageRecommendationReqList = dbResponseInfo.MapObjects<ClubHomePageRecommendationReqListModel>();
            if (!string.IsNullOrEmpty(pageid)) ViewBag.PageId = pageid;
            TempData["OriginalUrl"] = Request.Url.ToString();
            ViewBag.IsBackAllowed = true;
            ViewBag.BackButtonURL = "/RecommendationManagement/Index";
            ViewBag.StartIndex = StartIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.TotalData = dbResponseInfo != null && dbResponseInfo.Any() ? dbResponseInfo[0].TotalRecords : 0;
            return View(responseInfo);
        }

        [HttpGet]
        public ActionResult ManageHomePageRecommendationReq()
        {
            string agentId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            ViewBag.ClubDDL = ApplicationUtilities.LoadDropdownList("SELECTCLUB", agentId, "") as Dictionary<string, string>;
            ViewBag.HostDDLBYClubId = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("HOSTDDLBYCLUBID", agentId, "") as Dictionary<string, string>, null, "--- Select ---");
            TempData["OriginalUrl"] = Request.Url.ToString();
            ViewBag.IsBackAllowed = true;
            ViewBag.BackButtonURL = "/RecommendationManagement/HomePageRecommendationReqList";
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ManageHomePageRecommendationReq(ManageClubHomePageRecommendationReq Model)
        {
            string originalUrl = TempData["OriginalUrl"] as string;
            Uri redirectURL = new Uri(originalUrl);
            string query = redirectURL.Query;
            var queryParams = HttpUtility.ParseQueryString(query);
            string redirectPageId = queryParams["pageid"];
            if (ModelState.IsValid)
            {
                ManageClubHomePageRecommendationReqCommon commonModel = Model.MapObject<ManageClubHomePageRecommendationReqCommon>();
                commonModel.ClubId = Model.ClubId.DecryptParameter();
                commonModel.HostId = Model.HostId.DecryptParameter();
                commonModel.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
                commonModel.ActionIP = ApplicationUtilities.GetIP();
                var dbResponseInfo = _business.ManageHomePageRecommendationReq(commonModel);
                if (dbResponseInfo != null && dbResponseInfo.Code == ResponseCode.Success)
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.SUCCESS,
                        Message = dbResponseInfo.Message ?? "Your request has been sumitted",
                        Title = NotificationMessage.SUCCESS.ToString()

                    });
                    return RedirectToAction("HomePageRecommendationReqList", new { pageid = redirectPageId });
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.WARNING,
                        Message = dbResponseInfo.Message ?? "Something went wrong try again later",
                        Title = NotificationMessage.WARNING.ToString()
                    });
                    return RedirectToAction("HomePageRecommendationReqList", new { pageid = redirectPageId });
                }
            }
            return RedirectToAction("HomePageRecommendationReqList", new { pageid = redirectPageId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteHomePageRequest(string displayid = "", string recommendationholdid = "")
        {
            if (string.IsNullOrEmpty(recommendationholdid) && string.IsNullOrEmpty(displayid))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid request",
                    Title = NotificationMessage.INFORMATION.ToString()

                });
                return Json(JsonRequestBehavior.AllowGet);
            }
            string clubId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            var responseInfo = new CommonDbResponse();
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString(),
            };
            var dbResponseInfo = _business.DeleteHomePageRequest(clubId, displayid, recommendationholdid, commonRequest);
            responseInfo = dbResponseInfo;
            if (responseInfo != null && responseInfo.Code == ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = dbResponseInfo.Message ?? "Recommendation request deleted successfully",
                    Title = NotificationMessage.SUCCESS.ToString()

                });
                return Json(JsonRequestBehavior.AllowGet);
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = dbResponseInfo.Message ?? "Something went wrong try again later",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                return Json(JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region "MANAGE SEARCH PAGE RECOMMENDATION REQUEST"
        [HttpGet]
        public ActionResult SearchPageRecommendationReqList(string pageid = "", int StartIndex = 0, int PageSize = 10)
        {
            RecommendationReqCommonModel responseInfo = new RecommendationReqCommonModel();
            string agentId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            PaginationFilterCommon request = new PaginationFilterCommon();
            request.Skip = StartIndex;
            request.Take = PageSize;
            var dbSearchPageReqList = _business.GetSearchPageRecommendationReqList(agentId, request);
            responseInfo.GetClubSearchPageRecommendationReqList = dbSearchPageReqList.MapObjects<ClubSearchPageRecommendationReqListModel>();
            if (!string.IsNullOrEmpty(pageid)) ViewBag.PageId = pageid;
            TempData["OriginalUrl"] = Request.Url.ToString();
            ViewBag.IsBackAllowed = true;
            ViewBag.BackButtonURL = "/RecommendationManagement/Index";
            ViewBag.StartIndex = StartIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.TotalData = dbSearchPageReqList != null && dbSearchPageReqList.Any() ? dbSearchPageReqList[0].TotalRecords : 0;
            return View(responseInfo);
        }
        [HttpGet]
        public ActionResult ManageSearchPageRecommendationReq()
        {
            string agentId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            ViewBag.ClubDDL = ApplicationUtilities.LoadDropdownList("SELECTCLUB", agentId, "") as Dictionary<string, string>;
            ViewBag.HostDDLBYClubId = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("HOSTDDLBYCLUBID", agentId, "") as Dictionary<string, string>, null, "--- Select ---");
            TempData["OriginalUrl"] = Request.Url.ToString();
            ViewBag.IsBackAllowed = true;
            ViewBag.BackButtonURL = "/RecommendationManagement/SearchPageRecommendationReqList";
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ManageSearchPageRecommendationReq(ManageClubSearchPageRecommendationReq Model, string pageid = "")
        {
            string originalUrl = TempData["OriginalUrl"] as string;
            Uri redirectURL = new Uri(originalUrl);
            string query = redirectURL.Query;
            var queryParams = HttpUtility.ParseQueryString(query);
            string redirectPageId = queryParams["pageid"];
            string PageId = "";
            if (!string.IsNullOrEmpty(pageid)) PageId = pageid;
            if (ModelState.IsValid)
            {
                ManageClubSearchPageRecommendationReqCommon commonModel = Model.MapObject<ManageClubSearchPageRecommendationReqCommon>();
                commonModel.ClubId = Model.ClubId.DecryptParameter();
                commonModel.HostId = Model.HostId.DecryptParameter();
                commonModel.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
                commonModel.ActionIP = ApplicationUtilities.GetIP();
                var dbResponseInfo = _business.ManageSearchPageRecommendationReq(commonModel);
                if (dbResponseInfo != null && dbResponseInfo.Code == ResponseCode.Success)
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.SUCCESS,
                        Message = dbResponseInfo.Message ?? "Your request has been sumitted",
                        Title = NotificationMessage.SUCCESS.ToString()

                    });
                    return RedirectToAction("SearchPageRecommendationReqList", new { pageid = redirectPageId });
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.WARNING,
                        Message = dbResponseInfo.Message ?? "Something went wrong try again later",
                        Title = NotificationMessage.WARNING.ToString()
                    });
                    return RedirectToAction("SearchPageRecommendationReqList", new { pageid = redirectPageId });
                }
            }
            return RedirectToAction("SearchPageRecommendationReqList", new { pageid = redirectPageId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteSearchPageRequest(string displayid = "", string recommendationholdid = "")
        {
            if (string.IsNullOrEmpty(recommendationholdid) && string.IsNullOrEmpty(displayid))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid request",
                    Title = NotificationMessage.INFORMATION.ToString()

                });
                return Json(JsonRequestBehavior.AllowGet);
            }
            var responseInfo = new CommonDbResponse();
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString(),
            };
            string clubid = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            var dbResponseInfo = _business.DeleteSearchPageRequest(clubid, displayid, recommendationholdid, commonRequest);
            responseInfo = dbResponseInfo;
            if (dbResponseInfo == null)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.WARNING,
                    Message = dbResponseInfo.Message ?? "Something went wrong try again later",
                    Title = NotificationMessage.WARNING.ToString()
                });
                return Json(responseInfo.SetMessageInTempData(this));
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = dbResponseInfo.Message ?? "Club Recommendation has been deleted successfully",
                    Title = NotificationMessage.SUCCESS.ToString()
                });

            }
            return Json(responseInfo.SetMessageInTempData(this));
        }
        #endregion

        #region "MANAGE MAIN PAGE REQUEST"
        public ActionResult MainPageRecommendationReqList(int StartIndex = 0, int PageSize = 10)
        {
            RecommendationReqCommonModel responseInfo = new RecommendationReqCommonModel();
            string agentid = ApplicationUtilities.GetSessionValue("AgentID").ToString().DecryptParameter();
            PaginationFilterCommon request = new PaginationFilterCommon();
            request.Skip = StartIndex;
            request.Take = PageSize;
            var dbResponseInfo = _business.GetMainPageRecommendationReqList(agentid, request);
            responseInfo.GetClubMainPageRecommendationReqList = dbResponseInfo.MapObjects<ClubMainPageRecommendationReqListModel>();
            foreach (var item in responseInfo.GetClubMainPageRecommendationReqList)
            {
                item.ClubId = item.ClubId.EncryptParameter();
                item.DisplayId = item.DisplayId.EncryptParameter();
                item.RecommendationHoldId = item.RecommendationHoldId.EncryptParameter();
            }
            ViewBag.StartIndex = StartIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.TotalData = dbResponseInfo != null && dbResponseInfo.Any() ? dbResponseInfo[0].TotalRecords : 0;
            return View(responseInfo);
        }
        [HttpGet]
        public ActionResult ManageMainPageRecommendationReq(string recommendationHoldId = "", string displayId = "", string clubid = "")
        {
            #region "Get All Required DDL"
            var DisplayId = displayId.DecryptParameter();
            string AgentId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            string RecommendationHoldId = recommendationHoldId.DecryptParameter();
            ViewBag.ClubDDL = ApplicationUtilities.LoadDropdownList("SELECTCLUB", AgentId, "") as Dictionary<string, string>;
            if (!string.IsNullOrEmpty(recommendationHoldId))
            {
                ViewBag.HostDDLBYClubId = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("HOSTDDLBYCLUBID", "", "") as Dictionary<string, string>, null);
                ViewBag.DisplayOrderDDL = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("DISPLAYORDERDDL", AgentId, "") as Dictionary<string, string>, null);
            }
            else
            {
                ViewBag.DisplayOrderDDL = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("DISPLAYORDERDDL", "", "") as Dictionary<string, string>, null, "--- Select ---");
                ViewBag.HostDDLBYClubId = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("HOSTDDLBYCLUBID", AgentId, "") as Dictionary<string, string>, null, "--- Select ---");
            }
            #endregion

            #region "Get Recommendation Detail"
            RecommendationReqCommonModel responseInfo = new RecommendationReqCommonModel();
            TempData["OriginalUrl"] = Request.Url.ToString();
            if (!string.IsNullOrEmpty(recommendationHoldId))
            {
                string RecommmendationHoldId = recommendationHoldId.DecryptParameter();
                var dbResponseInfo = _business.GetClubMainPageRecommendationReqDetail(RecommendationHoldId, AgentId, DisplayId);
                responseInfo.GetClubMainPageRecommendationReqDetail = dbResponseInfo.MapObject<ManageClubMainPageRecommendationReq>();
                responseInfo.GetClubMainPageRecommendationReqDetail.RecommendationHoldId = responseInfo.GetClubMainPageRecommendationReqDetail.RecommendationHoldId.EncryptParameter();
                responseInfo.GetClubMainPageRecommendationReqDetail.OrderId = responseInfo.GetClubMainPageRecommendationReqDetail.OrderId.EncryptParameter();
                responseInfo.GetClubMainPageRecommendationReqDetail.ClubId = responseInfo.GetClubMainPageRecommendationReqDetail.ClubId.EncryptParameter();

                var dbHostDetailInfo = _business.GetClubMainPageHostRecommedationListDetail(AgentId, RecommendationHoldId, DisplayId);
                responseInfo.GetClubMainPageHostRecommedationListDetail = dbHostDetailInfo.MapObjects<ClubMainPageHostRecommendationReqModel>();
                foreach (var item in responseInfo.GetClubMainPageHostRecommedationListDetail)
                {
                    item.HostId = item.HostId.EncryptParameter();
                    item.HostDisplayOrderId = item.HostDisplayOrderId.EncryptParameter();
                }
                ViewBag.RecommendationId = recommendationHoldId;
                ViewBag.IsBackAllowed = true;
                ViewBag.BackButtonURL = "/RecommendationManagement/MainPageRecommendationReqList";
                return View(responseInfo);
            }
            else
            {
                ViewBag.IsBackAllowed = true;
                ViewBag.BackButtonURL = "/RecommendationManagement/MainPageRecommendationReqList";
                return View(responseInfo);
            }
            #endregion 
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ManageMainPageRecommendationReq(ManageClubMainPageRecommendationReq Model, string ClubId = "", string DisplayOrder = "", string[] HostDDLByClubId = null, string[] DisplayOrderDDL = null, string recommendationHoldId = "", string displayId = "", string pageid = "")
        {
            string originalUrl = TempData["OriginalUrl"] as string;
            Uri redirectURL = new Uri(originalUrl);
            string query = redirectURL.Query;
            var queryParams = HttpUtility.ParseQueryString(query);
            if (ModelState.IsValid)
            {
                ManageClubMainPageRecommendationReqCommon commonModel = Model.MapObject<ManageClubMainPageRecommendationReqCommon>();
                commonModel.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
                commonModel.ActionIP = ApplicationUtilities.GetIP();
                commonModel.ClubId = ClubId.DecryptParameter();
                commonModel.OrderId = DisplayOrder.DecryptParameter();
                commonModel.RecommendationHoldId = recommendationHoldId.DecryptParameter();
                commonModel.DisplayId = displayId;
                var hostDDLByClubId = "";
                var displayOrderDDL = "";

                if (HostDDLByClubId != null)
                {
                    foreach (var item in HostDDLByClubId)
                    {
                        hostDDLByClubId = hostDDLByClubId + "," + item.DecryptParameter();
                    }
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.WARNING,
                        Message = "Please select Host",
                        Title = NotificationMessage.WARNING.ToString()
                    });
                }
                if (!string.IsNullOrEmpty(hostDDLByClubId)) hostDDLByClubId = hostDDLByClubId.Substring(1);
                if (DisplayOrderDDL != null)
                {
                    foreach (var item in DisplayOrderDDL)
                    {
                        displayOrderDDL = displayOrderDDL + "," + item.DecryptParameter();
                    }
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.WARNING,
                        Message = "Please select Display order",
                        Title = NotificationMessage.WARNING.ToString()
                    });
                }
                if (!string.IsNullOrEmpty(displayOrderDDL)) displayOrderDDL = displayOrderDDL.Substring(1);
                var HostRecommendationCSValue = "";
                if (!string.IsNullOrEmpty(hostDDLByClubId) && !string.IsNullOrEmpty(displayOrderDDL))
                {
                    var hostList = hostDDLByClubId.Split(',');
                    var displayList = displayOrderDDL.Split(',');
                    for (int i = 0; i < Math.Min(hostList.Length, displayList.Length); i++)
                    {
                        HostRecommendationCSValue += $"{hostList[i]}:{displayList[i]},";
                    }
                    HostRecommendationCSValue = HostRecommendationCSValue.TrimEnd(',');
                }
                if (!string.IsNullOrEmpty(HostRecommendationCSValue)) commonModel.HostRecommendationCSValue = HostRecommendationCSValue;
                if (!string.IsNullOrEmpty(commonModel.HostRecommendationCSValue))
                {
                    var dbResponseInfo = _business.ManageMainPageRecommendationReq(commonModel);
                    if (dbResponseInfo != null && dbResponseInfo.Code == ResponseCode.Success)
                    {
                        AddNotificationMessage(new NotificationModel()
                        {
                            NotificationType = NotificationMessage.SUCCESS,
                            Message = dbResponseInfo.Message ?? "Club Recommendation assigned successfully",
                            Title = NotificationMessage.SUCCESS.ToString()

                        });
                        return RedirectToAction("MainPageRecommendationReqList", new { pageid = pageid });
                    }
                    else
                    {
                        AddNotificationMessage(new NotificationModel()
                        {
                            NotificationType = NotificationMessage.WARNING,
                            Message = dbResponseInfo.Message ?? "Something went wrong try again later",
                            Title = NotificationMessage.WARNING.ToString()
                        });
                        return RedirectToAction("MainPageRecommendationReqList", new { pageid = pageid });
                    }
                }
            }
            return RedirectToAction("MainPageRecommendationReqList", new { pageid = pageid });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteMainPageRequest(string displayid = "", string recommendationHoldId = "")
        {

            string ClubId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            string RecommendationHoldId = "";
            string DisplayId = "";
            if (!string.IsNullOrEmpty(displayid) && !string.IsNullOrEmpty(recommendationHoldId))
            {
                RecommendationHoldId = recommendationHoldId.DecryptParameter();
                DisplayId = displayid.DecryptParameter();
            }
            if (string.IsNullOrEmpty(displayid) || string.IsNullOrEmpty(recommendationHoldId))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid request",
                    Title = NotificationMessage.INFORMATION.ToString()

                });
                return Json(JsonRequestBehavior.AllowGet);
            }
            var responseInfo = new CommonDbResponse();
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString(),
            };
            var dbResponseInfo = _business.DeleteMainPageRequest(ClubId, DisplayId, RecommendationHoldId, commonRequest);
            responseInfo = dbResponseInfo;
            if (dbResponseInfo != null && dbResponseInfo.Code == ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = dbResponseInfo.Message ?? "Club Recommendation has been deleted successfully",
                    Title = NotificationMessage.SUCCESS.ToString()
                });
                return Json(responseInfo.SetMessageInTempData(this));
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = dbResponseInfo.Message ?? "Something went wrong try again later",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                return Json(responseInfo.SetMessageInTempData(this));
            }
        }
        #endregion

        #region "HOST LIST PAGE"
        public ActionResult HostListPageView(string recommendationHoldId = "", string displayId = "")
        {
            RecommendationReqCommonModel responseInfo = new RecommendationReqCommonModel();
            string AgentId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            string RecommendationHoldId = recommendationHoldId.DecryptParameter();
            string DisplayId = displayId.DecryptParameter();
            var dbResponseInfo = _business.GetHostListDetail(RecommendationHoldId, AgentId, DisplayId);
            responseInfo.GetHostListDetail = dbResponseInfo.MapObjects<HostListDetailModel>();
            foreach (var item in responseInfo.GetHostListDetail)
            {
                item.RecommendationHoldId = item.RecommendationHoldId.EncryptParameter();
                item.RecommendationHostHoldId = item.RecommendationHostHoldId.EncryptParameter();
                item.ClubId = item.ClubId.EncryptParameter();
                item.HostId = item.HostId.EncryptParameter();
            }
            ViewBag.IsBackAllowed = true;
            ViewBag.BackButtonURL = "/RecommendationManagement/MainPageRecommendationReqList";
            return View(responseInfo);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteSelectedHost(string recommendationhostid = "", string recommendationhostholdid = "", string hostid = "")
        {
            string RecommendationHostId = "";
            string ClubId = "";
            string RecommendationHostHoldId = "";
            string HostId = "";
            if (!string.IsNullOrEmpty(hostid)) HostId = hostid.DecryptParameter();
            if (!string.IsNullOrEmpty(recommendationhostid)) RecommendationHostId = recommendationhostid.DecryptParameter();
            if (!string.IsNullOrEmpty(recommendationhostholdid)) RecommendationHostHoldId = recommendationhostholdid.DecryptParameter();
            ClubId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            var responseInfo = new CommonDbResponse();
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString(),
            };
            var dbResponseInfo = _business.DeleteSelectedHost(ClubId, RecommendationHostId, RecommendationHostHoldId, HostId, commonRequest);
            responseInfo = dbResponseInfo;
            if (dbResponseInfo != null && dbResponseInfo.Code == ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = dbResponseInfo.Message ?? "Host has been deleted successfully",
                    Title = NotificationMessage.SUCCESS.ToString()
                });
            }
            else if (dbResponseInfo.Code == ResponseCode.Failed)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = dbResponseInfo.Message ?? "Invalid Detail Request",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                return Json(responseInfo.SetMessageInTempData(this));
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.WARNING,
                    Message = dbResponseInfo.Message ?? "Something went wrong try again later",
                    Title = NotificationMessage.WARNING.ToString()
                });
                return Json(responseInfo.SetMessageInTempData(this));
            }
            return Json(responseInfo.SetMessageInTempData(this));
        }
        #endregion
    }
}