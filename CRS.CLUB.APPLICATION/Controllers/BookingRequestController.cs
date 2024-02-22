using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.BookingRequest;
using CRS.CLUB.BUSINESS.BookingRequest;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.BookingRequest;
using CRS.CLUB.SHARED.PaginationManagement;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult BookingRequestList(CommonBookingRequestList Model, int StartIndex = 0, int PageSize = 10, int StartIndex2 = 0, int PageSize2 = 10, int StartIndex3 = 0, int PageSize3 = 10)
        {
            Session["CurrentURL"] = "/BookingRequest/BookingRequestList";
            string AgentId = Session["AgentId"]?.ToString().DecryptParameter();
            CommonBookingRequestList responseInfo = new CommonBookingRequestList();
            PaginationFilterCommon AllRequest = new PaginationFilterCommon()
            {
                Skip = StartIndex,
                Take = PageSize,
            };
            PaginationFilterCommon ApprovedRequest = new PaginationFilterCommon()
            {
                Skip = StartIndex2,
                Take = PageSize2,
            };
            PaginationFilterCommon PendingRequest = new PaginationFilterCommon()
            {
                Skip = StartIndex3,
                Take = PageSize3,
            };
            var request = Model.MapObject<SearchFilterCommon>();
            var dbResponse = _business.GetAllBookingRequestList(AgentId, request, AllRequest);
            responseInfo.GetAllBookingRequestLists = dbResponse.MapObjects<AllBookingRequestList>();
            List<PendingBookingRequestList> pendinglistInfo = new List<PendingBookingRequestList>();
            var dbPendingResponse = _business.GetPendingBookingList(AgentId, request, PendingRequest);
            responseInfo.GetPendingBookingRequestLists = dbPendingResponse.MapObjects<PendingBookingRequestList>();
            List<ApprovedBookingRequestList> approvedBookingRequestLists = new List<ApprovedBookingRequestList>();
            var dbapprovedResponse = _business.GetApprovedBookingList(AgentId, request, ApprovedRequest);
            responseInfo.GetApprovedBookingRequestLists = dbapprovedResponse.MapObjects<ApprovedBookingRequestList>();
            var dbResponseInfo = _business.GetBookingRequestAnalytics(AgentId);
            responseInfo.GetBookingRequestAnalyticData = dbResponseInfo.MapObject<BookingRequestAnalyticsModel>();
            var dbClubTimeList = _business.GetClubTimeList(AgentId);
            responseInfo.GetClubTimeList = dbClubTimeList.MapObjects<ClubTimeInfoModel>();           

            ViewBag.StartIndex1 = StartIndex;
            ViewBag.PageSize1 = PageSize;
            ViewBag.TotalData1 = dbResponse != null && dbResponse.Any() ? dbResponse[0].TotalRecords : 0;

            ViewBag.StartIndex2 = StartIndex2;
            ViewBag.PageSize2 = PageSize2;
            ViewBag.TotalData2 = dbapprovedResponse != null && dbapprovedResponse.Any() ? dbapprovedResponse[0].TotalRecords : 0;

            ViewBag.StartIndex3 = StartIndex3;
            ViewBag.PageSize3 = PageSize3;
            ViewBag.TotalData3 = dbPendingResponse != null && dbPendingResponse.Any() ? dbPendingResponse[0].TotalRecords : 0;

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