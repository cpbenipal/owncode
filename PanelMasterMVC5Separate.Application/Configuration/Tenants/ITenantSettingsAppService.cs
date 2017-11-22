using System.Threading.Tasks;
using Abp.Application.Services;
using PanelMasterMVC5Separate.Configuration.Tenants.Dto;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();

        List<CurrencyDto> GetCurrencies();
        List<TimeZoneDto> GetTimeZones();
        List<CountriesDto> GetCountries();
        List<PlanDto> GetSignOnPlans();

        TenantCompanyDto GetCompanyInfo();
        TenantRegisterDto GetRegisteredInfo();

        Task UpdateTenantProfile(TenantRegisterDto input);
        Task UpdateTenantCompany(TenantCompanyDto input);
         
    }
}
