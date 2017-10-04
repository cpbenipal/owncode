using Abp.Application.Services;
using PanelMasterMVC5Separate.Tenants.Dashboard.Dto;

namespace PanelMasterMVC5Separate.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();
    }
}
