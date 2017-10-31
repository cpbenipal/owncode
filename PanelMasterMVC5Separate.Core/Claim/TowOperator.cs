using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.MultiTenancy;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Claim
{
    [Table("tblTowOperator")]
    public class TowOperator : FullAuditedEntity
    {
        [Required]
        public virtual int TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }         
        public virtual string Description { get; set; }        
        [Phone]
        public virtual string ContactNumber { get; set; }         
        [Phone]
        public virtual string ContactPerson { get; set; }         
        [EmailAddress]
        public virtual string EmailAdress { get; set; }
        [Required]
        public virtual bool Enabled { get; set; }
    }
}
