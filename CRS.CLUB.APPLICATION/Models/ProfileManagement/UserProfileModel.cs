using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace CRS.CLUB.APPLICATION.Models.UserProfileManagement
{
    public class UserProfileModel
    {
        #region BASIC INFO
        public string ClubNameEng { get; set; }
        public string ClubNameJap { get; set; }
        public string GroupName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public string CoverPicture { get; set; }
        public string ClubOpeningTime { get; set; }
        public string ClubClosingTime { get; set; }
        #endregion

        #region SOCIAL LINKS
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Tiktok { get; set; }
        public string Line { get; set; }
        public string Website { get; set; }
        public string GoogleMaps { get; set; }
        #endregion

        #region USER INFO
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        #endregion

        #region Input data
        public string InputRole { get; set; }
        public string LastOrderTime { get; set; }
        public string LastEntrySyokai { get; set; }
        public string Holiday { get; set; }
        public string Tax { get; set; }
        public string InputZip { get; set; }
        public string InputPrefecture { get; set; }
        public string InputCity { get; set; }
        public string InputStreet { get; set; }
        public string InputHouseNo { get; set; }
        public string LocationURL { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        #endregion

        #region "PLANS"
        public string RegularPrice { get; set; }
        public string NominationFee { get; set; }
        public string AccompanyingFee { get; set; }
        public string OnSiteNominationFee { get; set; }
        public string VariousDrinksFee { get; set; }
        #endregion
    }

    public class ChangePasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [Display(Name = "Current Password")]
        [MaxLength(16, ErrorMessage = "Password length should not exceed 16 digit"), MinLength(8, ErrorMessage = "Password minimum length must be 8")]
        public string OldPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "New password is required")]
        [Display(Name = "New Password")]
        [MaxLength(16, ErrorMessage = "Password length should not exceed 16 digit"), MinLength(8, ErrorMessage = "Password minimum length must be 8")]
        //[RegularExpression(@"^.*(?=.{8,16})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Must be 8 to 16 Length and must contain a-z,A-Z,0-9,@#$%^&+=")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [MaxLength(16, ErrorMessage = "Password length should not exceed 16 digit"), MinLength(8, ErrorMessage = "Password minimum length must be 8")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Password  Mismatch")]
        public string ConfirmPassword { get; set; }
    }
}