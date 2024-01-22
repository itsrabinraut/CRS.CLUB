using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.NotificationManagement;
using CRS.CLUB.BUSINESS.NotificationManagement;
using CRS.CLUB.SHARED.NotificationManagement;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class NotificationManagementController : Controller
    {
        private readonly INotificationManagementBusiness _buss;
        public NotificationManagementController(INotificationManagementBusiness buss) => _buss = buss;
        [HttpGet]
        public ActionResult ViewAllNotifications(string NotificationId)
        {
            var requestCommon = new ManageNotificationCommon()
            {
                AgentId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter(),
                NotificationId = !string.IsNullOrEmpty(NotificationId) ? NotificationId : null,
            };
            var dbResponse = _buss.GetAllNotification(requestCommon);
            var response = dbResponse.MapObjects<NotificationDetailModel>();
            return View(response);
        }
    }
}