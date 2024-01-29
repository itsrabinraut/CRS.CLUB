using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.SHARED.ClubManagement
{
    public class HostManagementCommon : Common
    {
        public string HostId { get; set; }
        public string HostImage { get; set; }
        public string HostName { get; set; }
        public string Position { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string TikTok { get; set; }
        public string Rank { get; set; }
        public string Age { get; set; } //Data of Birth
        public string DateOfBirth { get; set; }
        public string Constellation { get; set; }
        public string Liquor { get; set; }
        public string ConstellationGroup { get; set; }
        public string Height { get; set; }
        public string BloodType { get; set; }
        public string LiquorStrength { get; set; }
        public string PreviousOccupation { get; set; }
        public string Sno { get; set; }
        public string ImagePath { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Address { get; set; }
        public string Line { get; set; }
        public int TotalRecords { get; set; }
        public int SNO { get; set; }
    }
    #region "Host Gallery Management"
    public class HostGalleryManagementCommon
    {
        public string GalleryId { get; set; }
        public string AgentId { get; set; }
        public string HostId { get; set; }
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedDate { get; set; }
    }

    public class HostManageGalleryImageCommon : Common
    {
        public string GalleryId { get; set; }
        public string AgentId { get; set; }
        public string HostId { get; set; }
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
    }
    #endregion
}
