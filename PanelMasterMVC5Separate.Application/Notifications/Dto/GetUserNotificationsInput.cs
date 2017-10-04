using Abp.Notifications;
using PanelMasterMVC5Separate.Dto;

namespace PanelMasterMVC5Separate.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}