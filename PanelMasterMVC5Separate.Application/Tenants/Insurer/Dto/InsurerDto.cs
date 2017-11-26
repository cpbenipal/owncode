using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Vendors;
using System.ComponentModel.DataAnnotations;

namespace PanelMasterMVC5Separate.Tenants.Insurer.Dto
{
    [AutoMapFrom(typeof(InsurerMaster))]
    public class InsurersDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string InsurerName { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        [Required]
        [MaxLength(400)]
        public string LogoPicture { get; set; }
        [Required]
        public int CountryID { get; set; }
    }


    public class InsurersUDto
    {
        [Required]
        public virtual string InsurerName { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        public virtual string NewFileName { get; set; }
        public virtual int Id { get; set; }
        public string LogoPicture { get; set; }
        [Required]
        public int CountryID { get; set; }
    }

    public class GetInsurersDto
    {
        public virtual string InsurerName { get; set; }
        public virtual string Mask { get; set; }
        public int Id { get; set; }
    }


    [AutoMapFrom(typeof(InsurerSub))]
    public class InsurersListDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int TenantID { get; set; }
        [Required]
        [EmailAddress]
        public virtual string SpeedbumpEmail { get; set; }
        [Required]
        public virtual string InsurerName { get; set; }
        [Required]
        [EmailAddress]
        public virtual string QuoteCentreEmail { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        [Required]
        public virtual string EarlySettleDisc { get; set; }
        [Required]
        public virtual string ContactName { get; set; }
        [Required]
        [Phone]
        public virtual string ContactPhone { get; set; }
        [Required]
        [Phone]
        public virtual string ContactFax { get; set; }
        [Required]
        [EmailAddress]
        public virtual string ContactEmail { get; set; }
        [Required]
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        [Required]
        public virtual string Location { get; set; }
        [Required]
        public virtual string RegistrationNumber { get; set; }
        [Required]
        public virtual string TaxRegistrationNumber { get; set; }
        [Required]
        public virtual string InsurerAccount { get; set; }
        [Required]
        public virtual string PaymentTerms { get; set; }
        [Required]
        public virtual string AccountNumber { get; set; }
        [Required]
        public virtual string Type { get; set; }
        [Required]
        public virtual string Branch { get; set; }
        public virtual string Comments { get; set; }
        [Required]
        public virtual int CurrencyID { get; set; }
        [Required]
        public virtual string Currency { get; set; }
        [Required]
        public virtual int BankID { get; set; }
        [Required]
        public virtual string Bank { get; set; }
        [Required]
        public virtual int InsurerID { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int SubpkId { get; set; }
    }

    [AutoMapTo(typeof(InsurerSub))]
    public class InsurersToListDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int TenantID { get; set; }
        [Required]
        [EmailAddress]
        public virtual string SpeedbumpEmail { get; set; }

        [Required]
        [EmailAddress]
        public virtual string QuoteCentreEmail { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        [Required]
        public virtual string EarlySettleDisc { get; set; }
        [Required]
        public virtual string ContactName { get; set; }
        [Required]
        [Phone]
        public virtual string ContactPhone { get; set; }
        [Required]
        [Phone]
        public virtual string ContactFax { get; set; }
        [Required]
        [EmailAddress]
        public virtual string ContactEmail { get; set; }
        [Required]
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        [Required]
        public virtual string Location { get; set; }
        [Required]
        public virtual string RegistrationNumber { get; set; }
        [Required]
        public virtual string TaxRegistrationNumber { get; set; }
        [Required]
        public virtual string InsurerAccount { get; set; }
        [Required]
        public virtual string PaymentTerms { get; set; }
        [Required]
        public virtual string AccountNumber { get; set; }
        [Required]
        public virtual string Type { get; set; }
        [Required]
        public virtual string Branch { get; set; }
        public virtual string Comments { get; set; }
        [Required]
        public virtual int CurrencyID { get; set; }

        [Required]
        public virtual int BankID { get; set; }

        [Required]
        public virtual int InsurerID { get; set; }
        public virtual bool IsActive { get; set; }
    }
    public class GetInsurerInput
    {
        public string Filter { get; set; }
    }

    public class StatusInsurerDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
    [AutoMapFrom(typeof(InsurerSub))]
    public class InsurersForListDto : FullAuditedEntityDto
    {
        public virtual string InsurerName { get; set; }
        public virtual string MaskMain { get; set; }

        [Required]
        public virtual int TenantID { get; set; }
        [Required]
        [EmailAddress]
        public virtual string SpeedbumpEmail { get; set; }
        [Required]
        [EmailAddress]
        public virtual string QuoteCentreEmail { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        [Required]
        public virtual string EarlySettleDisc { get; set; }
        [Required]
        public virtual string ContactName { get; set; }
        [Required]
        [Phone]
        public virtual string ContactPhone { get; set; }
        [Required]
        [Phone]
        public virtual string ContactFax { get; set; }
        [Required]
        [EmailAddress]
        public virtual string ContactEmail { get; set; }
        [Required]
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        [Required]
        public virtual string Location { get; set; }
        [Required]
        public virtual string RegistrationNumber { get; set; }
        [Required]
        public virtual string TaxRegistrationNumber { get; set; }
        [Required]
        public virtual string InsurerAccount { get; set; }
        [Required]
        public virtual string PaymentTerms { get; set; }
        [Required]
        public virtual string AccountNumber { get; set; }
        [Required]
        public virtual string Type { get; set; }
        [Required]
        public virtual string Branch { get; set; }
        public virtual string Comments { get; set; }
        [Required]
        public virtual int CurrencyID { get; set; }

        [Required]
        public virtual int BankID { get; set; }

        [Required]
        public virtual int InsurerID { get; set; }
        public virtual bool IsActive { get; set; }
    }

    [AutoMapFrom(typeof(InsurerMaster))]
    public class InsurersMasterDto : FullAuditedEntityDto
    { 
        public virtual string InsurerName { get; set; } 
        public virtual string Mask { get; set; } 
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }
    [AutoMapFrom(typeof(Countries))]
    public class CountriesDto : FullAuditedEntityDto
    {
        public virtual string Code { get; set; }

        public virtual string Country { get; set; }
    }
}

