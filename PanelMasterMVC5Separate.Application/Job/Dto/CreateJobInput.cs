using Abp.AutoMapper;
using PanelMasterMVC5Separate.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Job.Dto
{
    [AutoMapTo(typeof(Jobs))]
    public class CreateJobInput
    {
        public int TenantID { get; set; }
        public int ClientID { get; set; }
        public int VehicleID { get; set; }
        public int InsuranceID { get; set; }
        public int BrokerID { get; set; }
        public int CSAID { get; set; }
        public string New_Comeback { get; set; }
       
        public int JobStatusID { get; set; }       
        public int ClaimHandlerID { get; set; }       
        public int PartsBuyerID { get; set; }
        public int KeyAccountManagerID { get; set; }
        public int EstimatorID { get; set; }

        public string RegNo { get; set; }
        public string VinNumber { get; set; }
        public string Colour { get; set; }
        public string Year { get; set; }
        public string UnderWaranty { get; set; }
        
    }
}
