using CRS.CLUB.REPOSITORY.ProfileManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ProfileManagement;

namespace CRS.CLUB.BUSINESS.ProfileManagement
{
    public class ProfileManagementBusiness : IProfileManagementBusiness
    {
        private readonly IProfileManagementRepository _repo;

        public ProfileManagementBusiness(ProfileManagementRepository profileManagementRepository) => this._repo = profileManagementRepository;

        public CommonDbResponse ChangePassword(PasswordCommon passwordCommon)
        {
            return _repo.ChangePassword(passwordCommon);
        }

        public UserProfileCommon GetUserProfile(UserProfileCommon userProfileCommon)
        {
            return _repo.GetUserProfile(userProfileCommon);
        }

        public CommonDbResponse UpdateUserProfile(UserProfileCommon userProfileCommon)
        {
            return _repo.UpdateUserProfile(userProfileCommon);
        }
    }
}