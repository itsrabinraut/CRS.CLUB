using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ScheduleManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.ScheduleManagement
{
    public interface IScheduleManagementBusiness
    {
        CommonDbResponse ManageSchedule(ManageScheduleCommon Request);
        List<ClubScheduleCommon> GetClubSchedule(string ClubId);
    }
}
