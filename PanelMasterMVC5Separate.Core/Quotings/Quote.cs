using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.MultiTenancy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Quotings
{
    [Table("tblQuoteMaster")]
    public class QuoteMaster : FullAuditedEntity
    {
        [Required]
        public virtual int TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }

        [Required]
        public virtual int JobId { get; set; }
        [ForeignKey("JobId")]
        public virtual Jobs Jobs { get; set; }

        [Required]
        public virtual int QuoteStatusID { get; set; }
        [ForeignKey("QuoteStatusID")]
        public virtual QuoteStatus QuoteStatus { get; set; }

        [Required]
        public virtual int QuoteCatID { get; set; }
        [ForeignKey("QuoteCatID")]
        public virtual QuoteCategories QuoteCategories { get; set; }

        [Required]
        public virtual int RepairTypeId { get; set; }
        [ForeignKey("RepairTypeId")]
        public virtual RepairTypes RepairTypes { get; set; }

        [Required]
        public virtual bool Pre_Auth { get; set; }

        public virtual string Value { get; set; }

        public virtual string Comments{ get; set; }
    }

    [Table("tblRepairType")]
    public class RepairTypes:FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }

    [Table("tblQuoteCategories")]
    public class QuoteCategories: FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }

    [Table("tblQuoteStatus")]
    public class QuoteStatus: FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; } 
    }

}
