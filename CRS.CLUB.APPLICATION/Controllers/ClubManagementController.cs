using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.ClubManagement;
using CRS.CLUB.APPLICATION.Models.ScheduleManagement;
using CRS.CLUB.BUSINESS.ClubManagement;
using CRS.CLUB.BUSINESS.CommonManagement;
using CRS.CLUB.BUSINESS.ScheduleManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ClubManagement;
using DocumentFormat.OpenXml;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{

    public class ClubManagementController : BaseController
    {
        private readonly IClubManagementBussiness _buss;
        private readonly ICommonManagementBusiness _common;
        private readonly IScheduleManagementBusiness _scheduleBuss;

        public ClubManagementController(IClubManagementBussiness buss, ICommonManagementBusiness comm, IScheduleManagementBusiness scheduleBuss)
        {
            _buss = buss;
            _common = comm;
            _scheduleBuss = scheduleBuss;
        }
        public ActionResult Index(string SearchFilter = "", string HostSearchFilter = "", string GallerySearchFilter = "")
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            var AgentId = Session["AgentID"]?.ToString().DecryptParameter();
            string FileLocationPath = "";

            var response = _buss.GetClubImages(AgentId, GallerySearchFilter);
            var responseHost = _buss.GetHostList(AgentId, HostSearchFilter);
            var responseEvent = _buss.GetEventList(AgentId, SearchFilter);
            ClubManagementModel clubMgmtModel = new ClubManagementModel();

            #region Dropdown List
            ViewBag.LiquoreStrength = ApplicationUtilities.SetDDLValue(LoadDropdownList("liqStrength", "9") as Dictionary<string, string>, "", "--- 選択 ---");
            ViewBag.BloodType = ApplicationUtilities.SetDDLValue(LoadDropdownList("bloodtype", "18") as Dictionary<string, string>, "", "--- 選択 ---");
            ViewBag.PreviousOccupation = ApplicationUtilities.SetDDLValue(LoadDropdownList("preOcc", "12") as Dictionary<string, string>, "", "--- 選択 ---");
            ViewBag.Zodiac = ApplicationUtilities.SetDDLValue(LoadDropdownList("constellation", "13") as Dictionary<string, string>, "", "--- 選択 ---");
            ViewBag.Rank = ApplicationUtilities.SetDDLValue(LoadDropdownList("rank", "14") as Dictionary<string, string>, "", "--- 選択 ---");
            #endregion

            #region Club Management
            clubMgmtModel.clubManagement = response.MapObjects<clubManagement>();
            clubMgmtModel.clubManagement.ForEach(x => x.Sno = x.Sno.EncryptParameter());
            clubMgmtModel.clubManagement.ForEach(x => x.AgentID = x.AgentID.EncryptParameter());
            #endregion

            #region Host Management
            clubMgmtModel.HostManagement = responseHost.MapObjects<HostManagement>();
            clubMgmtModel.HostManagement.ForEach(x => x.HostID = x.HostID.EncryptParameter());
            clubMgmtModel.HostManagement.ForEach(x => x.AgentID = x.AgentID.EncryptParameter());
            #endregion

            #region Event Management
            clubMgmtModel.EventManagement = responseEvent.MapObjects<EventManagementModel>();
            clubMgmtModel.EventManagement.ForEach(x => x.EventId = x.EventId.EncryptParameter());
            clubMgmtModel.EventManagement.ForEach(x => x.AgentId = x.AgentId.EncryptParameter());
            #endregion

            if (ConfigurationManager.AppSettings["Phase"] != null
                   && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
            clubMgmtModel.clubManagement.ForEach(x => x.ImagePath = FileLocationPath + x.ImagePath);
            clubMgmtModel.HostManagement.ForEach(x => x.ImagePath = FileLocationPath + x.ImagePath);


            string RenderId = "";

            if (TempData.ContainsKey("ClubImage")) clubMgmtModel.ClubImage = TempData["ClubImage"] as AddClubImage;
            if (TempData.ContainsKey("EventManagement")) clubMgmtModel.EventDetail = TempData["EventManagement"] as EventManagementModel;
            if (TempData.ContainsKey("HostManagement")) clubMgmtModel.HostMgmt = TempData["HostManagement"] as HostManagement;
            if (TempData.ContainsKey("RenderId")) RenderId = TempData["RenderId"].ToString();
            ViewBag.PopUpRenderValue = !string.IsNullOrEmpty(RenderId) ? RenderId : null;

            var Response = new List<ClubScheduleModel>();
            var ClubId = ApplicationUtilities.GetSessionValue("AgentId").ToString()?.DecryptParameter();
            var scheduleDBResponse = _scheduleBuss.GetClubSchedule(ClubId);
            if (scheduleDBResponse != null && scheduleDBResponse.Count > 0) Response = scheduleDBResponse.MapObjects<ClubScheduleModel>();
            Response.ForEach(x => x.ScheduleId = x.ScheduleId.EncryptParameter());
            string scheduleJsonData = JsonConvert.SerializeObject(Response);
            ViewBag.ClubSchedulesJson = scheduleJsonData;
            ViewBag.ClubScheduleDDL = ApplicationUtilities.SetDDLValue(ApplicationUtilities.LoadDropdownList("CLUBSCHEDULE") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.SearchText = SearchFilter;
            ViewBag.HostSearchText = HostSearchFilter;
            ViewBag.GallerySearchText = GallerySearchFilter;
            return View(clubMgmtModel);
        }

        #region Club Management - Gallery Management
        [HttpGet]
        public ActionResult AddClubImage()
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            return View();
        }
        [HttpPost]
        public ActionResult AddClubImage(AddClubImage model, HttpPostedFileBase ImagePath)
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            var ClubImageCommon = new AddClubImageCommon();
            var response = new CommonDbResponse();

            if (string.IsNullOrEmpty(model.Title))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "Title is required",
                    Title = NotificationMessage.ERROR.ToString()
                });
                TempData["RenderId"] = "ManagePlan";
                return RedirectToAction("Index");
            }

            string FileLocationPath = "/Content/userupload/ClubManagement/";
            string FileLocation = FileLocationPath;
            HttpPostedFileBase file = Request.Files["imageFile"];
            if (ConfigurationManager.AppSettings["Phase"] != null
                && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocation = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;

            if (ImagePath != null && ImagePath.ContentLength > 0)
            {
                string imgPath;
                var allowedContenttype = new[] { ".heif", ".jpg", ".png", ".jpeg" };
                var ext = Path.GetExtension(ImagePath.FileName);
                if (allowedContenttype.Contains(ext.ToLower()))
                {
                    string datet = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string myfilename = "ClubManagementImg_" + datet + ext.ToLower();
                    imgPath = Path.Combine(Server.MapPath("~/Content/userupload/ClubManagement/"), myfilename);
                    ClubImageCommon.ImagePath = "/Content/userupload/ClubManagement/" + myfilename;
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.INFORMATION,
                        Message = "File Must be must be in jpeg, jpg, heif or png format",
                        Title = NotificationMessage.INFORMATION.ToString()
                    });
                    return RedirectToAction("Index");
                }
                ClubImageCommon.ActionUser = Session["username"]?.ToString();
                ClubImageCommon.ImageTitle = model.Title;
                ClubImageCommon.Sno = model.Sno;
                ClubImageCommon.AgentId = Session["AgentId"]?.ToString().DecryptParameter();
                var serviceResp = _buss.InsertClubImage(ClubImageCommon);
                if (serviceResp.Code == ResponseCode.Success)
                {
                    if (ImagePath != null) ApplicationUtilities.ResizeImage(ImagePath, imgPath);
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.SUCCESS,
                        Message = serviceResp.Message,
                        Title = NotificationMessage.SUCCESS.ToString()
                    });
                    return RedirectToAction("Index");
                }
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "Something went wrong",
                    Title = NotificationMessage.ERROR.ToString()
                });
                return RedirectToAction("Index");
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "Image File is missing",
                    Title = NotificationMessage.ERROR.ToString()
                });
                TempData["RenderId"] = "ManagePlan";
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteClubManagement(string AgentId, string clubsno)
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            var response = new CommonDbResponse();
            var CSNO = !string.IsNullOrEmpty(clubsno) ? clubsno.DecryptParameter() : null;
            var AID = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(CSNO) || string.IsNullOrEmpty(AID))
                response = new CommonDbResponse { ErrorCode = 1, Message = "Details are missing" };
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString()
            };
            var dbResponse = _buss.DeleteImage(CSNO, AID, commonRequest);
            response = dbResponse;
            AddNotificationMessage(new NotificationModel()
            {
                NotificationType = NotificationMessage.SUCCESS,
                Message = response.Message,
                Title = NotificationMessage.SUCCESS.ToString()
            });
            return RedirectToAction("Index");
        }

        public ActionResult EditClubManagement(string AgentId, string clubsno)
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            var response = new clubManagement();

            if (!string.IsNullOrWhiteSpace(AgentId) || !string.IsNullOrWhiteSpace(clubsno))
            {
                AddClubImageCommon common = new AddClubImageCommon()
                {
                    AgentId = AgentId.DecryptParameter(),
                    Sno = clubsno.DecryptParameter(),
                    ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString(),
                    ActionIP = ApplicationUtilities.GetIP(),
                    ActionPlatform = "Admin"
                };

                var serviceResp = _buss.GetClubImage(common);

                TempData["ClubImage"] = serviceResp.MapObject<AddClubImage>();
                TempData["RenderId"] = "ManagePlan";
            }
            return RedirectToAction("Index", "ClubManagement");
        }
        #endregion

        #region Club Management - Host Management
        [HttpGet]
        public ActionResult AddClubHost(string AgentId, string HostId)
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            if (!string.IsNullOrWhiteSpace(AgentId) || !string.IsNullOrWhiteSpace(HostId))
            {
                HostManagementCommon EHC = new HostManagementCommon
                {
                    AgentId = AgentId.DecryptParameter(),
                    HostId = HostId.DecryptParameter()
                };
                var dbResponse = _buss.GetHostByID(EHC);
                TempData["HostManagement"] = dbResponse.MapObject<HostManagement>();
                TempData["RenderId"] = "EditHostPlan";
            }
            return RedirectToAction("Index", "ClubManagement");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddClubHost(HostManagement model, HttpPostedFileBase ImagePath, string Addmonth = "", string AddDay = "")
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            var response = new CommonDbResponse();
            var HostManagementCommon = new HostManagementCommon();
            model.AgentID = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            if (!ModelState.IsValid)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Please fill all required input fields",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                TempData["RenderId"] = "EditHostPlan";
                TempData["HostManagement"] = model;
                return RedirectToAction("Index");
            }

            string FileLocationPath = "/Content/userupload/Host/Gallery/";
            string FileLocation = FileLocationPath;
            if (ConfigurationManager.AppSettings["Phase"] != null
                && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocation = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;

            HttpPostedFileBase file = Request.Files["hostimage"];
            if (ImagePath == null)
            {
                if (string.IsNullOrEmpty(model.ImagePath))
                {
                    var ErrorMessage = string.Empty;
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.INFORMATION,
                        Message = "Image required",
                        Title = NotificationMessage.INFORMATION.ToString()
                    });
                    TempData["RenderId"] = "EditHostPlan";
                    TempData["HostManagement"] = model;
                    return RedirectToAction("Index");
                }
            }
            string imgPath = "";
            if (ImagePath != null)
            {
                var allowedContenttype = new[] { ".heif", ".jpg", ".png", ".jpeg", ".heic" };
                var ext = Path.GetExtension(ImagePath.FileName);
                if (allowedContenttype.Contains(ext.ToLower()))
                {
                    string datet = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string myfilename = "ClubHostProfileImg_" + datet + ext.ToLower();
                    imgPath = Path.Combine(Server.MapPath(FileLocation), myfilename);
                    HostManagementCommon.ImagePath = FileLocationPath + myfilename;
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.INFORMATION,
                        Message = "Invalid Image File Format",
                        Title = NotificationMessage.INFORMATION.ToString()
                    });
                    TempData["RenderId"] = "EditHostPlan";
                    TempData["HostManagement"] = model;
                    return RedirectToAction("Index");
                }

            }
            HostManagementCommon.ActionUser = Session["username"]?.ToString();
            if (!string.IsNullOrEmpty(model.HostID)) HostManagementCommon.HostId = model.HostID;
            HostManagementCommon.HostName = model.HostName;
            HostManagementCommon.Position = model.Position;
            HostManagementCommon.Rank = model.Rank;
            HostManagementCommon.Height = model.Height;
            HostManagementCommon.Twitter = model.Twitter;
            HostManagementCommon.Instagram = model.Instagram;
            HostManagementCommon.TikTok = model.TikTok;
            HostManagementCommon.DateOfBirth = model.DateOfBirth + '-' + Addmonth + '-' + AddDay;
            HostManagementCommon.Constellation = model.ConstellationGroup;
            HostManagementCommon.BloodType = model.BloodType;
            HostManagementCommon.PreviousOccupation = model.PreviousOccupation;
            HostManagementCommon.Liquor = model.Liquor;
            HostManagementCommon.Height = model.Height;
            HostManagementCommon.AgentId = model.AgentID;
            HostManagementCommon.Address = model.Address;
            HostManagementCommon.Line = model.Line;
            HostManagementCommon.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
            HostManagementCommon.ActionIP = ApplicationUtilities.GetIP();
            var serviceResp = _buss.AddClubHost(HostManagementCommon);
            if (serviceResp != null && serviceResp.Code == ResponseCode.Success)
            {
                //ImagePath.SaveAs(imgPath);
                if (ImagePath != null) ApplicationUtilities.ResizeImage(ImagePath, imgPath);
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = serviceResp.Message,
                    Title = NotificationMessage.SUCCESS.ToString()
                });
                return RedirectToAction("Index");
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = serviceResp.Message,
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                TempData["RenderId"] = "EditHostPlan";
                TempData["HostManagement"] = model;
                return RedirectToAction("Index");
            }
        }

        //public ActionResult EditHostManagement(string AgentId, string HostId)
        //{
        //    Session["CurrentURL"] = "/ClubManagement/Index";
        //    if (!string.IsNullOrWhiteSpace(AgentId) || !string.IsNullOrWhiteSpace(HostId))
        //    {
        //        HostManagementCommon EHC = new HostManagementCommon
        //        {
        //            AgentId = AgentId.DecryptParameter(),
        //            HostId = HostId.DecryptParameter()
        //        };
        //        var dbResponse = _buss.GetHostByID(EHC);
        //        TempData["HostManagement"] = dbResponse.MapObject<HostManagement>();
        //        TempData["RenderId"] = "EditHostPlan";
        //    }
        //    return RedirectToAction("Index", "ClubManagement");
        //}

        public ActionResult DeleteHostManagement(string AgentId, string HostId)
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            var HID = !string.IsNullOrEmpty(HostId) ? HostId.DecryptParameter() : null;
            var AID = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(HostId) || string.IsNullOrEmpty(AgentId))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "ID are missing",
                    Title = NotificationMessage.ERROR.ToString()
                });
                return RedirectToAction("Index");
            }
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString()
            };
            var dbResponse = _buss.DeleteHost(HID, AID, commonRequest);
            AddNotificationMessage(new NotificationModel()
            {
                NotificationType = NotificationMessage.SUCCESS,
                Message = dbResponse.Message,
                Title = NotificationMessage.SUCCESS.ToString()
            });
            return RedirectToAction("Index");
        }
        #endregion

        #region Club Management - Event Management
        [HttpGet]
        public ActionResult AddEvent()
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            return View();
        }
        [HttpPost]
        public ActionResult AddEvent(EventManagementModel model)
        {
            var response = new EventManagementModel();
            #region Validation 
            if (string.IsNullOrEmpty(model.EventTitle))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "Event Title is required",
                    Title = NotificationMessage.ERROR.ToString()
                });
                TempData["RenderId"] = "EditEventPlan";
                return RedirectToAction("Index");
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "Description is required",
                    Title = NotificationMessage.ERROR.ToString()
                });
                TempData["RenderId"] = "EditEventPlan";
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "Please fill all required input fields",
                    Title = NotificationMessage.ERROR.ToString()
                });
                TempData["RenderId"] = "EditEventPlan";
                return RedirectToAction("Index");
            }
            #endregion
            var common = model.MapObject<EventManagementCommon>();
            if (!string.IsNullOrEmpty(common.EventId)) common.EventId = model.EventId.DecryptParameter();

            common.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
            common.AgentId = string.IsNullOrEmpty(model.AgentId) ? Session["AgentId"]?.ToString().DecryptParameter() : model.AgentId.DecryptParameter();
            common.ActionIP = ApplicationUtilities.GetIP();
            common.ActionPlatform = "CLUB";
            var serviceResp = _buss.AddEvent(common);
            if (serviceResp.Code == ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = serviceResp.Message,
                    Title = NotificationMessage.SUCCESS.ToString()
                });
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = serviceResp.Message,
                    Title = NotificationMessage.ERROR.ToString()
                });
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditEventManagement(string AgentId, string EventId)
        {
            var response = new EventManagementModel();

            if (!string.IsNullOrWhiteSpace(AgentId) || !string.IsNullOrWhiteSpace(EventId))
            {
                EventManagementCommon EMC = new EventManagementCommon
                {
                    AgentId = AgentId.DecryptParameter(),
                    EventId = EventId.DecryptParameter()
                };

                var dbResponse = _buss.GetEventById(EMC);
                dbResponse.AgentId = AgentId;
                dbResponse.EventId = EventId;
                TempData["EventManagement"] = dbResponse.MapObject<EventManagementModel>();
                TempData["RenderId"] = "EditEventPlan";
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "ID are missing",
                    Title = NotificationMessage.ERROR.ToString()
                });
            }
            return RedirectToAction("Index", "ClubManagement");
        }

        public ActionResult DeleteEventManagement(string AgentId, string EventId)
        {
            Session["CurrentURL"] = "/ClubManagement/Index";
            var EID = !string.IsNullOrEmpty(EventId) ? EventId.DecryptParameter() : null;
            var AID = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(EID) || string.IsNullOrEmpty(AID))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "ID are missing",
                    Title = NotificationMessage.ERROR.ToString()
                });
                return RedirectToAction("Index");
            }
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString()
            };
            var dbResponse = _buss.DeleteEvent(EID, AID, commonRequest);
            if (dbResponse.Code == ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = dbResponse.Message,
                    Title = NotificationMessage.SUCCESS.ToString()
                });
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = dbResponse.Message,
                    Title = NotificationMessage.ERROR.ToString()
                });
            }
            return RedirectToAction("Index");
        }
        #endregion

        private object LoadDropdownList(string ForMethod, string search1 = "", string search2 = "")
        {
            switch (ForMethod)
            {
                case "rank":
                    return CreateDropdownList(_common.GetDropDown("014", search1, search2));
                case "constellation":
                    return CreateDropdownList(_common.GetDropDown("014", search1, search2));
                case "bloodtype":
                    return CreateDropdownList(_common.GetDropDown("014", search1, search2));
                case "preOcc":
                    return CreateDropdownList(_common.GetDropDown("014", search1, search2));
                case "liqStrength":
                    return CreateDropdownList(_common.GetDropDown("022", search1, search2));
                default:
                    return new Dictionary<string, string>();
            }
        }
        private object CreateDropdownList(Dictionary<string, string> dbResponse)
        {
            var response = new Dictionary<string, string>();
            dbResponse.ForEach(item => { response.Add(item.Key, item.Value); });
            return response;
        }

        #region "HOST GALLERY MANAGEMENT"
        [HttpGet]
        public ActionResult HostGalleryManagement(string AgentId = "", string HostId = "", string SearchFilter = "")
        {
            var FileLocationPath = "";
            if (ConfigurationManager.AppSettings["Phase"] != null
                  && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
            ViewBag.AgentId = AgentId;
            ViewBag.HostId = HostId;
            ViewBag.SearchFilter = SearchFilter;

            var agentId = AgentId?.DecryptParameter();
            var hostId = HostId?.DecryptParameter();
            if (string.IsNullOrEmpty(agentId) || string.IsNullOrEmpty(hostId))
            {
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid Host details",
                    Title = NotificationMessage.INFORMATION.ToString(),
                });
                return RedirectToAction("Index", "ClubManagement");
            }
            string RenderId = "";
            var ReturnModel = new CommonHostGalleryManagementModel();
            if (TempData.ContainsKey("GalleryManagementModel")) ReturnModel.HostManageGalleryImageModel = TempData["GalleryManagementModel"] as HostManageGalleryImageModel;
            else ReturnModel.HostManageGalleryImageModel = new HostManageGalleryImageModel();
            if (TempData.ContainsKey("RenderId")) RenderId = TempData["RenderId"].ToString();
            var dbResponse = _buss.GetHostGalleryList(agentId, hostId, SearchFilter, "");
            ReturnModel.HostGalleryManagementModel = dbResponse.MapObjects<HostGalleryManagementModel>();
            foreach (var item in ReturnModel.HostGalleryManagementModel)
            {
                item.AgentId = AgentId;
                item.HostId = HostId;
                item.GalleryId = item.GalleryId?.EncryptParameter();
                item.ImagePath = FileLocationPath + item.ImagePath;
            }
            ReturnModel.HostManageGalleryImageModel.AgentId = AgentId;
            ReturnModel.HostManageGalleryImageModel.HostId = HostId;
            ViewBag.PopUpRenderValue = !string.IsNullOrEmpty(RenderId) ? RenderId : null;
            ViewBag.IsBackAllowed = true;
            ViewBag.BackButtonURL = "/HostManagement/HostList?AgentId=" + AgentId;
            return View(ReturnModel);
        }
        [HttpGet]
        public ActionResult ManageHostGallery(string AgentId, string HostId, string GalleryId)
        {
            HostManageGalleryImageModel model = new HostManageGalleryImageModel();
            var aId = AgentId?.DecryptParameter();
            var hId = HostId?.DecryptParameter();
            var gId = GalleryId?.DecryptParameter();
            if (string.IsNullOrEmpty(aId) || string.IsNullOrEmpty(hId))
            {
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid details",
                    Title = NotificationMessage.INFORMATION.ToString(),
                });
                return RedirectToAction("Index", "ClubManagement");
            }
            if (string.IsNullOrEmpty(gId))
            {
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid gallery details",
                    Title = NotificationMessage.INFORMATION.ToString(),
                });
                return RedirectToAction("HostGalleryManagement", "ClubManagement");
            }
            var dbResponse = _buss.GetHostGalleryList(aId, hId, "", gId);
            if (dbResponse.Count > 0) model = dbResponse[0].MapObject<HostManageGalleryImageModel>();
            model.AgentId = model.AgentId.EncryptParameter();
            model.HostId = model.HostId.EncryptParameter();
            model.GalleryId = model.GalleryId.EncryptParameter();
            TempData["GalleryManagementModel"] = model;
            TempData["RenderId"] = "ManageHostGallery";
            return RedirectToAction("HostGalleryManagement", "ClubManagement", new { AgentId, HostId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ManageHostGallery(HostManageGalleryImageModel Model, HttpPostedFileBase Image_Path)
        {
            var aId = Model.AgentId?.DecryptParameter();
            var hId = Model.HostId?.DecryptParameter();
            if (string.IsNullOrEmpty(aId) || string.IsNullOrEmpty(hId))
            {
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid details",
                    Title = NotificationMessage.INFORMATION.ToString(),
                });
                return RedirectToAction("Index", "ClubManagement");
            }
            if (!string.IsNullOrEmpty(Model.GalleryId))
            {
                var gId = Model.GalleryId?.DecryptParameter();
                if (string.IsNullOrEmpty(gId))
                {
                    this.AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.INFORMATION,
                        Message = "Invalid gallery details",
                        Title = NotificationMessage.INFORMATION.ToString(),
                    });
                    TempData["GalleryManagementModel"] = Model;
                    TempData["RenderId"] = "ManageHostGallery";
                    return RedirectToAction("HostGalleryManagement", "ClubManagement", new { AgentId = Model.AgentId, HostId = Model.HostId });
                }
            }
            string FileLocation = "/Content/UserUpload/Host/Gallery/";
            string FileLocationPath = "/Content/UserUpload/Host/Gallery/";
            string ImageVirtualPath = "";
            if (ConfigurationManager.AppSettings["Phase"] != null
               && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
            {
                FileLocation = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
                ImageVirtualPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString();
            }
            if (string.IsNullOrEmpty(Model.ImagePath))
            {
                if (Image_Path == null)
                {
                    bool allowRedirect = false;
                    var ErrorMessage = string.Empty;
                    if (Image_Path == null && string.IsNullOrEmpty(Model.ImagePath))
                    {
                        ErrorMessage = "Image required";
                        allowRedirect = true;
                    }
                    if (allowRedirect)
                    {
                        this.AddNotificationMessage(new NotificationModel()
                        {
                            NotificationType = NotificationMessage.INFORMATION,
                            Message = ErrorMessage ?? "Something went wrong. Please try again later.",
                            Title = NotificationMessage.INFORMATION.ToString(),
                        });
                        TempData["GalleryManagementModel"] = Model;
                        TempData["RenderId"] = "ManageHostGallery";
                        return RedirectToAction("HostGalleryManagement", "ClubManagement", new { AgentId = Model.AgentId, HostId = Model.HostId });
                    }
                }
            }
            string ImagePath = "";
            var allowedContentType = AllowedImageContentType();
            if (Image_Path != null)
            {
                var contentType = Image_Path.ContentType;
                var ext = Path.GetExtension(Image_Path.FileName);
                if (allowedContentType.Contains(contentType.ToLower()))
                {
                    var dateTime = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string fileName = "GalleryImage_" + dateTime + ext.ToLower();
                    ImagePath = Path.Combine(Server.MapPath(FileLocation), fileName);
                    Model.ImagePath = "/Content/UserUpload/Host/Gallery/" + fileName;
                }
                else
                {
                    this.AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.INFORMATION,
                        Message = "Invalid image format.",
                        Title = NotificationMessage.INFORMATION.ToString(),
                    });
                    TempData["GalleryManagementModel"] = Model;
                    TempData["RenderId"] = "ManageHostGallery";
                    return RedirectToAction("HostGalleryManagement", "ClubManagement", new { AgentId = Model.AgentId, HostId = Model.HostId });
                }
            }
            var dbRequest = Model.MapObject<HostManageGalleryImageCommon>();
            dbRequest.AgentId = Model.AgentId?.DecryptParameter();
            dbRequest.HostId = Model.HostId?.DecryptParameter();
            dbRequest.GalleryId = Model.GalleryId?.DecryptParameter();
            dbRequest.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
            dbRequest.ActionIP = ApplicationUtilities.GetIP();
            var dbResponse = _buss.ManageHostGalleryImage(dbRequest);
            if (dbResponse != null && dbResponse.Code == 0)
            {
                if (Image_Path != null) ApplicationUtilities.ResizeImage(Image_Path, ImagePath);
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = dbResponse.Code == ResponseCode.Success ? NotificationMessage.SUCCESS : NotificationMessage.INFORMATION,
                    Message = dbResponse.Message ?? "Failed",
                    Title = dbResponse.Code == ResponseCode.Success ? NotificationMessage.SUCCESS.ToString() : NotificationMessage.INFORMATION.ToString()
                });
                return RedirectToAction("HostGalleryManagement", "ClubManagement", new { AgentId = Model.AgentId, HostId = Model.HostId });
            }
            else
            {
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = dbResponse.Message ?? "Failed",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                TempData["GalleryManagementModel"] = Model;
                TempData["RenderId"] = "ManageHostGallery";
                return RedirectToAction("HostGalleryManagement", "ClubManagement", new { AgentId = Model.AgentId, HostId = Model.HostId });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ManageHostGalleryStatus(string AgentId, string HostId, string GalleryId)
        {
            var response = new CommonDbResponse();
            var aId = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            var hId = !string.IsNullOrEmpty(HostId) ? HostId.DecryptParameter() : null;
            var gId = !string.IsNullOrEmpty(GalleryId) ? GalleryId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(aId) || string.IsNullOrEmpty(hId) || string.IsNullOrEmpty(gId))
            {
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Invalid details",
                    Title = NotificationMessage.INFORMATION.ToString(),
                });
                return Json(response.Message, JsonRequestBehavior.AllowGet);
            }
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString()
            };
            var dbResponse = _buss.ManageHostGalleryImageStatus(aId, hId, gId, commonRequest);
            response = dbResponse;
            this.AddNotificationMessage(new NotificationModel()
            {
                NotificationType = response.Code == ResponseCode.Success ? NotificationMessage.SUCCESS : NotificationMessage.INFORMATION,
                Message = response.Message ?? "Something went wrong. Please try again later",
                Title = response.Code == ResponseCode.Success ? NotificationMessage.SUCCESS.ToString() : NotificationMessage.INFORMATION.ToString()
            });
            return Json(response.Message, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}