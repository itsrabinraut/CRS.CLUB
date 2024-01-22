using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CRS.CLUB.APPLICATION.Models.ClubManagement
{
    public class ClubModel
    {
        [DisplayName("Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [DisplayName("Gallery/Banner")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "HEIF image is required.")]
        public HttpPostedFileBase ImageFile { get; set; }
        [DisplayName("Event Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Title is required.")]
        public string EventTitle { get; set; }
        [DisplayName("Event Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Date is required.")]
        public string EventDate { get; set; }
        [DisplayName("Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [DisplayName("Host Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Host Name is required.")]
        public string HostName { get; set; }
        [DisplayName("Position")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Position is required.")]
        public string Position { get; set; }
        [DisplayName("Rank")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Rank is required.")]
        public string Rank { get; set; }
        [DisplayName("Twitter")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Twitter is required.")]
        public string Twitter { get; set; }
        [DisplayName("Instagram")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Instagram is required.")]
        public string Instagram { get; set; }
        [DisplayName("Tiktok")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tiktok is required.")]
        public string Tiktok { get; set; }
        [DisplayName("HostImage")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "HostImage is required.")]
        public string HostImage { get; set; }
        [DisplayName("Constellation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Constellation is required.")]
        public string Constellation { get; set; }
        [DisplayName("Height")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Height is required.")]
        public string Height { get; set; }
        [DisplayName("BloodType")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "BloodType is required.")]
        public string BloodType { get; set; }
        [DisplayName("PreviousOccupation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "PreviousOccupation is required.")]
        public string PreviousOccupation { get; set; }
        [DisplayName("Liquor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Liquor is required.")]
        public string Liquor { get; set; }
        [DisplayName("DateOfBirth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "DateOfBirth is required.")]
        public string DateOfBirth { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Values are missing.")]
        public string ExtraMessage { get; set; }
    }
}