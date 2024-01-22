using System.Collections.Generic;

namespace CRS.CLUB.REPOSITORY.CommonManagement
{
    public interface ICommonManagementRepository
    {
        Dictionary<string, string> GetDropDown(string Flag, string Extra1 = "", string Extra2 = "");
    }
}
