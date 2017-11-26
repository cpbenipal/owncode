using System;
using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;
using PanelMasterMVC5Separate.Authorization.Claim;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp;
using Abp.Domain.Entities;

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
        public virtual bool isActive { get; set; }
    }

    [Table("tblTenantProfile")]
    public class TenantProfile : FullAuditedEntity
    {
        [Required]
        public int TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }

        [StringLength(User.MaxNameLength)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string FullName { get; set; }

        [Phone, Required]
        [StringLength(User.MaxPhoneNumberLength)]
        public string CellNumber { get; set; }

        [Phone]
        [StringLength(User.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Phone]
        [StringLength(User.MaxPhoneNumberLength)]
        public string FaximileeNumber { get; set; }

        public string Address { get; set; }

        [StringLength(User.MaxSurnameLength)]
        public string City { get; set; }

        [Required, StringLength(2)]
        public string CountryCode { get; set; }

        [Required, StringLength(3)]
        public string CurrencyCode { get; set; }

        public string Timezone { get; set; }

        [StringLength(User.MaxSurnameLength)]
        public string CompanyRegistrationNo { get; set; }

        [StringLength(User.MaxSurnameLength)]
        public string CompanyVatNo { get; set; }

        public string InvoicingInstruction { get; set; }
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

        //[Required, StringLength(2)]
        //public string BillingCountryCode { get; set; }

        //[Required, StringLength(3)]
        //public string CurrencyCode { get; set; }

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

    [Table("tblTenantCompanyLogo")]
    public class TenantCompanyLogo : Entity<Guid>
    {
        public virtual int? CompanyId { get; set; }

        [Required]
        public virtual byte[] Bytes { get; set; }

        public TenantCompanyLogo()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        public TenantCompanyLogo(int? companyid, byte[] bytes)
            : this()
        {
            CompanyId = companyid;
            Bytes = bytes;
        }
    }
}