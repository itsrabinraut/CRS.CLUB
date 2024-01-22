using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ProfileManagement;

namespace CRS.CLUB.REPOSITORY.ProfileManagement
{
    public interface IProfileManagementRepository
    {
        CommonDbResponse ChangePassword(PasswordCommon passwordCommon);
        UserProfileCommon GetUserProfile(UserProfileCommon userProfileCommon);
        CommonDbResponse UpdateUserProfile(UserProfileCommon userProfileCommon);
    }
}