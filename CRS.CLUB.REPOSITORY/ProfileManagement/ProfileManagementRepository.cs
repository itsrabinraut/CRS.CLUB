using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ProfileManagement;

namespace CRS.CLUB.REPOSITORY.ProfileManagement
{
    public class ProfileManagementRepository : IProfileManagementRepository
    {
        private readonly RepositoryDao _dao;

        public ProfileManagementRepository(RepositoryDao dao) => this._dao = dao;

        public CommonDbResponse ChangePassword(PasswordCommon passwordCommon)
        {
            string sql = "exec sproc_club_profile_management";
            sql += " @Flag='cp'";
            sql += " ,@Session=" + _dao.FilterString(passwordCommon.Session);
            sql += " ,@CurrentPassword=" + _dao.FilterString(passwordCommon.OldPassword);
            sql += " ,@NewPassword=" + _dao.FilterString(passwordCommon.ConfirmPassword);
            sql += " ,@ActionPlatform=" + _dao.FilterString(passwordCommon.BrowserInfo);
            sql += " ,@ActionIP=" + _dao.FilterString(passwordCommon.IPAddress);
            sql += " ,@ActionUser=" + _dao.FilterString(passwordCommon.ActionUser);
            return _dao.ParseCommonDbResponse(sql);
        }

        public UserProfileCommon GetUserProfile(UserProfileCommon userProfileCommon)
        {
            string sql = "sproc_club_profile_management";
            sql += " @flag= 's'";
            sql += ", @AgentId=" + _dao.FilterString(userProfileCommon.AgentId);
            sql += ", @UserId=" + _dao.FilterString(userProfileCommon.ActionUserId);
            sql += ", @ActionUser=" + _dao.FilterString(userProfileCommon.ActionUser);
            var dr = _dao.ExecuteDataRow(sql);
            if (dr != null)
            {
                var Code = dr["Code"]?.ToString();
                var Message = dr["Message"]?.ToString();
                if (!string.IsNullOrEmpty(Code) && Code == "0")
                {
                    return new UserProfileCommon()
                    {

                        Code = ResponseCode.Success,
                        Message = "Success",
                        ClubNameEng = dr["ClubNameEng"]?.ToString(),
                        ClubNameJap = dr["ClubNameJap"]?.ToString(),
                        EmailAddress = dr["EmailAddress"]?.ToString(),
                        PhoneNumber = dr["MobileNumber"]?.ToString(),
                        GroupName = dr["GroupName"]?.ToString(),
                        Bio = dr["Bio"]?.ToString(),
                        LocationURL = dr["LocationURL"]?.ToString(),
                        Longitude = dr["Longitude"]?.ToString(),
                        Latitude = dr["Latitude"]?.ToString(),
                        UserName = dr["UserName"]?.ToString(),
                        ProfilePicture = dr["ProfilePhoto"]?.ToString(),
                        CoverPicture = dr["CoverPhoto"]?.ToString(),
                        ClubOpeningTime = dr["ClubOpeningTime"]?.ToString(),
                        ClubClosingTime = dr["ClubClosingTime"]?.ToString(),
                        InputRole = dr["InputRole"]?.ToString(),
                        LastOrderTime = dr["LastOrderTime"]?.ToString(),
                        LastEntrySyokai = dr["LastEntrySyokai"]?.ToString(),
                        Holiday = dr["Holiday"]?.ToString(),
                        Tax = dr["Tax"]?.ToString(),
                        InputZip = dr["InputZip"]?.ToString(),
                        InputPrefecture = dr["InputPrefecture"]?.ToString(),
                        InputCity = dr["InputCity"]?.ToString(),
                        InputStreet = dr["InputStreet"]?.ToString(),
                        InputHouseNo = dr["InputHouseNo"]?.ToString(),
                        FirstName = dr["FirstName"]?.ToString(),
                        LastName = dr["LastName"]?.ToString(),
                        Instagram = dr["InstagramLink"]?.ToString(),
                        Twitter = dr["TwitterLink"]?.ToString(),
                        Tiktok = dr["TiktokLink"]?.ToString(),
                        Website = dr["WebsiteLink"]?.ToString(),
                        GoogleMaps = dr["LocationUrl"]?.ToString(),
                        RegularPrice = dr["RegularPrice"].ToString(),
                        NominationFee = dr["NominationFee"].ToString(),
                        OnSiteNominationFee = dr["OnSiteNominationFee"].ToString(),
                        AccompanyingFee = dr["AccompanyingFee"].ToString(),
                        VariousDrinksFee = dr["VariousDrinksFee"].ToString(),
                        Line = dr["Line"].ToString(),

                    };
                }
                return new UserProfileCommon()
                {
                    Code = ResponseCode.Warning,
                    Message = Message ?? "Failed"
                };
            }
            return new UserProfileCommon
            {
                Code = ResponseCode.Failed,
                Message = "Something went wrong. Please Try again later"
            };
        }

        public CommonDbResponse UpdateUserProfile(UserProfileCommon userProfileCommon)
        {
            string sql = "sproc_club_profile_management";
            sql += " @flag= 'u'";
            sql += ", @UserId=" + _dao.FilterString(userProfileCommon.ActionUserId);
            sql += ", @AgentId=" + _dao.FilterString(userProfileCommon.AgentId);
            sql += ", @ClubNameEng=" + _dao.FilterString(userProfileCommon.ClubNameEng);
            sql += ", @ClubNameJap= N" + _dao.FilterString(userProfileCommon.ClubNameJap);
            sql += ", @GroupName=" + _dao.FilterString(userProfileCommon.GroupName);
            sql += ", @Bio=N" + _dao.FilterString(userProfileCommon.Bio);
            sql += ", @CoverPicture=" + _dao.FilterString(userProfileCommon.CoverPicture);
            sql += ", @ProfilePicture=" + _dao.FilterString(userProfileCommon.ProfilePicture);
            sql += ", @InputRole=" + _dao.FilterString(userProfileCommon.InputRole);
            sql += ", @LastOrderTime=" + _dao.FilterString(userProfileCommon.LastOrderTime);
            sql += ", @LastEntrySyokai=" + _dao.FilterString(userProfileCommon.LastEntrySyokai);
            sql += ", @Holiday=N" + _dao.FilterString(userProfileCommon.Holiday);
            sql += ", @Tax=" + _dao.FilterString(userProfileCommon.Tax);
            sql += ", @InputZip=" + _dao.FilterString(userProfileCommon.InputZip);
            sql += ", @InputPrefecture=" + _dao.FilterString(userProfileCommon.InputPrefecture);
            sql += ", @InputCity=N" + _dao.FilterString(userProfileCommon.InputCity);
            sql += ", @InputStreet=N" + _dao.FilterString(userProfileCommon.InputStreet);
            sql += ", @InputHouseNo=" + _dao.FilterString(userProfileCommon.InputHouseNo);
            sql += ", @Website=" + _dao.FilterString(userProfileCommon.Website);
            sql += ", @TikTok=" + _dao.FilterString(userProfileCommon.Tiktok);
            sql += ", @Twitter=" + _dao.FilterString(userProfileCommon.Twitter);
            sql += ", @Instagram=" + _dao.FilterString(userProfileCommon.Instagram);
            sql += ", @ActionUser=" + _dao.FilterString(userProfileCommon.ActionUser);
            sql += ", @ActionIp=" + _dao.FilterString(userProfileCommon.ActionIP);
            sql += ", @ActionPlatform=" + _dao.FilterString(userProfileCommon.ActionPlatform);
            sql += ",@GoogleMaps=" + _dao.FilterString(userProfileCommon.GoogleMaps);
            sql += ",@WorkingHoursFrom=" + _dao.FilterString(userProfileCommon.ClubOpeningTime);
            sql += ",@WorkingHoursTo=" + _dao.FilterString(userProfileCommon.ClubClosingTime);
            sql += ",@RegularPrice=" + _dao.FilterString(userProfileCommon.RegularPrice);
            sql += ",@NominationFee=" + _dao.FilterString(userProfileCommon.NominationFee);
            sql += ",@AccompanyingFee=" + _dao.FilterString(userProfileCommon.AccompanyingFee);
            sql += ",@OnSiteNominationFee=" + _dao.FilterString(userProfileCommon.OnSiteNominationFee);
            sql += ",@VariousDrinksFee=" + _dao.FilterString(userProfileCommon.VariousDrinksFee);
            sql += ",@Line=" + _dao.FilterString(userProfileCommon.Line);
            return _dao.ParseCommonDbResponse(sql);
        }
    }
}