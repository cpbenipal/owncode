using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.Vendors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Brokers
{
    [Table("tblBrokerMaster")]
    public class BrokerMaster : FullAuditedEntity
    {
        public BrokerMaster() { }

        public BrokerMaster(string brokerName, string mask, string logoPicture, int id)
        {
            BrokerName = brokerName;
            Mask = mask;
            LogoPicture = logoPicture;
            Id = id;
        }

        public BrokerMaster(string brokername, string mask, string logoPicture, int id ,int countryId)
        {
            BrokerName = brokername;
            Mask = mask;
            LogoPicture = logoPicture;
            Id = id;
            CountryID = countryId;
            IsActive = true;
        }

        [Required]
        public virtual string BrokerName { get; set; }
        [Required]
        public virtual string LogoPicture { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        public virtual int CountryID { get; set; }
        [ForeignKey("CountryID")]
        public virtual Countries Country { get; set; }

        public virtual bool IsActive { get; set; }
    }
    [Table("tblBrokerSubMaster")]
    public class BrokerSubMaster : FullAuditedEntity
    {
        [Required]
        public virtual int TenantID { get; set; }

        public virtual int BrokerID { get; set; }
        [ForeignKey("BrokerID")]
        public virtual BrokerMaster BrokerMasters { get; set; }
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
        public virtual string BrokerAccount { get; set; }
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
        public virtual CountryandCurrency Currency { get; set; }

        [Required]
        public virtual int BankID { get; set; }
        public virtual Banks Bank { get; set; }

        public virtual bool IsActive { get; set; }
    }

    [Table("tblBrokerMasterPics")]
    public class BrokerMasterPics : FullAuditedEntity
    {
        [Required]
        public virtual byte[] Bytes { get; set; }
        [Required]
        public virtual int BrokerID { get; set; }
        [ForeignKey("BrokerID")]
        public virtual BrokerMaster BrokerMasters { get; set; }
        public BrokerMasterPics() { }
        public BrokerMasterPics(byte[] bytes, int BrokerId)
            : this()
        {
            Bytes = bytes;
            BrokerID = BrokerId;
        }
    }
}
