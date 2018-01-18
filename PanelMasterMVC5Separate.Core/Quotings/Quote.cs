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
        public virtual bool IsStructuralRepairWork { get; set; }

        [Required]
        public virtual bool Pre_Auth { get; set; }

        public virtual string Value { get; set; }

        public virtual string Comments { get; set; }

        public virtual decimal TotalQuotedValue { get; set; }

        public virtual decimal EstimatedRepairDays { get; set; }

        public virtual int RepairerEstimatedDays { get; set; }
    }

    [Table("tblRepairType")]
    public class RepairTypes : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }

    [Table("tblQuoteCategories")]
    public class QuoteCategories : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }

    [Table("tblQuoteStatus")]
    public class QuoteStatus : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }
    [Table("tblQparttype")]
    public class QPartType : FullAuditedEntity
    {
        public virtual string Action { get; set; }
    }
    [Table("tblQlocation")]
    public class QLocation : FullAuditedEntity
    {
        public virtual string Location { get; set; }
    }
    [Table("tblQAction")]
    public class QAction : FullAuditedEntity
    {
        public virtual string Action { get; set; }

        public virtual int tblqparttypeId { get; set; }
        [ForeignKey("tblqparttypeId")]
        public virtual QPartType QPartTypes { get; set; }
    }
    [Table("tblQuoteDetails")]
    public class QuoteDetails : FullAuditedEntity
    {
        public virtual int? tenantid { get; set; }

        public virtual int QuoteId { get; set; }
        [ForeignKey("QuoteId")]
        public virtual QuoteMaster QuoteMaster { get; set; }

        public virtual int QuoteStatusId { get; set; }
        [ForeignKey("QuoteStatusId")]
        public virtual QuoteStatus QuoteStatus { get; set; }

        //public virtual int Actionid { get; set; }
        //[ForeignKey("Actionid")]
        //public virtual QAction QActions { get; set; }

        public virtual string QAction { get; set; }

        //public virtual int Locationid { get; set; }
        //[ForeignKey("Locationid")]
        //public virtual QLocation QLocations { get; set; }

        public virtual string QLocation { get; set; }

        public virtual string Description { get; set; }
        public virtual bool ToOrder { get; set; }
        public virtual bool Outwork { get; set; }
        public virtual int PartQty { get; set; }
        public virtual decimal PartPrice { get; set; }
        public virtual string Part { get; set; }
        public virtual decimal PanelHrs { get; set; }
        public virtual decimal PanelRate { get; set; }
        public virtual decimal PaintHrs { get; set; }
        public virtual decimal PaintRate { get; set; }
        public virtual decimal SAHrs { get; set; }
        public virtual decimal SARate { get; set; }
        public virtual bool NoTaxVat { get; set; }
        public virtual bool IsCurrent { get; set; }

        public virtual bool IsCompleted { get; set; }
    }
}
