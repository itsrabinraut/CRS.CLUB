using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.ReservationValidationManagement;
using CRS.CLUB.BUSINESS.ReservationValidationManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReservationValidationManagement;
using System.Configuration;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class ReservationValidationManagementController : BaseController
    {
        private readonly IReservationValidationManagementBusiness _reservationValidationManagementBuss;
        public ReservationValidationManagementController(IReservationValidationManagementBusiness reservationValidationManagementBuss) => _reservationValidationManagementBuss = reservationValidationManagementBuss;

        [HttpGet]
        public ActionResult CustomerValidation()
        {
            Session["CurrentURL"] = "/ReservationValidationManagement/CustomerValidation";
            return View();
        }

        [HttpGet]
        public ActionResult CustomerValidationConfirmation(string OTPCode)
        {
            Session["CurrentURL"] = "/ReservationValidationManagement/CustomerValidation";
            string FileLocationPath = "";
            if (!string.IsNullOrEmpty(OTPCode))
            {
                if (ConfigurationManager.AppSettings["Phase"] != null
                  && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                    FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString();
                var actionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
                var dbResponse = _reservationValidationManagementBuss.GetReservationDetailViaOTP(OTPCode, actionUser);
                if (dbResponse != null)
                {
                    if (dbResponse.Code == ResponseCode.Success)
                    {
                        var Model = dbResponse.MapObject<ReservationDetailViaOTPModel>();
                        Model.OTPCode = OTPCode;
                        if (string.IsNullOrEmpty(Model.ReservationId))
                        {
                            AddNotificationMessage(new NotificationModel()
                            {
                                Message = "Invalid detail",
                                NotificationType = NotificationMessage.INFORMATION,
                                Title = NotificationMessage.INFORMATION.ToString(),
                            });
                            return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
                        }
                        var HostDBResponse = _reservationValidationManagementBuss.GetReservationHostDetail(Model.ReservationId);
                        if (HostDBResponse.Count > 0)
                        {
                            Model.ReservationHostListModel = HostDBResponse.MapObjects<ReservationHostDetail>();
                            Model.ReservationHostListModel.ForEach(x => x.HostId = x.HostId.EncryptParameter());
                            Model.ReservationHostListModel.ForEach(x => x.HostLogo = FileLocationPath + x.HostLogo);
                        }
                        Model.ReservationId = Model.ReservationId.EncryptParameter();
                        return View(Model);
                    }
                    AddNotificationMessage(new NotificationModel()
                    {
                        Message = dbResponse.Message ?? "Invalid",
                        NotificationType = NotificationMessage.INFORMATION,
                        Title = NotificationMessage.INFORMATION.ToString(),
                    });
                    return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
                }
            }
            AddNotificationMessage(new NotificationModel()
            {
                Message = "Invalid detail",
                NotificationType = NotificationMessage.INFORMATION,
                Title = NotificationMessage.INFORMATION.ToString(),
            });
            return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CustomerValidationConfirmation(ManageReservationOTPStatusModel Model)
        {
            var ReservationId = !string.IsNullOrEmpty(Model.ReservationId) ? Model.ReservationId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(ReservationId) || string.IsNullOrEmpty(Model.OTPCode))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    Message = "Invalid detail",
                    NotificationType = NotificationMessage.INFORMATION,
                    Title = NotificationMessage.INFORMATION.ToString(),
                });
                return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
            }
            var dbRequestModel = new ManageReservationOTPStatusCommon()
            {
                OTPCode = Model.OTPCode,
                ReservationId = ReservationId,
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString(),
                ActionIP = ApplicationUtilities.GetIP()
            };
            var dbResponse = _reservationValidationManagementBuss.ManageReservationOTPStatus(dbRequestModel);
            if (dbResponse != null && dbResponse.Code == ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    Message = dbResponse.Message ?? "Success",
                    NotificationType = NotificationMessage.SUCCESS,
                    Title = NotificationMessage.SUCCESS.ToString(),
                });
                return RedirectToAction("CustomerValidationConfirmation", "ReservationValidationManagement", new { OTPCode = Model.OTPCode });
            }
            AddNotificationMessage(new NotificationModel()
            {
                Message = dbResponse.Message ?? "Failed",
                NotificationType = NotificationMessage.ERROR,
                Title = NotificationMessage.ERROR.ToString(),
            });
            return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
        }
    }
}