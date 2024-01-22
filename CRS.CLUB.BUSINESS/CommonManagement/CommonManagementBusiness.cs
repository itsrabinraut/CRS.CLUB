using CRS.CLUB.REPOSITORY;
using CRS.CLUB.REPOSITORY.CommonManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.CommonManagement
{
    public class CommonManagementBusiness : ICommonManagementBusiness
    {
        private ICommonManagementRepository _REPO;
        public CommonManagementBusiness()
        {
            _REPO = new CommonManagementRepository();
        }
        public Dictionary<string, string> GetDropDown(string Flag, string Extra1 = "", string Extra2 = "")
        {
            return _REPO.GetDropDown(Flag, Extra1, Extra2);
        }
    }
}
