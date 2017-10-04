using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Claim
{
    [Table("brClaimsStatus")]
    public class BranchClaimStatus : FullAuditedEntity
    {        
        public virtual string Description { get; set; }
        public virtual bool ShowStatus { get; set; }
    }
}
