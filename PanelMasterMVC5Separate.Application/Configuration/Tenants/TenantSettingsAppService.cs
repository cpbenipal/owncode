using System.Globalization;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Extensions;
using Abp.Json;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap.Configuration;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Configuration.Host.Dto;
using PanelMasterMVC5Separate.Configuration.Tenants.Dto;
using PanelMasterMVC5Separate.Security;
using PanelMasterMVC5Separate.Storage;
using PanelMasterMVC5Separate.Timing;
using Newtonsoft.Json;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using PanelMasterMVC5Separate.Vendors;
using System.Linq;
using System;
using PanelMasterMVC5Separate.MultiTenancy;
using Abp.UI;

namespace PanelMasterMVC5Separate.Configuration.Tenants
{
    [AbpAuthorize(AppPermissions.Pages_Administration_Tenant_Settings)]
    public class TenantSettingsAppService : PanelMasterMVC5SeparateAppServiceBase, ITenantSettingsAppService
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IAbpZeroLdapModuleConfig _ldapModuleConfig;
        private readonly ITimeZoneService _timeZoneService;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<CountryandCurrency> _currRepository;
        private readonly IRepository<Countries> _couRepository;
        private readonly IRepository<SignonPlans> _planRepository;
        private readonly IRepository<TenantProfile> _TenantProfile;
        private readonly IRepository<TenantPlanBillingDetails> _TenantPlanBillingDetails;


        public TenantSettingsAppService(
            IRepository<TenantProfile> tenantprofile,
              IRepository<TenantPlanBillingDetails> tenantplanbillingdetails,
            IMultiTenancyConfig multiTenancyConfig,
            IAbpZeroLdapModuleConfig ldapModuleConfig,
            ITimeZoneService timeZoneService,
            IBinaryObjectManager binaryObjectManager,
            IRepository<CountryandCurrency> currRepository,
            IRepository<Countries> couRepository,
            IRepository<SignonPlans> planRepository
             )
        {
            _TenantPlanBillingDetails = tenantplanbillingdetails;
            _TenantProfile = tenantprofile;
            _multiTenancyConfig = multiTenancyConfig;
            _ldapModuleConfig = ldapModuleConfig;
            _timeZoneService = timeZoneService;
            _binaryObjectManager = binaryObjectManager;
            _currRepository = currRepository;
            _couRepository = couRepository;
            _planRepository = planRepository;
        }

        #region Get Settings

        public async Task<TenantSettingsEditDto> GetAllSettings()
        {
            var settings = new TenantSettingsEditDto
            {
                UserManagement = await GetUserManagementSettingsAsync(),
                Security = await GetSecuritySettingsAsync()
            };

            if (!_multiTenancyConfig.IsEnabled || Clock.SupportsMultipleTimezone)
            {
                settings.General = await GetGeneralSettingsAsync();
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                settings.Email = await GetEmailSettingsAsync();

                if (_ldapModuleConfig.IsEnabled)
                {
                    settings.Ldap = await GetLdapSettingsAsync();
                }
                else
                {
                    settings.Ldap = new LdapSettingsEditDto { IsModuleEnabled = false };
                }
            }

            return settings;
        }

        private async Task<LdapSettingsEditDto> GetLdapSettingsAsync()
        {
            return new LdapSettingsEditDto
            {
                IsModuleEnabled = true,
                IsEnabled = await SettingManager.GetSettingValueAsync<bool>(LdapSettingNames.IsEnabled),
                Domain = await SettingManager.GetSettingValueAsync(LdapSettingNames.Domain),
                UserName = await SettingManager.GetSettingValueAsync(LdapSettingNames.UserName),
                Password = await SettingManager.GetSettingValueAsync(LdapSettingNames.Password),
            };
        }

        private async Task<EmailSettingsEditDto> GetEmailSettingsAsync()
        {
            return new EmailSettingsEditDto
            {
                DefaultFromAddress = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromAddress),
                DefaultFromDisplayName = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromDisplayName),
                SmtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host),
                SmtpPort = await SettingManager.GetSettingValueAsync<int>(EmailSettingNames.Smtp.Port),
                SmtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName),
                SmtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password),
                SmtpDomain = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Domain),
                SmtpEnableSsl = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.EnableSsl),
                SmtpUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials)
            };
        }

        private async Task<GeneralSettingsEditDto> GetGeneralSettingsAsync()
        {
            var settings = new GeneralSettingsEditDto();

            if (Clock.SupportsMultipleTimezone)
            {
                var timezone = await SettingManager.GetSettingValueForTenantAsync(TimingSettingNames.TimeZone, AbpSession.GetTenantId());

                settings.Timezone = timezone;
                settings.TimezoneForComparison = timezone;
            }

            var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);

            if (settings.Timezone == defaultTimeZoneId)
            {
                settings.Timezone = string.Empty;
            }

            return settings;
        }

        private async Task<TenantUserManagementSettingsEditDto> GetUserManagementSettingsAsync()
        {
            return new TenantUserManagementSettingsEditDto
            {
                AllowSelfRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.AllowSelfRegistration),
                IsNewRegisteredUserActiveByDefault = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault),
                IsEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin),
                UseCaptchaOnRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration)
            };
        }

        private async Task<SecuritySettingsEditDto> GetSecuritySettingsAsync()
        {
            var passwordComplexitySetting = await SettingManager.GetSettingValueAsync(AppSettings.Security.PasswordComplexity);
            var defaultPasswordComplexitySetting = await SettingManager.GetSettingValueForApplicationAsync(AppSettings.Security.PasswordComplexity);

            var settings = new SecuritySettingsEditDto
            {
                UseDefaultPasswordComplexitySettings = passwordComplexitySetting == defaultPasswordComplexitySetting,
                PasswordComplexity = JsonConvert.DeserializeObject<PasswordComplexitySetting>(passwordComplexitySetting),
                DefaultPasswordComplexity = JsonConvert.DeserializeObject<PasswordComplexitySetting>(defaultPasswordComplexitySetting),
                UserLockOut = await GetUserLockOutSettingsAsync()
            };

            settings.TwoFactorLogin = await GetTwoFactorLoginSettingsAsync();

            return settings;
        }

        private async Task<UserLockOutSettingsEditDto> GetUserLockOutSettingsAsync()
        {
            return new UserLockOutSettingsEditDto
            {
                IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.UserLockOut.IsEnabled),
                MaxFailedAccessAttemptsBeforeLockout = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout),
                DefaultAccountLockoutSeconds = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds)
            };
        }

        private Task<bool> IsTwoFactorLoginEnabledForApplicationAsync()
        {
            return SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled);
        }

        private async Task<TwoFactorLoginSettingsEditDto> GetTwoFactorLoginSettingsAsync()
        {
            var settings = new TwoFactorLoginSettingsEditDto
            {
                IsEnabledForApplication = await IsTwoFactorLoginEnabledForApplicationAsync()
            };

            if (_multiTenancyConfig.IsEnabled && !settings.IsEnabledForApplication)
            {
                return settings;
            }

            settings.IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled);
            settings.IsRememberBrowserEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled);

            if (!_multiTenancyConfig.IsEnabled)
            {
                settings.IsEmailProviderEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled);
                settings.IsSmsProviderEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled);
            }

            return settings;
        }

        #endregion

        #region Update Settings

        public async Task UpdateAllSettings(TenantSettingsEditDto input)
        {
            await UpdateUserManagementSettingsAsync(input.UserManagement);
            await UpdateSecuritySettingsAsync(input.Security);

            //Time Zone
            if (Clock.SupportsMultipleTimezone)
            {
                if (input.General.Timezone.IsNullOrEmpty())
                {
                    var defaultValue = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, defaultValue);
                }
                else
                {
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, input.General.Timezone);
                }
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                input.ValidateHostSettings();

                await UpdateEmailSettingsAsync(input.Email);

                if (_ldapModuleConfig.IsEnabled)
                {
                    await UpdateLdapSettingsAsync(input.Ldap);
                }
            }
        }

        private async Task UpdateLdapSettingsAsync(LdapSettingsEditDto input)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.IsEnabled, input.IsEnabled.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Domain, input.Domain.IsNullOrWhiteSpace() ? null : input.Domain);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.UserName, input.UserName.IsNullOrWhiteSpace() ? null : input.UserName);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Password, input.Password.IsNullOrWhiteSpace() ? null : input.Password);
        }

        private async Task UpdateEmailSettingsAsync(EmailSettingsEditDto input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Host, input.SmtpHost);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Port, input.SmtpPort.ToString(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UserName, input.SmtpUserName);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Password, input.SmtpPassword);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Domain, input.SmtpDomain);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.EnableSsl, input.SmtpEnableSsl.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UseDefaultCredentials, input.SmtpUseDefaultCredentials.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
        }

        private async Task UpdateUserManagementSettingsAsync(TenantUserManagementSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettings.UserManagement.AllowSelfRegistration,
                settings.AllowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture)
            );
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault,
                settings.IsNewRegisteredUserActiveByDefault.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture)
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin,
                settings.IsEmailConfirmationRequiredForLogin.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture)
            );
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettings.UserManagement.UseCaptchaOnRegistration,
                settings.UseCaptchaOnRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture)
            );
        }

        private async Task UpdateSecuritySettingsAsync(SecuritySettingsEditDto settings)
        {
            if (settings.UseDefaultPasswordComplexitySettings)
            {
                await SettingManager.ChangeSettingForTenantAsync(
                    AbpSession.GetTenantId(),
                    AppSettings.Security.PasswordComplexity,
                    settings.DefaultPasswordComplexity.ToJsonString()
                );
            }
            else
            {
                await SettingManager.ChangeSettingForTenantAsync(
                    AbpSession.GetTenantId(),
                    AppSettings.Security.PasswordComplexity,
                    settings.PasswordComplexity.ToJsonString()
                );
            }

            await UpdateUserLockOutSettingsAsync(settings.UserLockOut);
            await UpdateTwoFactorLoginSettingsAsync(settings.TwoFactorLogin);
        }

        private async Task UpdateUserLockOutSettingsAsync(UserLockOutSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.UserLockOut.IsEnabled, settings.IsEnabled.ToString(CultureInfo.InvariantCulture).ToLower());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds, settings.DefaultAccountLockoutSeconds.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout, settings.MaxFailedAccessAttemptsBeforeLockout.ToString());
        }

        private async Task UpdateTwoFactorLoginSettingsAsync(TwoFactorLoginSettingsEditDto settings)
        {
            if (_multiTenancyConfig.IsEnabled &&
                !await IsTwoFactorLoginEnabledForApplicationAsync()) //Two factor login can not be used by tenants if disabled by the host
            {
                return;
            }

            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled, settings.IsEnabled.ToString(CultureInfo.InvariantCulture).ToLower());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled, settings.IsRememberBrowserEnabled.ToString(CultureInfo.InvariantCulture).ToLower());

            if (!_multiTenancyConfig.IsEnabled)
            {
                //These settings can only be changed by host, in a multitenant application.
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled, settings.IsEmailProviderEnabled.ToString(CultureInfo.InvariantCulture).ToLower());
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled, settings.IsSmsProviderEnabled.ToString(CultureInfo.InvariantCulture).ToLower());
            }
        }

        #endregion

        #region Others

        public async Task ClearLogo()
        {
            var tenant = await GetCurrentTenantAsync();

            if (!tenant.HasLogo())
            {
                return;
            }

            var logoObject = await _binaryObjectManager.GetOrNullAsync(tenant.LogoId.Value);
            if (logoObject != null)
            {
                await _binaryObjectManager.DeleteAsync(tenant.LogoId.Value);
            }

            tenant.ClearLogo();
        }

        public async Task ClearCustomCss()
        {
            var tenant = await GetCurrentTenantAsync();

            if (!tenant.CustomCssId.HasValue)
            {
                return;
            }

            var cssObject = await _binaryObjectManager.GetOrNullAsync(tenant.CustomCssId.Value);
            if (cssObject != null)
            {
                await _binaryObjectManager.DeleteAsync(tenant.CustomCssId.Value);
            }

            tenant.CustomCssId = null;
        }

        #endregion

        #region

        public List<CurrencyDto> GetCurrencies()
        {
            var banks = _currRepository
                .GetAll()
                .OrderBy(p => p.CurrencyCode)
                .ToList();

            var cur = (from f in banks
                       select new CurrencyDto()
                       {
                           CurrencyCode = f.CurrencyCode,
                           CurrencyType = f.CountryAndCurrency
                       }
                ).ToList();

            return cur;
        }

        public List<CountriesDto> GetCountries()
        {
            var banks = _couRepository
                .GetAll()
                .OrderBy(p => p.Code)
                .ToList();

            var cur = (from f in banks
                       select new CountriesDto()
                       {
                           Code = f.Code,
                           Country = f.Country
                       }
                ).ToList();

            return cur;
        }
        public List<PlanDto> GetSignOnPlans()
        {
            var banks = _planRepository
                .GetAll()
                .OrderBy(p => p.PlanName)
                .ToList();

            var cur = (from f in banks
                       select new PlanDto()
                       {
                           Id = f.Id,
                           PlanName = f.PlanName
                       }
                ).ToList();

            return cur;
        }

        public List<TimeZoneDto> GetTimeZones()
        {
            var cur = (from f in TimeZoneInfo.GetSystemTimeZones()
                       select new TimeZoneDto()
                       {
                           DisplayName = f.DisplayName,
                           Id = f.Id
                       }).ToList();
            
            return cur;
        }

        public TenantRegisterDto GetRegisteredInfo()
        {
            try
            {
                var reginfo = _TenantPlanBillingDetails.FirstOrDefault(x => x.TenantId == AbpSession.TenantId);
                TenantRegisterDto retinfo = null;
                if (reginfo != null)
                {
                    retinfo = new TenantRegisterDto()
                    {
                        TenantId = reginfo.TenantId,
                        planId = reginfo.planId,
                        CurrentPlan = _planRepository.FirstOrDefault(x => x.Id == reginfo.planId).PlanName,
                        CardHoldersName = reginfo.CardHoldersName,
                        CardNumber = reginfo.CardNumber,
                        CardExpiration = reginfo.CardExpiration,
                        CVV = reginfo.CVV,
                        payment = reginfo.PaymentOptions
                    };
                    string[] paymentoption = retinfo.payment.Split(',');

                    if (paymentoption.Length == 2)
                    {
                        retinfo.paymentoption1 = true;
                        retinfo.paymentoption2 = true;
                    }
                    else
                    {
                        if (paymentoption[0] == "1")
                            retinfo.paymentoption1 = true;
                        else if (paymentoption[0] == "2")
                            retinfo.paymentoption2 = true;
                    }
                    retinfo.FullName = _TenantProfile.FirstOrDefault(x => x.TenantId == AbpSession.TenantId).FullName;
                }
                return retinfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TenantCompanyDto GetCompanyInfo()
        {
            try
            {
                var reginfo = _TenantProfile.FirstOrDefault(x => x.TenantId == AbpSession.TenantId);
                TenantCompanyDto retinfo = null;
                if (reginfo != null)
                {
                    retinfo = new TenantCompanyDto()
                    {
                        TenantId = reginfo.TenantId,
                        address = reginfo.Address,
                        cellNumber = reginfo.CellNumber,
                        phoneNumber = reginfo.PhoneNumber,                        
                        city = reginfo.City,
                        companyName = reginfo.CompanyName,
                        companyRegistrationNo = reginfo.CompanyRegistrationNo,
                        companyVatNo = reginfo.CompanyVatNo,
                        country = reginfo.CountryCode,
                        countryName = reginfo.CountryCode != null ? _couRepository.FirstOrDefault(x => x.Code == reginfo.CountryCode).Country : "",
                        currency = reginfo.CurrencyCode,
                        currencyName = reginfo.CurrencyCode != null ? _currRepository.FirstOrDefault(x => x.CurrencyCode == reginfo.CurrencyCode).CountryAndCurrency : "",
                        faximileeNumber = reginfo.FaximileeNumber,
                        invoicingInstruction = reginfo.InvoicingInstruction,
                        timezone = reginfo.Timezone,
                        VatorTax  = reginfo.VatorTax
                    };
                }
                return retinfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task UpdateTenantProfile(TenantRegisterDto reginfo)
        {
            if (reginfo.paymentoption1 == false && reginfo.paymentoption2 == false)
            {
                throw new UserFriendlyException("Payment options is required");
            }
            else
            {
                var current = await _TenantPlanBillingDetails.FirstOrDefaultAsync(x => x.TenantId == AbpSession.TenantId);
                if (current != null)
                {
                    string payment = "";
                    if (reginfo.paymentoption1 == true)
                    {
                        payment = "1";
                    }
                    if (reginfo.paymentoption2 == true)
                    {
                        payment += (payment != "") ? "," : "";
                        payment += "2";
                    }

                    current.TenantId = reginfo.TenantId;
                    current.planId = reginfo.planId;
                    current.CardHoldersName = reginfo.CardHoldersName;
                    current.CardNumber = reginfo.CardNumber;
                    current.CardExpiration = reginfo.CardExpiration;
                    current.CVV = reginfo.CVV;
                    current.PaymentOptions = payment;
                    await _TenantPlanBillingDetails.UpdateAsync(current);
                }
            }
        }

        public async Task UpdateTenantCompany(TenantCompanyDto reginfo)
        {
            var current = await _TenantProfile.FirstOrDefaultAsync(x => x.TenantId == AbpSession.TenantId);
            if (current != null)
            {
                current.TenantId = reginfo.TenantId;
                current.Address = reginfo.address;
                current.PhoneNumber = reginfo.phoneNumber.Replace("(","").Replace(")", "").Replace("-", "").Replace(" ", "");
                current.CellNumber = reginfo.cellNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                current.City = reginfo.city;
                current.CompanyName = reginfo.companyName;
                current.CompanyRegistrationNo = reginfo.companyRegistrationNo;
                current.CompanyVatNo = reginfo.companyVatNo;
                current.CountryCode = reginfo.country;
                current.FaximileeNumber = reginfo.faximileeNumber;
                current.InvoicingInstruction = reginfo.invoicingInstruction;
                current.Timezone = reginfo.timezone;
                current.CurrencyCode = reginfo.currency;
                current.VatorTax = reginfo.VatorTax;
                await _TenantProfile.UpdateAsync(current);
            }
        }

        #endregion
    }
}