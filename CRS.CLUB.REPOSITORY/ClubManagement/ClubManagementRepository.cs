using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ClubManagement;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CRS.CLUB.REPOSITORY.ClubManagement
{
    public class ClubManagementRepository : IClubManagementRepository
    {
        private readonly RepositoryDao _DAO;

        public ClubManagementRepository()
        {
            _DAO = new RepositoryDao();
        }

        #region Gallery Management

        public CommonDbResponse DeleteImage(string CSNO, string AID, Common commonRequest)
        {
            var SQL = "EXEC sproc_club_management @Flag='dgi'";
            SQL += ",@ClubSno=" + _DAO.FilterString(CSNO);
            SQL += ",@AgentId=" + _DAO.FilterString(AID);
            SQL += ",@ActionUser=" + _DAO.FilterString(commonRequest.ActionUser);
            SQL += ",@ActionIP=" + _DAO.FilterString(commonRequest.ActionIP);
            return _DAO.ParseCommonDbResponse(SQL);
        }

        public List<ClubManagementCommon> GetClubImages(string AgentId, string GallerySearchFilter)
        {
            var response = new List<ClubManagementCommon>();
            string SQL = "EXEC sproc_club_management @Flag='cmg'";
            SQL += ",@AgentId=" + _DAO.FilterString(AgentId);
            SQL += ",@SearchFilter=" + _DAO.FilterString(GallerySearchFilter);
            var dbResponse = _DAO.ExecuteDataTable(SQL);
            if (dbResponse != null)
            {
                foreach (DataRow item in dbResponse.Rows)
                {
                    response.Add(new ClubManagementCommon()
                    {
                        //LoginId = _DAO.ParseColumnValue(item, "LoginId").ToString(),
                        AgentID = _DAO.ParseColumnValue(item, "AgentId").ToString(),
                        ImagePath = _DAO.ParseColumnValue(item, "ImagePath").ToString(),
                        Title = _DAO.ParseColumnValue(item, "ImageTitle").ToString(),
                        Status = _DAO.ParseColumnValue(item, "Status").ToString(),
                        Sno = _DAO.ParseColumnValue(item, "Sno").ToString(),
                        CreatedDate = _DAO.ParseColumnValue(item, "CreatedDate").ToString(),
                        UpdatedDate = _DAO.ParseColumnValue(item, "UpdatedDate").ToString(),
                    });
                }
            }
            return response;
        }

        public CommonDbResponse InsertClubImage(AddClubImageCommon clubImageCommon)
        {
            var sql = "Exec sproc_club_management";
            sql += !string.IsNullOrEmpty(clubImageCommon.Sno) ? " @Flag='uci'" : " @Flag='aci'"; // Update club image /Add
            sql += ",@AgentId = " + _DAO.FilterString(clubImageCommon.AgentId);
            sql += ",@ImageID = " + _DAO.FilterString(clubImageCommon.Sno); //ImageID
            sql += ",@ImageTitle = " + _DAO.FilterString(clubImageCommon.ImageTitle);
            sql += ",@ImagePath = " + _DAO.FilterString(clubImageCommon.ImagePath);
            sql += ",@ActionUser=" + _DAO.FilterString(clubImageCommon.ActionUser);
            return _DAO.ParseCommonDbResponse(sql);
        }

        public AddClubImageCommon GetClubImage(AddClubImageCommon ACC)
        {
            var SQL = "EXEC sproc_club_management @Flag='gci'";
            SQL += ",@ClubSno=" + _DAO.FilterString(ACC.Sno);
            SQL += ",@AgentId=" + _DAO.FilterString(ACC.AgentId);
            var dbResp = _DAO.ExecuteDataTable(SQL);

            if (dbResp != null && dbResp.Rows.Count > 0)
            {
                return new AddClubImageCommon()
                {
                    Title = dbResp.Rows[0]["ImageTitle"]?.ToString(),
                    ImagePath = dbResp.Rows[0]["ImagePath"]?.ToString(),
                    Sno = dbResp.Rows[0]["Sno"]?.ToString(),
                    AgentId = dbResp.Rows[0]["AgentId"]?.ToString()
                };
            }
            return new AddClubImageCommon();
        }
        #endregion

        #region Host Management

        public List<HostManagementCommon> GetHostList(string AgentID, string HostSearchFilter)
        {
            var response = new List<HostManagementCommon>();
            string SQL = "EXEC sproc_host_management @Flag='ghl'";
            SQL += ",@AgentId=" + _DAO.FilterString(AgentID);
            SQL += ",@SearchFilter=" + _DAO.FilterString(HostSearchFilter);
            var dbResponse = _DAO.ExecuteDataTable(SQL);
            if (dbResponse != null)
            {
                foreach (DataRow item in dbResponse.Rows)
                {
                    response.Add(new HostManagementCommon()
                    {
                        AgentId = _DAO.ParseColumnValue(item, "AgentId").ToString(),
                        HostId = _DAO.ParseColumnValue(item, "HostId").ToString(),
                        HostName = _DAO.ParseColumnValue(item, "HostName").ToString(),
                        Position = _DAO.ParseColumnValue(item, "Position").ToString(),
                        Rank = _DAO.ParseColumnValue(item, "Rank").ToString(),
                        Age = _DAO.ParseColumnValue(item, "Age").ToString(),
                        ConstellationGroup = _DAO.ParseColumnValue(item, "ConstellationGroup").ToString(),
                        Status = _DAO.ParseColumnValue(item, "Status").ToString(),
                        ActionUser = _DAO.ParseColumnValue(item, "ActionUser").ToString(),
                        ActionIP = _DAO.ParseColumnValue(item, "ActionIP").ToString(),
                        ActionPlatform = _DAO.ParseColumnValue(item, "ActionPlatform").ToString(),
                        CreatedDate = _DAO.ParseColumnValue(item, "CreatedDate").ToString(),
                        UpdatedDate = _DAO.ParseColumnValue(item, "UpdatedDate").ToString(),
                        ImagePath = _DAO.ParseColumnValue(item, "ImagePath").ToString(),
                    });
                }
            }
            return response;
        }

        public CommonDbResponse DeleteHost(string HID, string AID, Common commonRequest)
        {
            var SQL = "EXEC sproc_host_management @Flag='dhi'"; // Disable Host
            SQL += ",@HostId=" + _DAO.FilterString(HID);
            SQL += ",@AgentId=" + _DAO.FilterString(AID);
            SQL += ",@ActionUser=" + _DAO.FilterString(commonRequest.ActionUser);
            SQL += ",@ActionIP=" + _DAO.FilterString(commonRequest.ActionIP);
            return _DAO.ParseCommonDbResponse(SQL);
        }

        public HostManagementCommon GetHostByID(HostManagementCommon EHC)
        {
            var SQL = "EXEC sproc_host_management @Flag='ghbi'";
            SQL += ",@HostId=" + _DAO.FilterString(EHC.HostId);
            SQL += ",@AgentId=" + _DAO.FilterString(EHC.AgentId);
            var dbResp = _DAO.ExecuteDataTable(SQL);

            if (dbResp != null && dbResp.Rows.Count > 0)
            {
                return new HostManagementCommon()
                {
                    Sno = dbResp.Rows[0]["Sno"]?.ToString(),
                    AgentId = dbResp.Rows[0]["AgentId"]?.ToString(),
                    HostId = dbResp.Rows[0]["HostId"]?.ToString(),
                    HostName = dbResp.Rows[0]["HostName"]?.ToString(),
                    Twitter = dbResp.Rows[0]["TwitterLink"]?.ToString(),
                    Instagram = dbResp.Rows[0]["InstagramLink"]?.ToString(),
                    TikTok = dbResp.Rows[0]["TiktokLink"]?.ToString(),
                    Position = dbResp.Rows[0]["Position"]?.ToString(),
                    Rank = dbResp.Rows[0]["Rank"]?.ToString(),
                    Age = dbResp.Rows[0]["DOB"]?.ToString(),
                    ConstellationGroup = dbResp.Rows[0]["ConstellationGroup"]?.ToString(),
                    Height = dbResp.Rows[0]["Height"]?.ToString(),
                    BloodType = dbResp.Rows[0]["BloodType"]?.ToString(),
                    Liquor = dbResp.Rows[0]["LiquorStrength"]?.ToString(),
                    PreviousOccupation = dbResp.Rows[0]["PreviousOccupation"]?.ToString(),
                    ActionDate = dbResp.Rows[0]["ActionDate"]?.ToString(),
                    Status = dbResp.Rows[0]["Status"]?.ToString(),
                    Year = dbResp.Rows[0]["Year"]?.ToString(),
                    Month = dbResp.Rows[0]["Month"]?.ToString(),
                    Day = dbResp.Rows[0]["Day"]?.ToString(),
                    Line = dbResp.Rows[0]["Line"]?.ToString(),
                    Address = dbResp.Rows[0]["Address"]?.ToString(),
                    ImagePath = dbResp.Rows[0]["ImagePath"]?.ToString(),
                };
            }
            return new HostManagementCommon();
        }

        public CommonDbResponse AddClubHost(HostManagementCommon common)
        {
            string sql = "Exec sproc_host_management";
            string flag = string.IsNullOrEmpty(common.HostId) ? "rh" : "uh";
            sql += $" @Flag='{flag}'";
            sql += ",@AgentId=" + _DAO.FilterString(common.AgentId);
            sql += ",@HostId=" + _DAO.FilterString(common.HostId);
            sql += ",@HostName=" + _DAO.FilterString(common.HostName);
            sql += ",@Position=" + _DAO.FilterString(common.Position);
            sql += ",@Rank=" + _DAO.FilterString(common.Rank);
            sql += ",@Height=" + _DAO.FilterString(common.Height);
            sql += ",@TwitterLink=" + _DAO.FilterString(common.Twitter);
            sql += ",@InstagramLink=" + _DAO.FilterString(common.Instagram);
            sql += ",@TiktokLink=" + _DAO.FilterString(common.TikTok);
            sql += ",@ImagePath=" + _DAO.FilterString(common.ImagePath);
            sql += ",@DOB=" + _DAO.FilterString(common.DateOfBirth);
            sql += ",@ConstellationGroup=" + _DAO.FilterString(common.Constellation);
            sql += ",@BloodType=" + _DAO.FilterString(common.BloodType);
            sql += ",@PreviousOccupation=" + _DAO.FilterString(common.PreviousOccupation);
            sql += ",@LiquorStrength=" + _DAO.FilterString(common.Liquor);
            sql += ",@Address=" + _DAO.FilterString(common.Address);
            sql += ",@Line=" + _DAO.FilterString(common.Line);
            sql += ",@ActionUser=" + _DAO.FilterString(common.ActionUser);
            sql += ",@ActionIP=" + _DAO.FilterString(common.ActionIP);
            sql += ",@ActionPlatform=" + _DAO.FilterString(common.ActionPlatform);
            var commondbResp = _DAO.ParseCommonDbResponse(sql);
            return commondbResp;
        }

        #endregion

        #region Event Management

        public List<EventManagementCommon> GetEventList(string agentId, string SearchFilter)
        {
            var response = new List<EventManagementCommon>();
            string SQL = "EXEC sproc_event_management @Flag='gel'";
            SQL += ",@AgentId=" + _DAO.FilterString(agentId);
            SQL += ",@SearchFilter=" + _DAO.FilterString(SearchFilter);
            var dbResponse = _DAO.ExecuteDataTable(SQL);
            if (dbResponse != null)
            {
                foreach (DataRow item in dbResponse.Rows)
                {
                    response.Add(new EventManagementCommon()
                    {
                        AgentId = _DAO.ParseColumnValue(item, "AgentId").ToString(),
                        EventId = _DAO.ParseColumnValue(item, "EventId").ToString(),
                        EventTitle = _DAO.ParseColumnValue(item, "EventTitle").ToString(),
                        Message = _DAO.ParseColumnValue(item, "Description").ToString(),
                        CreatedDate = _DAO.ParseColumnValue(item, "ActionDate").ToString(),
                        UpdatedDate = _DAO.ParseColumnValue(item, "UpdatedDate").ToString(),
                        EventDate = _DAO.ParseColumnValue(item, "EventDate").ToString(),
                    });
                }
            }
            return response;
        }

        public CommonDbResponse DeleteEvent(string EID, string aID, Common commonRequest)
        {
            var SQL = "EXEC sproc_event_management @Flag='del'";
            SQL += ",@EventId=" + _DAO.FilterString(EID);
            SQL += ",@AgentId=" + _DAO.FilterString(aID);
            SQL += ",@ActionUser=" + _DAO.FilterString(commonRequest.ActionUser);
            SQL += ",@ActionIP=" + _DAO.FilterString(commonRequest.ActionIP);
            return _DAO.ParseCommonDbResponse(SQL);
        }

        public CommonDbResponse AddEvent(EventManagementCommon common)
        {
            string sql = "Exec sproc_event_management";
            string flag = common.EventId is null ? "rc" : "me";
            sql += $" @Flag='{flag}'";
            sql += ", @AgentId=" + _DAO.FilterString(common.AgentId);
            sql += ", @EventId=" + _DAO.FilterString(common.EventId);
            sql += ", @EventTitle=N" + _DAO.FilterString(common.EventTitle);
            sql += ", @Description=N" + _DAO.FilterString(common.Description);
            sql += ", @EventDate=" + _DAO.FilterString(string.IsNullOrEmpty(common.ActionDate) ? common.EventDate : common.ActionDate); //Date of Event
            sql += ", @ActionUser=" + _DAO.FilterString(common.ActionUser);
            sql += ", @ActionIP=" + _DAO.FilterString(common.ActionIP);
            sql += ", @ActionPlatform=" + _DAO.FilterString(common.ActionPlatform);
            var commondbResp = _DAO.ParseCommonDbResponse(sql);
            return commondbResp;
        }

        public EventManagementCommon GetEventById(EventManagementCommon EMC)
        {
            var SQL = "EXEC sproc_event_management @Flag='ged'";
            SQL += ",@EventId=" + _DAO.FilterString(EMC.EventId);
            SQL += ",@AgentId=" + _DAO.FilterString(EMC.AgentId);
            var dbResp = _DAO.ExecuteDataTable(SQL);

            if (dbResp != null && dbResp.Rows.Count > 0)
            {
                return new EventManagementCommon()
                {
                    EventId = dbResp.Rows[0]["EventId"]?.ToString(),
                    AgentId = dbResp.Rows[0]["AgentId"]?.ToString(),
                    EventTitle = dbResp.Rows[0]["EventTitle"]?.ToString(),
                    Description = dbResp.Rows[0]["Description"]?.ToString(),
                    EventDate = dbResp.Rows[0]["Date"]?.ToString(),
                    Status = dbResp.Rows[0]["Status"]?.ToString()
                };
            }
            return new EventManagementCommon();
        }


        #endregion

        #region "Host Gallery Management"
        public List<HostGalleryManagementCommon> GetHostGalleryList(string agentId, string hostId, string searchFilter, string GalleryId)
        {
            string SQL = "EXEC dbo.sproc_host_gallery_management @Flag='ghgl'";
            SQL += ", @AgentId =" + _DAO.FilterString(agentId);
            SQL += ", @HostId =" + _DAO.FilterString(hostId);
            SQL += !string.IsNullOrEmpty(GalleryId) ? ", @GalleryId =" + _DAO.FilterString(GalleryId) : "";
            SQL += !string.IsNullOrEmpty(searchFilter) ? ", @SearchFilter =N" + _DAO.FilterString(searchFilter) : "";
            var dbResponse = _DAO.ExecuteDataTable(SQL);
            if (dbResponse != null && dbResponse.Rows.Count > 0) return _DAO.DataTableToListObject<HostGalleryManagementCommon>(dbResponse).ToList();
            return new List<HostGalleryManagementCommon>();
        }

        public CommonDbResponse ManageHostGalleryImage(HostManageGalleryImageCommon dbRequest)
        {
            string SQL = "EXEC dbo.sproc_host_gallery_management ";
            SQL += !string.IsNullOrEmpty(dbRequest.GalleryId) ? "@Flag='mhgi'" : "@Flag='ihgi'";
            SQL += !string.IsNullOrEmpty(dbRequest.GalleryId) ? ", @GalleryId =" + _DAO.FilterString(dbRequest.GalleryId) : "";
            SQL += ",@AgentId=" + _DAO.FilterString(dbRequest.AgentId);
            SQL += ", @HostId =" + _DAO.FilterString(dbRequest.HostId);
            SQL += ",@ImageTitle=N" + _DAO.FilterString(dbRequest.ImageTitle);
            SQL += ",@ImagePath=" + _DAO.FilterString(dbRequest.ImagePath);
            SQL += ",@ActionUser=" + _DAO.FilterString(dbRequest.ActionUser);
            SQL += ",@ActionPlatform=" + _DAO.FilterString(dbRequest.ActionPlatform);
            SQL += ",@ActionIP=" + _DAO.FilterString(dbRequest.ActionIP);
            return _DAO.ParseCommonDbResponse(SQL);
        }

        public CommonDbResponse ManageHostGalleryImageStatus(string aId, string hId, string gId, Common commonRequest)
        {
            string SQL = "EXEC dbo.sproc_host_gallery_management @Flag='mhgis'";
            SQL += ",@AgentId=" + _DAO.FilterString(aId);
            SQL += ",@HostId=" + _DAO.FilterString(hId);
            SQL += ",@GalleryId=" + _DAO.FilterString(gId);
            SQL += ",@ActionUser=" + _DAO.FilterString(commonRequest.ActionUser);
            SQL += ",@ActionPlatform=" + _DAO.FilterString(commonRequest.ActionPlatform);
            SQL += ",@ActionIP=" + _DAO.FilterString(commonRequest.ActionIP);
            return _DAO.ParseCommonDbResponse(SQL);
        }
        #endregion
    }
}
