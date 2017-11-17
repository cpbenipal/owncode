using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Vendors;
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
        public virtual string ContactPerson { get; set; }         
        [EmailAddress]
        public virtual string EmailAddress { get; set; }
        [Required]
        public virtual bool isActive { get; set; }
        public virtual int CountryID { get; set; }
        [ForeignKey("CountryID")]
        public virtual Countries Country { get; set; }
    }
}
