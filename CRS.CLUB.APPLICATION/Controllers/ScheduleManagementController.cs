using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.ScheduleManagement;
using CRS.CLUB.BUSINESS.ScheduleManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ScheduleManagement;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{

    public class ScheduleManagementController : BaseController
    {
        private readonly IScheduleManagementBusiness _scheduleBuss;
        public ScheduleManagementController(IScheduleManagementBusiness scheduleBuss)
        {
            _scheduleBuss = scheduleBuss;
        }
        [HttpPost, ValidateAntiForgeryToken, OverrideActionFilters]
        public JsonResult ManageSchedule(ClubScheduleModel Request)
        {
            var redirectToUrl = string.Empty;
            var ScheduleId = !string.IsNullOrEmpty(Request.ScheduleId) ? Request.ScheduleId.DecryptParameter() : null;
            var ClubSchedule = !string.IsNullOrEmpty(Request.ClubSchedule) ? Request.ClubSchedule.DecryptParameter() : null;
            if (!string.IsNullOrEmpty(Request.ScheduleId) && string.IsNullOrEmpty(ScheduleId))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid request",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                return Json(new { redirectToUrl });
            }
            if (string.IsNullOrEmpty(ClubSchedule))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid request",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                return Json(new { redirectToUrl });
            }
            var dbRequest = Request.MapObject<ManageScheduleCommon>();
            dbRequest.ScheduleId = ScheduleId;
            dbRequest.ClubSchedule = ClubSchedule;
            dbRequest.ClubId = ApplicationUtilities.GetSessionValue("AgentId").ToString()?.DecryptParameter();
            dbRequest.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
            dbRequest.ActionIP = ApplicationUtilities.GetIP();
            var dbResponse = _scheduleBuss.ManageSchedule(dbRequest);
            if (dbResponse != null && dbResponse.Code == ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = dbResponse.Message ?? "Success",
                    Title = NotificationMessage.SUCCESS.ToString()
                });
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = dbResponse.Message ?? "Failed",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
            }
            return Json(new { redirectToUrl });
        }

        [HttpGet]
        public JsonResult GetScheduleSchedule()
        {
            var Response = new List<ClubScheduleModel>();
            var ClubId = ApplicationUtilities.GetSessionValue("AgentId").ToString()?.DecryptParameter();
            var dbResponse = _scheduleBuss.GetClubSchedule(ClubId);
            if (dbResponse != null && dbResponse.Count > 0) Response = dbResponse.MapObjects<ClubScheduleModel>();
            return Json(Response);
        }
    }
}