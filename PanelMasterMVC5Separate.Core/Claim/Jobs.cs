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

        public virtual int? TenantID { get; set; }

        [Required]
        public virtual int ClientID { get; set; }
        public virtual Clients.Client Client { get; set; }

        //[Required]
        //public virtual int ManufactureID { get; set; }
        //public virtual VehicleMake Manufacture { get; set; }

        //public virtual int ModelID { get; set; }     
        public virtual int VehicleID { get; set; }
        [ForeignKey("VehicleID")]
        public virtual BrVehicle BrVehicle { get; set; }

        [Required]
        public virtual int InsuranceID { get; set; }
        public virtual Insurer.InsurerMaster Insurance { get; set; }

        [Required]
        public int BrokerID { get; set; }
        public Brokers.BrokerMaster Broker { get; set; }

        public string ClaimAdministrator { get; set; }
        public string ClaimNumber { get; set; }
        public string InsuranceOtherInfo { get; set; }
        public string PolicyNumber { get; set; }

        [Required]
        public virtual int JobStatusID { get; set; }
        public virtual Jobstatus JobStatus { get; set; }
        
        public int CSAID { get; set; }
        public int ClaimHandlerID { get; set; }
        public int PartsBuyerID { get; set; }
        public int KeyAccountManagerID { get; set; }
        public int EstimatorID { get; set; }

        //public virtual string RegNo { get; set; }
        //public virtual string VinNumber { get; set; }
        //public virtual string Colour { get; set; }
        //public virtual string Year { get; set; }
        //public virtual string UnderWaranty { get; set; }
        public bool New_Comeback { get; set; }
        public bool UnderWaranty { get; set; }
        public string DamangeReason { get; set; }
        public string BranchEntryMethod { get; set; }
        public bool IsUnrelatedDamangeReason { get; set; }
        public int CurrentKMs { get; set; }
        public string OtherInformation { get; set; }
        public int ShopAllocation { get; set; }
        public bool HighPriority { get; set; }
        public bool Contents { get; set; }
        public bool JobNotProceeding { get; set; }
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
