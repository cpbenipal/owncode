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
        public int ClientID { get; set; }
        public int ManufactureID { get; set; }
        public int ModelID { get; set; }
        public int InsuranceID { get; set; }
        public int BrokerID { get; set; }
        public int BranchID { get; set; }
        public int FinancialID { get; set; }
        public int CSAID { get; set; }
        public int EstimatorID { get; set; }
        public int ProductiveStaffID { get; set; }
        public int ClaimStatusID { get; set; }
        public int ClaimEventID { get; set; }

        public string RegNo { get; set; }
        public string VinNumber { get; set; }
        public string Colour { get; set; }
        public string Year { get; set; }
        public string UnderWaranty { get; set; }
        public bool New_Comeback { get; set; }
    }
}
