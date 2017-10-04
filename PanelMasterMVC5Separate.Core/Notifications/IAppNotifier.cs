using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.MultiTenancy;

namespace PanelMasterMVC5Separate.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}
