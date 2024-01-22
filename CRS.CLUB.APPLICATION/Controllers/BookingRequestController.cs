using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.BookingRequest;
using CRS.CLUB.BUSINESS.BookingRequest;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.BookingRequest;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class BookingRequestController : BaseController
    {
        private readonly IBookingRequestBusiness _business;
        public BookingRequestController(IBookingRequestBusiness business)
        {
            _business = business;
        }
        [HttpGet]
        public ActionResult BookingRequestList(CommonBookingRequestList Model)
        {
            Session["CurrentURL"] = "/BookingRequest/BookingRequestList";
            string AgentId = Session["AgentId"]?.ToString().DecryptParameter();
            CommonBookingRequestList responseInfo = new CommonBookingRequestList();
            var request = Model.MapObject<SearchFilterCommon>();
            var dbResponse = _business.GetAllBookingRequestList(AgentId, request);
            responseInfo.GetAllBookingRequestLists = dbResponse.MapObjects<AllBookingRequestList>();
            List<PendingBookingRequestList> pendinglistInfo = new List<PendingBookingRequestList>();
            var dbPendingResponse = _business.GetPendingBookingList(AgentId, request);
            responseInfo.GetPendingBookingRequestLists = dbPendingResponse.MapObjects<PendingBookingRequestList>();
            List<ApprovedBookingRequestList> approvedBookingRequestLists = new List<ApprovedBookingRequestList>();
            var dbapprovedResponse = _business.GetApprovedBookingList(AgentId, request);
            responseInfo.GetApprovedBookingRequestLists = dbapprovedResponse.MapObjects<ApprovedBookingRequestList>();
            var dbResponseInfo = _business.GetBookingRequestAnalytics(AgentId);
            responseInfo.GetBookingRequestAnalyticData = dbResponseInfo.MapObject<BookingRequestAnalyticsModel>();
            var dbClubTimeList = _business.GetClubTimeList(AgentId);
            responseInfo.GetClubTimeList = dbClubTimeList.MapObjects<ClubTimeInfoModel>();
            foreach (var item in responseInfo.GetAllBookingRequestLists)
            {
                item.VisitedDate = DateTime.Parse(item.VisitedDate).ToString("yyyy-MM-dd");
            }
            foreach (var item in responseInfo.GetApprovedBookingRequestLists)
            {
                item.VisitedDate = DateTime.Parse(item.VisitedDate).ToString("yyyy-MM-dd");
            }
            foreach (var item in responseInfo.GetPendingBookingRequestLists)
            {
                item.VisitedDate = DateTime.Parse(item.VisitedDate).ToString("yyyy-MM-dd");
            }
            TempData["OriginalUrl"] = Request.Url.ToString();
            string originalUrl = TempData["OriginalUrl"] as string;
            Uri redirectURL = new Uri(originalUrl);
            string path = redirectURL.AbsolutePath;
            ViewBag.CurrentUrl = path;
            string query = redirectURL.Query;
            var queryParams = HttpUtility.ParseQueryString(query);
            string offset = queryParams["Offset"];
            ViewBag.Offset = offset;
            ViewBag.Limit = Model.Limit ?? "10";

            ViewBag.SearchText = Model.SearchFilter;
            ViewBag.FromDate = Model.FromDate;
            ViewBag.ToDate = Model.ToDate;
            return View(responseInfo);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ApproveBookingRequest(string id = "")
        {
            Session["CurrentURL"] = "/BookingRequest/BookingRequestList";
            if (string.IsNullOrEmpty(id)) return Json(new { Code = 1, Message = "Invalid Data." });
            string newId = id;
            string actionUser = Session["username"]?.ToString();
            string actionIp = ApplicationUtilities.GetIP();
            string actionPlatform = "Club";

            var dbResponse = _business.ApproveBookingRequest(newId, actionUser, actionIp, actionPlatform);
            dbResponse.Extra1 = "true";
            if (dbResponse != null)
            {
                if (dbResponse.Code == ResponseCode.Success)
                {
                    if (string.IsNullOrEmpty(newId))
                    {
                        AddNotificationMessage(new NotificationModel()
                        {
                            Message = "Invalid details",
                            NotificationType = NotificationMessage.WARNING,
                            Title = NotificationMessage.WARNING.ToString(),
                        });
                        return RedirectToAction("BookingRequestList", "BookingRequest");
                    }
                    AddNotificationMessage(new NotificationModel()
                    {
                        Message = dbResponse.Message ?? "Booking Request accepted",
                        NotificationType = NotificationMessage.SUCCESS,
                        Title = NotificationMessage.SUCCESS.ToString(),
                    });
                }
            }
            return Json(dbResponse.SetMessageInTempData(this));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RejectBookingRequest(string id = "")
        {
            Session["CurrentURL"] = "/BookingRequest/BookingRequestList";
            if (string.IsNullOrEmpty(id)) return Json(new { Code = 1, Message = "Invalid Data." });
            string newId = id;
            string actionUser = Session["username"]?.ToString();
            string actionIp = ApplicationUtilities.GetIP();
            string actionPlatform = "Club";

            var dbResponse = _business.RejectBookingRequest(newId, actionUser, actionIp, actionPlatform);
            dbResponse.Extra1 = "true";
            if (dbResponse != null)
            {
                if (dbResponse.Code == ResponseCode.Success)
                {
                    if (string.IsNullOrEmpty(newId))
                    {
                        AddNotificationMessage(new NotificationModel()
                        {
                            Message = "Invalid details",
                            NotificationType = NotificationMessage.WARNING,
                            Title = NotificationMessage.WARNING.ToString(),
                        });
                        return RedirectToAction("BookingRequestList", "BookingRequest");
                    }
                    AddNotificationMessage(new NotificationModel()
                    {
                        Message = dbResponse.Message ?? "Booking Request rejected",
                        NotificationType = NotificationMessage.ERROR,
                        Title = "REJECTED",
                    });
                }
            }
            return Json(dbResponse.SetMessageInTempData(this));
        }
    }
}