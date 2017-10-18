using System.ComponentModel.DataAnnotations;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.MultiTenancy;
using System.ComponentModel;
using PanelMasterMVC5Separate.Security;
using System.Collections.Generic;
using Abp.Extensions;
using PanelMasterMVC5Separate.Validation;

namespace PanelMasterMVC5Separate.Web.Models.TenantRegistration
{
    public class TenantRegistrationViewModel : IValidatableObject
    { 
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

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
            if (!Name.IsNullOrEmpty())
            {
                if (!Name.Equals(AdminEmailAddress) && new ValidationHelper().IsEmail(Name))
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
        public string PhoneNumber { get; set; }
         

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string CompanyRegistrationNo { get; set; }

        [Required]
        [Display(Name = "Company Vat No."), StringLength(User.MaxSurnameLength)]
        public string CompanyVatNo { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City/Town"), StringLength(User.MaxSurnameLength)]
        public string City { get; set; }

        [Required]
        public string Country_list { get; set; }

        public string Remarks { get; set; }
 
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

        public int PlanId { get; set; }

        [Required]
        [StringLength(5)]
        public string TaxID { get; set; }
    }
}