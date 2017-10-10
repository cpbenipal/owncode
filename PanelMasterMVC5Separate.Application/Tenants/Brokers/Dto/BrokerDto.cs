using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Brokers;
using System.ComponentModel.DataAnnotations;

namespace PanelMasterMVC5Separate.Tenants.Brokers.Dto
{
    [AutoMapFrom(typeof(BrokerMaster))]
    public class BrokersDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string BrokerName { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        [Required]
        [MaxLength(400)]
        public string LogoPicture { get; set; }
    }


    public class BrokersUDto
    {
        [Required]
        public virtual string BrokerName { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        public virtual string NewFileName { get; set; }
        public virtual int Id { get; set; }
        public string LogoPicture { get; set; }
    }

    public class GetBrokersDto
    {
        public virtual string BrokerName { get; set; }
        public virtual string Mask { get; set; }
        public int Id { get; set; }
    }


    [AutoMapFrom(typeof(BrokerSubMaster))]
    public class BrokersListDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int TenantID { get; set; }
        [Required]
        [EmailAddress]
        public virtual string SpeedbumpEmail { get; set; }
        [Required]
        public virtual string BrokerName { get; set; }
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
        [Required]
        public virtual string Currency { get; set; }
        [Required]
        public virtual int BankID { get; set; }
        [Required]
        public virtual string Bank { get; set; }
        [Required]
        public virtual int BrokerID { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int SubpkId { get; set; }
    }

    [AutoMapTo(typeof(BrokerSubMaster))]
    public class BrokersToListDto : FullAuditedEntityDto
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

        [Required]
        public virtual int BankID { get; set; }

        [Required]
        public virtual int BrokerID { get; set; }
        public virtual bool IsActive { get; set; }
    }
    public class GetBrokerInput
    {
        public string Filter { get; set; }
    }

    public class StatusBrokerDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
    [AutoMapFrom(typeof(BrokerSubMaster))]
    public class BrokersForListDto : FullAuditedEntityDto
    {
        public virtual string BrokerName { get; set; }
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

        [Required]
        public virtual int BankID { get; set; }

        [Required]
        public virtual int BrokerID { get; set; }
        public virtual bool IsActive { get; set; }
    }
}

