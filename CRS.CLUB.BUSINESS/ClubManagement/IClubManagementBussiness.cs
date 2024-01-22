using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ClubManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.BUSINESS.ClubManagement
{
    public interface IClubManagementBussiness
    {
        CommonDbResponse DeleteImage(string cSNO, string aID, Common commonRequest);
        List<ClubManagementCommon> GetClubImages(string AgentId,string GallerySearchFilter);
        CommonDbResponse InsertClubImage(AddClubImageCommon clubImageCommon);
        AddClubImageCommon GetClubImage(AddClubImageCommon ACC);
        List<HostManagementCommon> GetHostList(string AgentID, string HostSearchFilter);
        CommonDbResponse DeleteHost(string HID, string AID, Common commonRequest);
        HostManagementCommon GetHostByID(HostManagementCommon EHC);
        CommonDbResponse AddClubHost(HostManagementCommon common);
        List<EventManagementCommon> GetEventList(string agentId,string SearchFilter);
        CommonDbResponse DeleteEvent(string EID, string aID, Common commonRequest);
        CommonDbResponse AddEvent(EventManagementCommon common);
        EventManagementCommon GetEventById(EventManagementCommon eMC);
        #region "Host Gallery Management"
        List<HostGalleryManagementCommon> GetHostGalleryList(string agentId, string hostId, string SearchFilter = "", string GalleryId = "");
        CommonDbResponse ManageHostGalleryImage(HostManageGalleryImageCommon dbRequest);
        CommonDbResponse ManageHostGalleryImageStatus(string aId, string hId, string gId, Common commonRequest);
        #endregion
    }
}
