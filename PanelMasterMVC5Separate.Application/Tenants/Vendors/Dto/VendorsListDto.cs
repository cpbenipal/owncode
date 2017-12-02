using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.ComponentModel.DataAnnotations;

namespace PanelMasterMVC5Separate.Tenants.Vendors.Dto
{
    public class GVendorsListDto
    {
        public int? VendorID { get; set; }
        public int? TenantId { get; set; }
        public Guid SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public string ContactEmail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Location { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string SupplierAccount { get; set; }
        public string PaymentTerms { get; set; }
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public string Branch { get; set; }
        public int CurrencyID { get; set; }
        public string Currency { get; set; }
        public int BankID { get; set; }
        public string Bank { get; set; }
        public bool IsActive { get; set; }
    }

    [AutoMapFrom(typeof(Banks))]
    public class BankDto : FullAuditedEntityDto
    {
        public string BankName { get; set; }
    }

    [AutoMapFrom(typeof(CountryandCurrency))]
    public class CurrencyDto : FullAuditedEntityDto
    {
        public string CurrencyCode { get; set; }
        public string CountryAndCurrency { get; set; }
    }

    public class VendorMainListDto
    {
        public int? id { get; set; }
        public Guid SupplierCode { get; set; }
        public string SupplierName { get; set; }        
        public bool IsActive { get; set; }
        public bool HasSub { get; set; }
        public string Country { get; set; }
    }
    public class VendorSubListDto
    {
        public virtual int? subVendorID { get; set; }
        public virtual int? TenantId { get; set; }
        public virtual int? VendorID { get; set; }     
        public virtual string RegistrationNumber { get; set; }        
        public virtual string TaxRegistrationNumber { get; set; }
        public virtual string ContactName { get; set; }
        [Phone]
        public virtual string ContactPhone { get; set; }
        [Phone]
        public virtual string ContactFax { get; set; }
        [EmailAddress]
        public virtual string ContactEmail { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string Location { get; set; }
        public virtual string SupplierAccount { get; set; }
        public virtual string PaymentTerms { get; set; }         
        public virtual string AccountNumber { get; set; }
        public virtual string Type { get; set; }
        public virtual string Branch { get; set; }
        public virtual int CurrencyID { get; set; }
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
        public int TenantId { get; set; }
        public bool Status { get; set; }
    }

    public class MasterStatusDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
    [AutoMapTo(typeof(VendorMain))]
    public class VendorSaveDto : FullAuditedEntityDto
    {
        [Required]
        public virtual Guid SupplierCode { get; set; }
        [Required]
        public virtual string SupplierName { get; set; }        
        [Required]
        public virtual int CountryID { get; set; }
    }
    [AutoMapFrom(typeof(Countries))]
    public class CountryDto : FullAuditedEntityDto
    {
        public virtual string Code { get; set; }

        public virtual string Country { get; set; }
    }
    [AutoMapFrom(typeof(VendorMain))]
    public class VendorDto : FullAuditedEntityDto
    {
        public Guid SupplierCode { get; set; }
        public string SupplierName { get; set; }       
        public int CountryID { get; set; }         
    }
}
