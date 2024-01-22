using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ScheduleManagement;
using System.Collections.Generic;

namespace CRS.CLUB.REPOSITORY.ScheduleManagement
{
    public interface IScheduleManagementRepository
    {
        CommonDbResponse ManageSchedule(ManageScheduleCommon Request);
        List<ClubScheduleCommon> GetClubSchedule(string ClubId);
    }
}
