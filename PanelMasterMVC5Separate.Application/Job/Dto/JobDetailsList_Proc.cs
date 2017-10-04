using Abp.AutoMapper;
using PanelMasterMVC5Separate.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Job.Dto
{
    [AutoMapTo(typeof(JobDetails_StoredProc))]
    public class JobDetailsList_Proc
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string CommunicationType { get; set; }
        public string ContactAfterService { get; set; }
        public string RegNo { get; set; }
        public string VinNumber { get; set; }
        public string Colour { get; set; }
        public string Year { get; set; }
        public string UnderWaranty { get; set; }
        public string New_Comeback { get; set; }
    }
}
