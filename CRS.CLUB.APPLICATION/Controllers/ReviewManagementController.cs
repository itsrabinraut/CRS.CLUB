using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.ReviewManagement;
using CRS.CLUB.BUSINESS.ReviewManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReviewManagement;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class ReviewManagementController : BaseController
    {
        private readonly IReviewManagementBusiness _buss;
        public ReviewManagementController(IReviewManagementBusiness buss) => _buss = buss;

        public ActionResult Index(SearchFilterCommonModel Request, int StartIndex = 0, int PageSize = 10)
        {
            Session["CurrentUrl"] = "/ReviewManagement/Index";
            var reviewAndRatingsViewModel = new ReviewManagementModel();
            string FileLocationPath = "";
            if (ConfigurationManager.AppSettings["Phase"] != null
               && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
            var dbRequest = Request.MapObject<SearchFilterCommonModel>();
            dbRequest.Skip = StartIndex;
            dbRequest.Take = PageSize;
            string agentId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            var reviewCommon = _buss.GetReviews(agentId, dbRequest, "");
            if (reviewCommon != null && reviewCommon.Count > 0)
            {
                reviewAndRatingsViewModel.ReviewsAndRatings = reviewCommon.MapObjects<ReviewModel>();
                reviewAndRatingsViewModel.ReviewsAndRatings.ForEach(x =>
                {
                    x.ReviewId = x.ReviewId.EncryptParameter();
                    x.UserImage = FileLocationPath + x.UserImage;
                });
            }
            ViewBag.SearchText = Request.SearchFilter;
            ViewBag.StartIndex = StartIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.TotalData = reviewCommon != null && reviewCommon.Any() ? reviewCommon[0].TotalRecords : 0;
            return View(reviewAndRatingsViewModel);
        }
        [HttpPost]
        public JsonResult RemoveReviewAndRatings(string reviewId)
        {
            var rId = string.IsNullOrEmpty(reviewId) ? string.Empty : reviewId.DecryptParameter();
            if (string.IsNullOrEmpty(rId))
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.WARNING,
                    Message = "Invalid Details",
                    Title = NotificationMessage.WARNING.ToString()
                });
                return Json(new { success = false, message = "Invalid Details" });
            }

            string actionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
            string actionIp = ApplicationUtilities.GetIP();

            var dbResp = _buss.DeleteReview(rId, actionUser, actionIp);
            if (dbResp != null && dbResp.Code == ResponseCode.Success)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.SUCCESS,
                    Message = dbResp?.Message,
                    Title = NotificationMessage.SUCCESS.ToString()
                });
                return Json(new { success = true, message = dbResp?.Message });
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = dbResp?.Message ?? "Something went wrong",
                    Title = NotificationMessage.ERROR.ToString()
                });
                return Json(new { success = false, message = dbResp?.Message ?? "Something went wrong" });
            }
        }
    }
}