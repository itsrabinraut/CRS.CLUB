using CRS.CLUB.APPLICATION.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRS.CLUB.APPLICATION.Models.ClubManagement
{
    public class ClubManagementModel : CommonModel
    {
        public List<clubManagement> clubManagement { get; set; } = new List<clubManagement>();
        public AddClubImage ClubImage { get; set; } = new AddClubImage();
        public List<HostManagement> HostManagement { get; set; } = new List<HostManagement>();
        public HostManagement HostMgmt { get; set; } = new HostManagement();
        public List<EventManagementModel> EventManagement { get; set; } = new List<EventManagementModel>();
        public EventManagementModel EventDetail { get; set; } = new EventManagementModel();
    }

    #region Gallery Management
    public class clubManagement : CommonModel
    {
        public string Title { get; set; }
        public string AgentID { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Sno { get; set; }
        public string ImagePath { get; set; }
    }
    public class AddClubImage
    {
        [DisplayName("Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Display(Name = "Gallery_or_Banner", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessage = "HEIF image is required.")]
        public HttpPostedFileBase ImageFile { get; set; }
        public string AgentId { get; set; }
        public string ClubSno { get; set; }
        public string Sno { get; set; }
        public string ImagePath { get; set; }
    }
    #endregion

    #region Host Management
    public class HostManagement : CommonModel
    {
        public string Sno { get; set; }
        public string Age { get; set; }
        public string AgentID { get; set; }
        public string HostID { get; set; }
        public string ImagePath { get; set; }
        public string ExtraMessage { get; set; }

        [DisplayName("Height")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Height { get; set; }
        public string Email { get; set; }
        [DisplayName("Host Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string HostName { get; set; }
        [DisplayName("Position")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Position { get; set; }
        [DisplayName("Twitter (X)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Twitter { get; set; }
        [DisplayName("Instagram")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Instagram { get; set; }
        [DisplayName("TikTok")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string TikTok { get; set; }
        [DisplayName("Line")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Line { get; set; }
        [DisplayName("Date of Birth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string DateOfBirth { get; set; }
        [DisplayName("Liquor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Liquor { get; set; }
        [DisplayName("Rank")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Rank { get; set; }
        [DisplayName("Blood Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string BloodType { get; set; }
        [DisplayName("Previous Occupation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string PreviousOccupation { get; set; }
        [DisplayName("Constellation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string ConstellationGroup { get; set; }
        public string Address { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
    }
    #endregion

    #region Event Management
    public class EventManagementModel
    {
        public string AgentId { get; set; }
        public string EventId { get; set; }
        [DisplayName("Event Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string EventTitle { get; set; }
        [DisplayName("Event Detail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Description { get; set; }
        [DisplayName("Event Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string EventDate { get; set; }
        public string CreatedDate { get; set; }
        public string Message { get; set; }
    }
    #endregion

    #region "Host Gallery Management"
    public class CommonHostGalleryManagementModel
    {
        public List<HostGalleryManagementModel> HostGalleryManagementModel { get; set; }
        public HostManageGalleryImageModel HostManageGalleryImageModel { get; set; }
    }
    public class HostGalleryManagementModel
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

    public class HostManageGalleryImageModel
    {
        public string GalleryId { get; set; }
        public string AgentId { get; set; }
        public string HostId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        [MinLength(1, ErrorMessage = "Minimum 1 characters required")]
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
    }
    #endregion
}