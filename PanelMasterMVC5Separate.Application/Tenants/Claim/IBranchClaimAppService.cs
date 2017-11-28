using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using PanelMasterMVC5Separate.Tenants.Claim.Dto;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
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

        ListResultDto<Brokers.Dto.CountriesDto> GetCountry();
        ListResultDto<InsurersDto> GetInsurances();
        ListResultDto<BrokersDto> GetBrokers();


        ListResultDto<JobStatusDto> GetJobStatuses(GetClaimsInput input);
        Task<FileDto> GetJobStatusToExcel();

        ListResultDto<JobStatusMasksDto> GetJobStatusMasks();

        Task<JobstatusTenantDto> GetJobStatusForEdit(GetJobInput input);
        void ChangeStatus(JobStatusDto input);
        void CreateOrUpdateJobStatus(JobstatusTenantToDto input);

        ListResultDto<int> GetSortOrders(int jobStatusId);

        ListResultDto<TowOperatorDto> GetTowOperators(GetClaimsInput input);

        Task<TowTenantDto> GetTow(NullableIdDto<int> input);

        void CreateOrUpdateTowOperator(TowTenantDto input);
        Task<FileDto> GetTowOperatorsToExcel();
        void ChangeTowStatus(JobStatusDto input);

        ListResultDto<TowOperatorDto> GetHostTowOperators(GetClaimsInput input);
        Task<TowTenantDto> GetHostTow(NullableIdDto<int> input);
    }
}
