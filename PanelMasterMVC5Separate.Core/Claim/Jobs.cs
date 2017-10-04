using Abp.Domain.Entities.Auditing;
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
        public virtual Vehicle.Manufacture Manufacture { get; set; }

        [Required]
        public virtual int ModelID { get; set; }
        public virtual Vehicle.VehicleModel Model { get; set; }

        [Required]
        public virtual int InsuranceID { get; set; }
        public virtual Insurance_Brokers.Insurance Insurance { get; set; }

        [Required]
        public virtual int BrokerID { get; set; }
        public virtual Insurance_Brokers.Broker Broker { get; set; }


        public virtual int BranchID { get; set; }
        public virtual int FinancialID { get; set; }
        public virtual int CSAID { get; set; }

        [Required]
        public virtual int EstimatorID { get; set; }
        public virtual Estimations.Estimator Estimator { get; set; }

        public virtual int ProductiveStaffID { get; set; }

        [Required]
        public virtual int ClaimStatusID { get; set; }
        public virtual Claim.BranchClaimStatus ClaimStatus { get; set; }

        public virtual int ClaimEventID { get; set; }

        public virtual string RegNo { get; set; }
        public virtual string VinNumber { get; set; }
        public virtual string Colour { get; set; }
        public virtual string Year { get; set; }
        public virtual string UnderWaranty { get; set; }
        public virtual bool New_Comeback { get; set; }

    }
}
