using CRS.CLUB.REPOSITORY.NotificationManagement;
using CRS.CLUB.SHARED.NotificationManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.NotificationManagement
{
    public class NotificationManagementBusiness : INotificationManagementBusiness
    {
        private readonly INotificationManagementRepository _repo;
        public NotificationManagementBusiness()
        {
            _repo = new NotificationManagementRepository();
        }

        public List<NotificationDetailCommon> GetAllNotification(ManageNotificationCommon Request)
        {
            return _repo.GetAllNotification(Request);
        }

        public List<NotificationDetailCommon> GetNotification(string AgentId)
        {
            return _repo.GetNotification(AgentId);
        }
    }
}
