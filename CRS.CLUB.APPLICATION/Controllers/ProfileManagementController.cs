using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.UserProfileManagement;
using CRS.CLUB.BUSINESS.ProfileManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ProfileManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Google.Apis.Requests.BatchRequest;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class ProfileManagementController : BaseController
    {
        private readonly IProfileManagementBusiness _business;

        public ProfileManagementController(IProfileManagementBusiness business) => this._business = business;
        public ActionResult Index()
        {
            Session["CurrentURL"] = "/ProfileManagement/Index";
            var common = new UserProfileCommon()
            {
                AgentId = ApplicationUtilities.GetSessionValue("AgentId")?.ToString()?.DecryptParameter(),
                ActionUser = ApplicationUtilities.GetSessionValue("UserName")?.ToString(),
            };

            var dbResp = _business.GetUserProfile(common);
            if (dbResp.Code == ResponseCode.Success)
            {
                var viewModel = dbResp.MapObject<UserProfileModel>();
                string FileLocationPath = "";
                if (ConfigurationManager.AppSettings["Phase"] != null
                   && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                    FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
                viewModel.ProfilePicture = FileLocationPath + viewModel.ProfilePicture;
                viewModel.CoverPicture = FileLocationPath + viewModel.CoverPicture;
                ViewBag.PrefectureDDL = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("PREFECTUREDDL") as Dictionary<string, string>, viewModel.InputPrefecture?.EncryptParameter(), "--- 選択 ---");
                ViewBag.LocationDDL = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("LOCATIONDDL") as Dictionary<string, string>, viewModel.InputCity?.EncryptParameter(), "--- 選択 ---");
                ViewBag.PrefectureKey = viewModel.InputPrefecture?.EncryptParameter();
                ViewBag.CityKey = viewModel.InputCity?.EncryptParameter();
                return View(viewModel);
            }
            AddNotificationMessage(new NotificationModel()
            {
                Message = !string.IsNullOrEmpty(dbResp.Message) ? dbResp.Message : "Something went wrong",
                NotificationType = NotificationMessage.INFORMATION,
                Title = NotificationMessage.INFORMATION.ToString(),
            });
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(UserProfileModel userProfile, HttpPostedFileBase profilePicture, HttpPostedFileBase coverPicture, string InputPrefectureId, string InputPlanId, string InputCityId)
        {
            Session["CurrentURL"] = "/ProfileManagement/Index";
            ViewBag.PrefectureDDL = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("PREFECTUREDDL") as Dictionary<string, string>, userProfile.InputPrefecture, "--- 選択 ---");
            ViewBag.LocationDDL = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("LOCATIONDDL") as Dictionary<string, string>, userProfile.InputCity, "--- 選択 ---");
            if (!ModelState.IsValid)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    Message = "Required fields are missing",
                    NotificationType = NotificationMessage.ERROR,
                    Title = NotificationMessage.ERROR.ToString(),
                });
                return View(userProfile);
            }
            string FileLocationPath = "/Content/userupload/ClubManagement/";
            string FileLocation = FileLocationPath;
            string VirtualPath = "";
            if (ConfigurationManager.AppSettings["Phase"] != null
                && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
            {
                FileLocation = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
                VirtualPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString();
            }

            string profilePicturePath = "";
            string coverPicturePath = "";
            var allowedContentType = new[] { "image/png", "image/jpeg", "image/jpg", "image/heif" };
            string dateTime = "";
            if (profilePicture != null)
            {
                var contentType = profilePicture.ContentType;
                var ext = Path.GetExtension(profilePicture.FileName);
                if (allowedContentType.Contains(contentType.ToLower()))
                {
                    dateTime = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string fileName = "ProfileImage_" + dateTime + ext.ToLower();
                    profilePicturePath = Path.Combine(Server.MapPath(FileLocation), fileName);
                    userProfile.ProfilePicture = FileLocationPath + fileName;
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.INFORMATION,
                        Message = "Profile image must be in jpeg, jpg, heif or png format",
                        Title = NotificationMessage.INFORMATION.ToString()
                    });
                    return View(userProfile);
                }
            }
            else if (string.IsNullOrEmpty(userProfile.ProfilePicture))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Profile picture required",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                return View(userProfile);
            }

            if (coverPicture != null)
            {
                var contentType = coverPicture.ContentType;
                var ext = Path.GetExtension(coverPicture.FileName);
                if (allowedContentType.Contains(contentType.ToLower()))
                {
                    dateTime = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string fileName = "CoverImage_" + dateTime + ext.ToLower();
                    coverPicturePath = Path.Combine(Server.MapPath(FileLocation), fileName);
                    userProfile.CoverPicture = FileLocationPath + fileName;
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.INFORMATION,
                        Message = "Cover image must be in jpeg, jpg, heif or png format",
                        Title = NotificationMessage.INFORMATION.ToString()
                    });
                    return View(userProfile);
                }
            }
            else if (string.IsNullOrEmpty(userProfile.CoverPicture))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Cover image required",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                return View(userProfile);
            }
            var dbRequestCommon = userProfile.MapObject<UserProfileCommon>();
            dbRequestCommon.AgentId = ApplicationUtilities.GetSessionValue("AgentId").ToString()?.DecryptParameter();
            dbRequestCommon.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
            dbRequestCommon.ActionIP = ApplicationUtilities.GetIP();
            dbRequestCommon.InputCity = InputCityId?.DecryptParameter();
            dbRequestCommon.InputPrefecture = InputPrefectureId?.DecryptParameter();
            dbRequestCommon.ProfilePicture = profilePicture != null ? dbRequestCommon.ProfilePicture : null;
            dbRequestCommon.CoverPicture = coverPicture != null ? dbRequestCommon.CoverPicture : null;
            var dbResponse = _business.UpdateUserProfile(dbRequestCommon);
            if (dbResponse.Code == ResponseCode.Success)
            {
                if (profilePicture != null)
                {
                    Session["LogoImage"] = VirtualPath + dbRequestCommon.ProfilePicture;
                    ApplicationUtilities.ResizeImage(profilePicture, profilePicturePath);
                }
                if (coverPicture != null) ApplicationUtilities.ResizeImage(coverPicture, coverPicturePath);
                AddNotificationMessage(new NotificationModel()
                {
                    Message = dbResponse.Message ?? "Success",
                    NotificationType = NotificationMessage.SUCCESS,
                    Title = NotificationMessage.SUCCESS.ToString(),
                });
                return RedirectToAction("Dashboard", "Home");
            }
            AddNotificationMessage(new NotificationModel()
            {
                Message = dbResponse.Message ?? "Failed",
                NotificationType = NotificationMessage.INFORMATION,
                Title = NotificationMessage.INFORMATION.ToString(),
            });
            return View(userProfile);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            Session["CurrentUrl"] = "/ProfileManagement/ChangePassword";
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel changePassword)
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
                return View(changePassword);
            }

            var passwordCommon = changePassword.MapObject<PasswordCommon>();
            passwordCommon.UserId = ApplicationUtilities.GetSessionValue("UserId")?.ToString()?.DecryptParameter();
            passwordCommon.IPAddress = ApplicationUtilities.GetIP().ToString();
            passwordCommon.BrowserInfo = ApplicationUtilities.GetBrowserInfo().ToString();
            passwordCommon.ActionUser = ApplicationUtilities.GetSessionValue("UserName").ToString();
            passwordCommon.Session = Session.SessionID;

            var dbResp = _business.ChangePassword(passwordCommon);

            if (dbResp.Code != ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = dbResp.Message,
                    Title = NotificationMessage.ERROR.ToString(),
                });

                return View();
            }

            AddNotificationMessage(new NotificationModel()
            {
                NotificationType = NotificationMessage.SUCCESS,
                Message = dbResp.Message,
                Title = NotificationMessage.SUCCESS.ToString(),
            });
            return RedirectToAction("LogOff", "Home");
        }
    }
}