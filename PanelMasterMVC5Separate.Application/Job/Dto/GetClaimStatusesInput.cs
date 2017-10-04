using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Job.Dto
{
    public class GetClaimStatusModelInput
    {
        public string Filter { get; set; }
    }

    [AutoMapFrom(typeof(BranchClaimStatus))]
    public class ClaimStatusListDto : FullAuditedEntityDto
    {
        public string Description { get; set; }
        public bool ShowStatus { get; set; }
    }
}
