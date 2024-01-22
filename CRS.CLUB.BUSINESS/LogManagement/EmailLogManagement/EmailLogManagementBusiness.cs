using CRS.CLUB.REPOSITORY.LogManagement.EmailLogManagement;

namespace CRS.CLUB.BUSINESS.LogManagement.EmailLogManagement
{
    public class EmailLogManagementBusiness : IEmailLogManagementBusiness
    {
        IEmailLogManagementRepository _REPO;
        public EmailLogManagementBusiness(EmailLogManagementRepository REPO)
        {
            _REPO = REPO;
        }
    }
}
