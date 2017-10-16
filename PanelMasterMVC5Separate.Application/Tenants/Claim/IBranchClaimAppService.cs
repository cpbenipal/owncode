using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Authorization.Claim.Dto;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Job;
using PanelMasterMVC5Separate.Job.Dto;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using PanelMasterMVC5Separate.Tenants.Claim.Dto;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Claim
{
    public interface IBranchClaimAppService : IApplicationService
    {
        ListResultDto<BranchClaimListDto> GetClaims(GetClaimsInput input);

        Task<FileDto> GetClaimsToExcel();

        BranchClaimListDto GetJobDetails(GetClaimsInput input);

        void UpdateVehicleInfo(BranchClaimListDto input);

        void UpdateInsuranceInfo(BranchClaimListDto input);

        void UpdateClient(BranchClaimListDto input);
         

        ListResultDto<InsurersDto> GetInsurances();
        ListResultDto<BrokersDto> GetBrokers();
    }
}
