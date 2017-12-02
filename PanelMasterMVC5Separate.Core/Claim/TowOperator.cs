using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Vendors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Claim
{
    [Table("tblTowOperatorSub")]
    public class TowSubOperator : FullAuditedEntity
    {
        [Required]
        public virtual int TowOperatorId { get; set; }
        [ForeignKey("TowOperatorId")]
        public virtual TowOperator TowOperator { get; set; }
        [Required]
        public virtual int TenantId { get; set; }  
        [Phone]
        public virtual string ContactNumber { get; set; }
        public virtual string ContactPerson { get; set; }
        [EmailAddress]
        public virtual string EmailAddress { get; set; }
        public virtual bool isActive { get; set; }
    }
    [Table("tblTowOperator")]
    public class TowOperator : FullAuditedEntity
    {
        [Required]
        public virtual string Description { get; set; }        
        [Required]
        public virtual int CountryID { get; set; }
        [ForeignKey("CountryID")]
        public virtual Countries Country { get; set; }
        [Required]
        public virtual bool isActive { get; set; }
    }
}
