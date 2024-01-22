using CRS.CLUB.SHARED.NotificationManagement;
using System.Collections.Generic;

namespace CRS.CLUB.REPOSITORY.NotificationManagement
{
    public interface INotificationManagementRepository
    {
        List<NotificationDetailCommon> GetNotification(string AgentId);
        List<NotificationDetailCommon> GetAllNotification(ManageNotificationCommon Request);
    }
}
