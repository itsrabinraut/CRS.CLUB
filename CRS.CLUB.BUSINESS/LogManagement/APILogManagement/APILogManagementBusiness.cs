using CRS.CLUB.REPOSITORY.LogManagement.APILogManagement;

namespace CRS.CLUB.BUSINESS.LogManagement.APILogManagement
{
    public class APILogManagementBusiness : IAPILogManagementBusiness
    {
        IAPILogManagementRepository _REPO;
        public APILogManagementBusiness(APILogManagementRepository REPO)
        {
            _REPO = REPO;
        }
    }
}
