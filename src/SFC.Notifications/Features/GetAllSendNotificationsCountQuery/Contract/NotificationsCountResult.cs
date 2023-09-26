using SFC.SharedKernel;

namespace SFC.Notifications.Features.GetAllSendNotificationsCountQuery.Contract
{
    public class NotificationsCountResult
    {
        public LoginName LoginName { get; set; }
        public int Count { get; set; }
    }
}