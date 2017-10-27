using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Claim
{
    [Table("tblNotProceedReason")]
    public class NotProceedReason : FullAuditedEntity
    { 
        public virtual string Description { get; set; }
    }
}
