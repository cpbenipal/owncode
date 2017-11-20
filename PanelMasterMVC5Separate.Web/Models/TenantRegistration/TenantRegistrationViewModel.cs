using System.ComponentModel.DataAnnotations;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.MultiTenancy;
using System.ComponentModel;
using PanelMasterMVC5Separate.Security;
using System.Collections.Generic;
using Abp.Extensions;
using PanelMasterMVC5Separate.Validation;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.Collections.ObjectModel;

namespace PanelMasterMVC5Separate.Web.Models.TenantRegistration
{
    public class TenantRegistrationViewModel
    {
        public int PlanId { get; set; }

        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }   

        [Required]
        [Phone, StringLength(User.MaxPhoneNumberLength)]
        public string CellNumber { get; set; }

        [Required]
        [Phone, StringLength(User.MaxPhoneNumberLength)]
        public string FaximileeNumber { get; set; }
        
        public string Address { get; set; }

        [Required]
        [Display(Name = "City/Town"), StringLength(User.MaxSurnameLength)]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }         

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public string Timezone { get; set; }   
         
        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string CompanyRegistrationNo { get; set; }

        [Required] 
        public string CompanyVatNo { get; set; }

        public string InvoicingInstruction { get; set; } 

        [Required(ErrorMessage = "Card Holders Name is required")]
        [DisplayName("Card Holders Name")]
        [StringLength(160)]
        public string CardHoldersName { get; set; }

        [Required]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [DisplayName("Expiration(MM/YYYY)")]
        public string CardExpiration { get; set; }

        [Required]
        [StringLength(4)]
        public string CVV { get; set; }

        public string payment { get; set; }

        public string paymentoption1 { get; set; }
        public string paymentoption2 { get; set; }

        public List<Countries> listCountries { get; set; }
        public List<CountryandCurrency> listCurrencies { get; set; }
        public ReadOnlyCollection<TimeZoneInfo> listTimezones { get; internal set; }
        public List<SignonPlans> listSignOnPlans { get; set; }

        public string LoginName { get; set; }
        public string AdminPassword { get; set; }
        public string AdminEmailAddress { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public string BillingCountryCode { get; set; }
        public string CurrentPlan { get; set; }
    }
    public class OtpConfirmation
    {
        [Required]
        [StringLength(5)]
        public string OTP { get; set; } 
    }
    public class CountryandBillingDetail
    {
        [Required]
        public string BillingCountryCode { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required(ErrorMessage = "Card Holders Name is required")]
        [DisplayName("Card Holders Name")]
        [StringLength(160)]
        public string CardHoldersName { get; set; }

        [Required]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [DisplayName("Expiration(MM/YYYY)")]
        public string CardExpiration { get; set; }

        [Required]
        [StringLength(4)]
        public string CVV { get; set; }

        public string[] payment { get; set; }

        public List<Countries> listCountries { get; set; }

        public List<CountryandCurrency> listCurrencies { get; set; }         

    }
    public class TenantRegistrationView : IValidatableObject
    {
        [Required]
        public int PlanId { get; set; }
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string LoginName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        [Required]
        [StringLength(User.MaxPlainPasswordLength)]
        public string AdminPassword { get; set; }

        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!LoginName.IsNullOrEmpty())
            {
                if (!LoginName.Equals(AdminEmailAddress) && new ValidationHelper().IsEmail(LoginName))
                {
                    yield return new ValidationResult("Username cannot be an email address unless it's same with your email address !");
                }
            }
        }
        [Required]
        [StringLength(User.MaxNameLength)]
        public string FullName { get; set; }

        [Required]
        [Phone, StringLength(User.MaxPhoneNumberLength)]
        public string CellNumber { get; set; }

        [Required]
        public string BillingCountryCode { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required(ErrorMessage = "Card Holders Name is required")]
        [DisplayName("Card Holders Name")]
        [StringLength(160)]
        public string CardHoldersName { get; set; }

        [Required]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [DisplayName("Expiration(MM/YYYY)")]
        public string CardExpiration { get; set; }

        [Required]
        [StringLength(4)]
        public string CVV { get; set; }

        public string payment { get; set; }

        public string CurrentPlan { get; set; }

        public List<Countries> listCountries { get; set; }

        public List<CountryandCurrency> listCurrencies { get; set; }

        [Required]
        [StringLength(5)]
        public string OTP { get; set; }
    }

    public class RegisterDetail : IValidatableObject
    {
        [Required]
        public int PlanId { get; set; }
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string LoginName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        [Required]
        [StringLength(User.MaxPlainPasswordLength)]
        public string AdminPassword { get; set; }

        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!LoginName.IsNullOrEmpty())
            {
                if (!LoginName.Equals(AdminEmailAddress) && new ValidationHelper().IsEmail(LoginName))
                {
                    yield return new ValidationResult("Username cannot be an email address unless it's same with your email address !");
                }
            }
        }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string FullName { get; set; }


        [Required]
        [Phone, StringLength(User.MaxPhoneNumberLength)]
        public string CellNumber { get; set; }
    }
}