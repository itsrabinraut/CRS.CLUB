using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.PaymentManagement;
using CRS.CLUB.BUSINESS.PaymentManagement;
using CRS.CLUB.SHARED;
using System.Configuration;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{

    public class PaymentManagementController : BaseController
    {
        private readonly IPaymentManagementBusiness _business;

        public PaymentManagementController(IPaymentManagementBusiness business) => this._business = business;

        public ActionResult Index(string searchText = null, string Date = null)
        {
            Session["CurrentURL"] = "/PaymentManagement/Index";
            string clubId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            var paymentManagementModel = new PaymentManagementModel();
            var overview = _business.GetPaymentOverview(clubId);
            var paymentLog = _business.GetPaymentLog(searchText, clubId, Date);

            paymentManagementModel.PaymentOverviewModel = overview.MapObject<PaymentOverviewModel>();
            paymentManagementModel.PaymentLogModels = paymentLog.MapObjects<PaymentLogModel>();
            ViewBag.SearchText = searchText;
            return View(paymentManagementModel);
        }
        [HttpGet]
        public ActionResult PaymentLedger(string clubId, string searchText = "", string Date = null)
        {
            string FileLocationPath = "";
            if (ConfigurationManager.AppSettings["Phase"] != null
               && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
            if (string.IsNullOrWhiteSpace(clubId))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    Title = NotificationMessage.INFORMATION.ToString(),
                    Message = "Club Id invalid",
                    NotificationType = NotificationMessage.INFORMATION
                });
                return RedirectToAction("Index", new { Date = Date });
            }
            var paymentLedgerCommon = _business.GetPaymentLedgerDetail(searchText, clubId, Date);
            var paymentLedgerModel = paymentLedgerCommon.MapObjects<PaymentLedgerModel>();
            foreach (var item in paymentLedgerModel)
            {
                item.ClubId = item.ClubId.EncryptParameter();
                item.CustomerImage = FileLocationPath + item.CustomerImage;
            }
            return View(paymentLedgerModel);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UpdatePaymentLogs(string Remarks = "", string ReservationId = "")
        {
            Session["CurrentURL"] = "/PaymentManagement/Index";
            if (string.IsNullOrEmpty(Remarks)) return Json(new { Code = 1, Message = "Invalid Data." });
            var request = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString(),
                AgentId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter(),
            };
            var dbResponse = _business.UpdatePaymentLogs(Remarks, request, ReservationId);
            dbResponse.Extra1 = "true";
            if (dbResponse != null)
            {
                if (dbResponse.Code == ResponseCode.Success)
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        Message = dbResponse.Message ?? "Payment Log Updated",
                        NotificationType = NotificationMessage.SUCCESS,
                        Title = NotificationMessage.SUCCESS.ToString(),
                    });
                    return RedirectToAction("Index", "PaymentManagement");
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        Message = "Invalid details",
                        NotificationType = NotificationMessage.WARNING,
                        Title = NotificationMessage.WARNING.ToString(),
                    });
                    return RedirectToAction("Index", "PaymentManagement");
                }
            }
            return Json(dbResponse.SetMessageInTempData(this));
        }
    }
}