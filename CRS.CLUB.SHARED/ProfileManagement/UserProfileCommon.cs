namespace CRS.CLUB.SHARED.ProfileManagement
{
    public class UserProfileCommon : Common
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
        public string Website { get; set; }
        public string GoogleMaps { get; set; }
        public string Line { get; set; }
        #endregion

        #region USER INFO
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
    public class PasswordCommon
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserId { get; set; }
        public string Session { get; set; }
        public string IPAddress { get; set; }
        public string BrowserInfo { get; set; }
        public string ActionUser { get; set; }
    }
}