using System.Threading.Tasks;
using Abp.Application.Services;
using PanelMasterMVC5Separate.Configuration.Host.Dto;

namespace PanelMasterMVC5Separate.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
