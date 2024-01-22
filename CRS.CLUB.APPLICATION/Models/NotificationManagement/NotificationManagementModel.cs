namespace CRS.CLUB.APPLICATION.Models.NotificationManagement
{
    public class NotificationDetailModel
    {
        public string NotificationId { get; set; }
        public string NotificationTo { get; set; }
        public string NotificationType { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public string NotificationStatus { get; set; }
        public string NotificationReadStatus { get; set; }
        public string NotificationCount { get; set; }
    }
}