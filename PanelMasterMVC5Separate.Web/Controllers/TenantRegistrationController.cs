using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero.Configuration;
using PanelMasterMVC5Separate.Configuration;
using PanelMasterMVC5Separate.Debugging;
using PanelMasterMVC5Separate.Web.Models.TenantRegistration;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using Abp.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.Editions;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Notifications;
using PanelMasterMVC5Separate.Web.Auth;
using System.Collections.Generic;
using Newtonsoft.Json;
using PanelMasterMVC5Separate.Security;
using Abp.Web.Security.AntiForgery;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace PanelMasterMVC5Separate.Web.Controllers
{
    public class TenantRegistrationController : PanelMasterMVC5SeparateControllerBase
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly LogInManager _logInManager;
        private readonly EditionManager _editionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public TenantRegistrationController(
            IMultiTenancyConfig multiTenancyConfig,
            TenantManager tenantManager,
            EditionManager editionManager,
            IAppNotifier appNotifier,
            UserManager userManager,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            LogInManager logInManager, ITenantAppService signonplansrepository)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _tenantManager = tenantManager;
            _editionManager = editionManager;
            _appNotifier = appNotifier;
            _userManager = userManager;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _logInManager = logInManager;
        }

        public ActionResult Index()
        {
            CheckTenantRegistrationIsEnabled();

            ViewBag.UseCaptcha = UseCaptchaOnRegistration();
            ViewBag.PasswordComplexitySetting = SettingManager.GetSettingValue(AppSettings.Security.PasswordComplexity).Replace("\"", "");

            return View();
        }
        public ActionResult Chooseyourplan()
        {
            List<SignonPlans> data = _tenantManager.GetTenantPlans();

            return View(data);
        }
        /// <summary>
        /// Step 1 Registration Details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult newtenant(int Id)
        {
            CheckTenantRegistrationIsEnabled();
            ViewBag.UseCaptcha = UseCaptchaOnRegistration();
            ViewBag.PasswordComplexitySetting = SettingManager.GetSettingValue(AppSettings.Security.PasswordComplexity).Replace("\"", "");
            return View(new RegisterDetail()
            {
                PlanId = Id
            });
        }

        [Abp.Runtime.Validation.DisableValidation]
        [HttpPost]
        public ActionResult newtenant(RegisterDetail detail)
        {
            TempData["RegisterDetail"] = detail;
            TempData.Keep("RegisterDetail");
            return RedirectToAction("otpconfirm");
        }
        public ActionResult otpconfirm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult otpconfirm(OtpConfirmation detail)
        {
            if (detail.OTP.Equals("00-00"))
            {
                //   TempData["RegisterDetail"] = TempData["RegisterDetail"];
                return RedirectToAction("CountryAndBilling");
            }
            else
            {
                ViewBag.ErrorMessage = L("OTPMismatched");
                return View();
            }
        }
        public ActionResult CountryAndBilling()
        {
            return View("CountryAndBilling", new CountryandBillingDetail
            {
                listCountries = _tenantManager.GetCountries(),
                listCurrencies = _tenantManager.GetCurrencies()
            });
        }

        [HttpPost]
        public ActionResult CountryAndBilling(CountryandBillingDetail countryandBillingDetail)
        {
            TempData.Add("CountryAndBilling", countryandBillingDetail);
            TempData.Keep("CountryAndBilling");

            return RedirectToAction("confirm");
        }
        public ActionResult confirm()
        {
            ViewBag.UseCaptcha = UseCaptchaOnRegistration();
            ViewBag.PasswordComplexitySetting = SettingManager.GetSettingValue(AppSettings.Security.PasswordComplexity).Replace("\"", "");

            RegisterDetail registerDetail = null;
            if (TempData["RegisterDetail"] != null)
            {
                registerDetail = (RegisterDetail)TempData.Peek("RegisterDetail");
            }
            CountryandBillingDetail countryandBillingDetail = null;
            if (TempData["CountryAndBilling"] != null)
            {
                countryandBillingDetail = (CountryandBillingDetail)TempData.Peek("CountryAndBilling");
            }
            TenantRegistrationViewModel model = null;
            // in case loss of data
            if (registerDetail != null && countryandBillingDetail != null)
            {
                model = new TenantRegistrationViewModel();
                model.TenancyName = registerDetail.TenancyName;
                model.LoginName = registerDetail.LoginName;
                model.AdminEmailAddress = registerDetail.AdminEmailAddress;
                model.AdminPassword = registerDetail.AdminPassword;
                model.FullName = registerDetail.FullName;
                model.CellNumber = registerDetail.CellNumber;
                model.BillingCountryCode = countryandBillingDetail.BillingCountryCode;
                model.CurrencyCode = countryandBillingDetail.CurrencyCode;
                model.PlanId = registerDetail.PlanId;
                model.CurrentPlan = _tenantManager.GetCurrentTenantPlan(model.PlanId);
                model.CardHoldersName = countryandBillingDetail.CardHoldersName;
                model.CardNumber = countryandBillingDetail.CardNumber;
                model.CVV = countryandBillingDetail.CVV;
                model.CardExpiration = countryandBillingDetail.CardExpiration;
                model.payment = string.Join(",", countryandBillingDetail.payment);

                if (countryandBillingDetail.payment.Length == 2)
                {
                    if (countryandBillingDetail.payment[0] == "1")
                        model.paymentoption1 = L("AutoPaywiththisCreditCard");
                    if (countryandBillingDetail.payment[1] == "2")
                        model.paymentoption2 = L("Emailmemonthlybilling");
                }
                else
                {
                    if (countryandBillingDetail.payment[0] == "1")
                        model.paymentoption1 = L("AutoPaywiththisCreditCard");
                    else if (countryandBillingDetail.payment[0] == "2")
                        model.paymentoption2 = L("Emailmemonthlybilling");
                }
                
                TempData["model"] = model;
                TempData.Keep("model");
            }
            else
            {
                model = (TenantRegistrationViewModel)TempData.Peek("model");
            }
            return View("confirm", model);
        }
         

        [Abp.Runtime.Validation.DisableValidation]
        [HttpPost]
        [UnitOfWork]
        public virtual async Task<ActionResult> confirm(TenantRegistrationViewModel model)
        {
            try
            {
               // if (model == null)
                {
                    model = (TenantRegistrationViewModel)TempData.Peek("model");
                }
                CheckTenantRegistrationIsEnabled();
                if (UseCaptchaOnRegistration())
                {
                    var recaptchaHelper = this.GetRecaptchaVerificationHelper();
                    if (recaptchaHelper.Response.IsNullOrEmpty())
                    {
                        throw new UserFriendlyException(L("CaptchaCanNotBeEmpty"));
                    }

                    if (recaptchaHelper.VerifyRecaptchaResponse() != RecaptchaVerificationResult.Success)
                    {
                        throw new UserFriendlyException(L("IncorrectCaptchaAnswer"));
                    }
                }

                //Getting host-specific settings
                var isNewRegisteredTenantActiveByDefault = await SettingManager.GetSettingValueForApplicationAsync<bool>(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault);
                var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);
                var defaultEditionIdValue = await SettingManager.GetSettingValueForApplicationAsync(AppSettings.TenantManagement.DefaultEdition);
                int? defaultEditionId = null;

                if (!string.IsNullOrEmpty(defaultEditionIdValue) && (await _editionManager.FindByIdAsync(Convert.ToInt32(defaultEditionIdValue)) != null))
                {
                    defaultEditionId = Convert.ToInt32(defaultEditionIdValue);
                }
                //  model.PlanId = Convert.ToInt32(ViewBag.PlanId);
                CurrentUnitOfWork.SetTenantId(null);

                var tenantId = await _tenantManager.CreateWithAdminUserAsync(
                 model.TenancyName,
                 model.LoginName,
                 model.AdminPassword,
                 model.AdminEmailAddress,
                 model.FullName,
                 model.CellNumber,
                 //model.PhoneNumber,
                 //model.CompanyRegistrationNo,
                 //model.CompanyVatNo,
                 //model.Address,
                 //model.City,
                 //model.CountryCode,
                 //model.InvoicingInstruction,
                 model.BillingCountryCode,
                 model.CurrencyCode,
                 //model.Timezone,
                 model.CardHoldersName,
                 model.CardNumber,
                 model.CardExpiration,
                 model.CVV,
                 model.payment,
                 model.PlanId,
                 null,
                 true,
                 defaultEditionId,
                 false,
                 true);



                ViewBag.UseCaptcha = UseCaptchaOnRegistration();

                var tenant = await _tenantManager.GetByIdAsync(tenantId);
                await _appNotifier.NewTenantRegisteredAsync(tenant);

                CurrentUnitOfWork.SetTenantId(tenant.Id);

                var user = await _userManager.FindByNameAsync(AbpUserBase.AdminUserName);

                //Directly login if possible
                if (tenant.IsActive && user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin))
                {
                    var loginResult = await GetLoginResultAsync(user.UserName, model.AdminPassword , tenant.TenancyName);

                    if (loginResult.Result == AbpLoginResultType.Success)
                    {
                        await SignInAsync(loginResult.User, loginResult.Identity);
                        return Redirect("~/Application#!/tenant/settings");
                    }

                    Logger.Warn("New registered user could not be login. This should not be normally. login result: " + loginResult.Result);
                }
                return View("RegisterResult", new TenantRegisterResultViewModel
                {
                    TenancyName = model.TenancyName,
                    Name = model.LoginName,
                    UserName = AbpUserBase.AdminUserName,
                    EmailAddress = model.AdminEmailAddress,
                    IsActive = isNewRegisteredTenantActiveByDefault,
                    IsEmailConfirmationRequired = isEmailConfirmationRequiredForLogin
                });

            }
            catch (UserFriendlyException ex)
            {
                ViewBag.UseCaptcha = UseCaptchaOnRegistration();
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.PlanId = model.PlanId;
                return View(model);
            }
        }

        private bool CheckActivationCode(string TaxID)
        {
            try
            {
                if (!string.IsNullOrEmpty(TaxID))
                {
                    // SMS service in future
                    if (TaxID == "00-00")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsSelfRegistrationEnabled()
        {
            return SettingManager.GetSettingValueForApplication<bool>(AppSettings.TenantManagement.AllowSelfRegistration);
        }

        private void CheckTenantRegistrationIsEnabled()
        {
            if (!IsSelfRegistrationEnabled())
            {
                throw new UserFriendlyException(L("SelfTenantRegistrationIsDisabledMessage_Detail"));
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                throw new UserFriendlyException(L("MultiTenancyIsNotEnabled"));
            }
        }

        private bool UseCaptchaOnRegistration()
        {
            if (DebugHelper.IsDebug)
            {
                return false;
            }

            return SettingManager.GetSettingValueForApplication<bool>(AppSettings.TenantManagement.UseCaptchaOnRegistration);
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            AuthenticationManager.SignOutAllAndSignIn(identity, rememberMe);
        }
    }
}