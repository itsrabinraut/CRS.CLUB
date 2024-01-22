using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ProfileManagement;

namespace CRS.CLUB.BUSINESS.ProfileManagement
{
    public interface IProfileManagementBusiness
    {
        CommonDbResponse ChangePassword(PasswordCommon passwordCommon);
        UserProfileCommon GetUserProfile(UserProfileCommon userProfileCommon);
        CommonDbResponse UpdateUserProfile(UserProfileCommon userProfileCommon);
    }
}