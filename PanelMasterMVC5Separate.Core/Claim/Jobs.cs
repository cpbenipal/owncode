using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.Vehicle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Claim
{
    [Table("brJobs")]
    public class Jobs : FullAuditedEntity
    {
        public const int MaxLength = 500;

        [Required]
        public virtual int ClientID { get; set; }
        public virtual Clients.Client Client { get; set; }

        [Required]
        public virtual int ManufactureID { get; set; }
        public virtual VehicleMake Manufacture { get; set; }

        public virtual int ModelID { get; set; }

        [Required]
        public virtual int InsuranceID { get; set; }
        public virtual Insurer.InsurerMaster Insurance { get; set; }

        [Required]
        public virtual int BrokerID { get; set; }
        public virtual Brokers.BrokerMaster Broker { get; set; }

        public virtual int BranchID { get; set; }
        public virtual int FinancialID { get; set; }
        public virtual int CSAID { get; set; }

        public virtual int ProductiveStaffID { get; set; }

        public virtual int ClaimEventID { get; set; }

        public virtual NotProceedReason NotProceedReason { get; set; }
         
        public virtual TowOperator TowOperator { get; set; }

        public virtual string RegNo { get; set; }
        public virtual string VinNumber { get; set; }
        public virtual string Colour { get; set; }
        public virtual string Year { get; set; }
        public virtual string UnderWaranty { get; set; }
        public virtual bool New_Comeback { get; set; }

        public virtual string DamangeReason { get; set; }
        public virtual string BranchEntryMethod { get; set; }
        public virtual bool IsUnrelatedDamangeReason { get; set; }
        public virtual string CurrentKMs { get; set; }
        public virtual string OtherInformation { get; set; }
    }

    [Table("tblJobstatus")]
    public class Jobstatus : FullAuditedEntity
    {
        public virtual string Description { get; set; }
    }

    [Table("tblJobstatusMask")]
    public class JobstatusMask : FullAuditedEntity
    {       
        public virtual string Description1 { get; set; }
        public virtual string Description2 { get; set; }
        public virtual string Description3 { get; set; }
        public virtual bool Enabled { get; set; }
    }

    [Table("tblJobstatusTenant")]
    public class JobstatusTenant : FullAuditedEntity
    {
        public int JobStatusID { get; set; }  
        [ForeignKey("JobStatusID")]
        public virtual Jobstatus Jobstatus { get; set; }
        public virtual int Tenant { get; set; }
        public virtual bool isActive { get; set; }
        public virtual bool ShowSpeedbump { get; set; }
        public virtual bool ShowAwaiting { get; set; }
        public virtual int Sortorder { get; set; }
        public int Mask { get; set; }
        [ForeignKey("Mask")]
        public virtual JobstatusMask JobstatusMask { get; set; }
    }


}
