using System.Threading.Tasks;
using Abp.Application.Services;
using PanelMasterMVC5Separate.Configuration.Tenants.Dto;
using System.Collections.Generic;
using PanelMasterMVC5Separate.MultiTenancy;
using System;

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

        Task SaveAsync(TenantCompanyLogo file);
        Task<TenantCompanyLogo> GetOrNullAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
