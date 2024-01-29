using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ClubManagement;
using CRS.CLUB.SHARED.PaginationManagement;
using System;
using System.Collections.Generic;

namespace CRS.CLUB.REPOSITORY.ClubManagement
{
    public interface IClubManagementRepository
    {
        CommonDbResponse DeleteImage(string CSNO, string AID, Common commonRequest);
        List<ClubManagementCommon> GetClubImages(string AgentId, PaginationFilterCommon GalleryReq);
        CommonDbResponse InsertClubImage(AddClubImageCommon clubImageCommon);
        AddClubImageCommon GetClubImage(AddClubImageCommon ACC);
        List<HostManagementCommon> GetHostList(string AgentID, PaginationFilterCommon HostReq);
        CommonDbResponse DeleteHost(string HID, string AID, Common commonRequest);
        HostManagementCommon GetHostByID(HostManagementCommon EHC);
        CommonDbResponse AddClubHost(HostManagementCommon common);
        List<EventManagementCommon> GetEventList(string agentId, PaginationFilterCommon EventReq);
        CommonDbResponse DeleteEvent(string EID, string aID, Common commonRequest);
        CommonDbResponse AddEvent(EventManagementCommon common);
        EventManagementCommon GetEventById(EventManagementCommon EMC);
        #region "Host Gallery Management"
        List<HostGalleryManagementCommon> GetHostGalleryList(string agentId, string hostId, string searchFilter, string GalleryId);
        CommonDbResponse ManageHostGalleryImage(HostManageGalleryImageCommon dbRequest);
        CommonDbResponse ManageHostGalleryImageStatus(string aId, string hId, string gId, Common commonRequest);
        #endregion
    }
}
