using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Vendors
{
    [Table("tblVendors")]
    public class Vendor : FullAuditedEntity
    {
        public const int MaxLength = 500;

        public virtual int? TenantId { get; set; }
        public virtual Guid SupplierCode { get; set; }
        public virtual string SupplierName { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string ContactPhone { get; set; }
        public virtual string ContactFax { get; set; }
        public virtual string ContactEmail { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string Location { get; set; }
        public virtual string RegistrationNumber { get; set; }
        public virtual string TaxRegistrationNumber { get; set; }
        public virtual string SupplierAccount { get; set; }
        public virtual string PaymentTerms { get; set; }
        public virtual string AccountNumber { get; set; }
        public virtual string Type { get; set; }
        public virtual string Branch { get; set; }

        [Required]
        public virtual int CurrencyID { get; set; }
        public virtual Currencies Currency { get; set; }

        [Required]
        public virtual int BankID { get; set; }
        public virtual Banks Bank { get; set; }

        public virtual bool IsActive { get; set; }
    }
}
