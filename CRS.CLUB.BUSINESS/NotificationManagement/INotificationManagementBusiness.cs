using CRS.CLUB.SHARED.NotificationManagement;
using System.Collections.Generic;

namespace CRS.CLUB.BUSINESS.NotificationManagement
{
    public interface INotificationManagementBusiness
    {
        List<NotificationDetailCommon> GetNotification(string AgentId);
        List<NotificationDetailCommon> GetAllNotification(ManageNotificationCommon Request);
    }
}
