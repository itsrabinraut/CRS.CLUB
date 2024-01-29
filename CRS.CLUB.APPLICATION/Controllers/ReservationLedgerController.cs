using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.ReservationLedger;
using CRS.CLUB.BUSINESS.ReservationLedger;
using CRS.CLUB.SHARED.ReservationLedger;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class ReservationLedgerController : BaseController
    {
        private readonly IReservationLedgerBusiness _business;
        public ReservationLedgerController(IReservationLedgerBusiness business) => _business = business;
        public ActionResult ReservationLedger(CommonReservationLedgerModel Model, int StartIndex = 0, int PageSize = 10)
        {
            Session["CurrentURL"] = "/ReservationLedger/ReservationLedger";
            string ClubId = ApplicationUtilities.GetSessionValue("AgentId").ToString().DecryptParameter();
            string FileLocationPath = "";
            if (ConfigurationManager.AppSettings["Phase"] != null
               && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
            var request = Model.MapObject<SearchFilterModel>();
            request.Skip = StartIndex;
            request.Take = PageSize;
            CommonReservationLedgerModel responseInfo = new CommonReservationLedgerModel();
            var dbResponse = _business.GetReservationLedgerListDetail(ClubId, request);
            responseInfo.GetReservationLedgerList = dbResponse.MapObjects<ReservationLedgerModel>();
            responseInfo.GetReservationLedgerList.ForEach(x => x.CustomerProfileImage = FileLocationPath + x.CustomerProfileImage);
            var analyticeDBResponse = _business.GetLedgerAnalyticDetail(ClubId);
            responseInfo.GetReservationLedgerAnalyticData = analyticeDBResponse.MapObject<ReservationLedgerAnalyticDetailModel>();
            ViewBag.SearchText = Model.SearchFilter;
            ViewBag.FromDate = Model.FromDate;
            ViewBag.ToDate = Model.ToDate;
            ViewBag.StartIndex = StartIndex;
            ViewBag.PageSize = PageSize;
            ViewBag.TotalData = dbResponse != null && dbResponse.Any() ? dbResponse[0].TotalRecords : 0;
            return View(responseInfo);
        }
    }
}