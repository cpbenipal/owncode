using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.Vendors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Insurer
{
    [Table("tblInsurerSubMaster")]
    public class InsurerSub : FullAuditedEntity
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

        public virtual int InsurerID { get; set; }
        [ForeignKey("InsurerID")]
        public virtual InsurerMaster InsurerMasters { get; set; }

        [Required]
        public virtual int CurrencyID { get; set; }
        public virtual CountryandCurrency Currency { get; set; }

        [Required]
        public virtual int BankID { get; set; }
        public virtual Banks Bank { get; set; }

        public virtual bool IsActive { get; set; }
    }
}