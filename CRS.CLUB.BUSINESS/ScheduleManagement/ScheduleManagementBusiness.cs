using CRS.CLUB.REPOSITORY.ScheduleManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ScheduleManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.ScheduleManagement
{
    public class ScheduleManagementBusiness : IScheduleManagementBusiness
    {
        private readonly IScheduleManagementRepository _repo;
        public ScheduleManagementBusiness(ScheduleManagementRepository repo)
        {
            _repo = repo;
        }

        public List<ClubScheduleCommon> GetClubSchedule(string ClubId)
        {
            return _repo.GetClubSchedule(ClubId);
        }

        public CommonDbResponse ManageSchedule(ManageScheduleCommon Request)
        {
            return _repo.ManageSchedule(Request);
        }
    }
}
