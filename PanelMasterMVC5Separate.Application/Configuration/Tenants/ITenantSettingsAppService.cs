using System.Threading.Tasks;
using Abp.Application.Services;
using PanelMasterMVC5Separate.Configuration.Tenants.Dto;

namespace PanelMasterMVC5Separate.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
