using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.ComponentModel.DataAnnotations;

namespace PanelMasterMVC5Separate.Tenants.Vendors.Dto
{
    [AutoMapFrom(typeof(Vendor))]
    public class VendorsListDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int? TenantId { get; set; }
        [Required]
        public virtual Guid SupplierCode { get; set; }
        [Required]
        public virtual string SupplierName { get; set; }
        [Required]
        public virtual string ContactName { get; set; }
        [Required]
        [Phone]
        public virtual string ContactPhone { get; set; }
        [Required]
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
        public virtual string SupplierAccount { get; set; }
        [Required]
        public virtual string PaymentTerms { get; set; }
        [Required]
        public virtual string AccountNumber { get; set; }
        [Required]
        public virtual string Type { get; set; }
        [Required]
        public virtual string Branch { get; set; }
        [Required]
        public virtual int CurrencyID { get; set; }
        [Required]
        public virtual string Currency { get; set; }
        [Required]
        public virtual int BankID { get; set; }
        [Required]
        public virtual string Bank { get; set; }
        public virtual bool IsActive { get; set; }
    }

    [AutoMapFrom(typeof(Banks))]
    public class BankDto : FullAuditedEntityDto
    {
        public string BankName { get; set; }
    }

    [AutoMapFrom(typeof(Currencies))]
    public class CurrencyDto : FullAuditedEntityDto
    {
        public string CurrencyCode { get; set; }
        public string CurrencyType { get; set; }
    }

    [AutoMapTo(typeof(Vendor))]
    public class GVendorsListDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int? TenantId { get; set; }
        [Required]
        public virtual Guid SupplierCode { get; set; }
        [Required]
        public virtual string SupplierName { get; set; }
        [Required]
        public virtual string ContactName { get; set; }
        [Required]
        [Phone]
        public virtual string ContactPhone { get; set; }
        [Required]
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
        public virtual string SupplierAccount { get; set; }
        [Required]
        public virtual string PaymentTerms { get; set; }
        [Required]
        public virtual string AccountNumber { get; set; }
        [Required]
        public virtual string Type { get; set; }
        [Required]
        public virtual string Branch { get; set; }

        [Required]
        public virtual int CurrencyID { get; set; } 

        [Required]
        public virtual int BankID { get; set; } 

        public virtual bool IsActive { get; set; }
    }
    public class GetClaimsInput
    {
        public string Filter { get; set; }
    }
     
    public class StatusDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
