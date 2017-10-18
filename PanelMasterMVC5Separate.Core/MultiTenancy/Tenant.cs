using System;
using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;
using PanelMasterMVC5Separate.Authorization.Claim;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace PanelMasterMVC5Separate.MultiTenancy
{
    /// <summary>
    /// Represents a Tenant in the system.
    /// A tenant is a isolated customer for the application
    /// which has it's own users, roles and other application entities.
    /// </summary>
    public class Tenant : AbpTenant<User>
    {
        public const int MaxLogoMimeTypeLength = 64;

        //Can add application specific tenant properties here

        public virtual Guid? CustomCssId { get; set; }

        public virtual Guid? LogoId { get; set; }

        [MaxLength(MaxLogoMimeTypeLength)]
        public virtual string LogoFileType { get; set; }

        protected Tenant()
        {

        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {

        }

        public virtual bool HasLogo()
        {
            return LogoId != null && LogoFileType != null;
        }

        public void ClearLogo()
        {
            LogoId = null;
            LogoFileType = null;
        }

    }

    [Table("tblSignonPlans")]
    public class SignonPlans : FullAuditedEntity
    { 
        public virtual string PlanName { get; set; }
        [DataType(DataType.Currency)]
        public virtual double Price { get; set; }
        public virtual string HeaderColor { get; set; }
        public virtual int Members { get; set; }
    }

    [Table("tblTenantProfile")]
    public class TenantProfile : FullAuditedEntity
    {
        [Required]
        public int TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string FullName { get; set; }

        [Required]
        [StringLength(User.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string CompanyRegistrationNo { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string CompanyVatNo { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string City { get; set; }

        [Required]
        public string Country_list { get; set; }

        public string Remarks { get; set; }
    }

    [Table("tblTenantPlanBillingDetails")]
    public class TenantPlanBillingDetails : FullAuditedEntity
    {
        [Required]
        public int TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }

        [Required]
        public int planId { get; set; }
        [ForeignKey("planId")]
        public virtual SignonPlans SignonPlans { get; set; }

        [Required]
        [StringLength(160)]
        public string CardHoldersName { get; set; }

        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(7)]
        public string CardExpiration { get; set; }

        [Required]
        [StringLength(4)]
        public string CVV { get; set; }

        public string PaymentOptions { get; set; }
    }
}