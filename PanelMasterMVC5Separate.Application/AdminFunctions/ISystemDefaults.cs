using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.AdminFunctions.Dto;
using PanelMasterMVC5Separate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.AdminFunctions
{
    public interface ISystemDefaults : IApplicationService
    {
        ListResultDto<BankDto> GetBanks(GetInputs input);
        void CreateOrUpdateBank(BankToDto input);
        ListResultDto<CountriesDto> GetCountry();
        BankDetailDto GetBank(GetClaimsInput input);
        Task<FileDto> GetBanksExcel();
        void ChangeStatus(ActiveDto input);
        // Job status
        ListResultDto<JobStatusDto> GetJobStatuses(GetInput input);
        void CreateOrUpdateJobStatus(JobStatusToDto input);
        JobStatusDto GetJobStatus(GetClaimsInput input);
        Task<FileDto> GetJobStatusToExcel();
        //Job Status Mask
        ListResultDto<JobStatusMaskDto> GetJobMaskStatuses(GetInput input);
        void CreateOrUpdateJobMaskStatus(JobStatusMaskToDto input);
        JobStatusMaskDto GetJobStatusMask(GetClaimsInput input);
        void ChangeJobMaskStatus(ActiveDto input);
        Task<FileDto> GetJobStatusMaskToExcel();
        //Quote Status
        ListResultDto<QuoteStatusDto> GetQuoteStatuses(GetInput input);
        void CreateOrUpdateQuoteStatus(QuoteStatusToDto input);
        QuoteStatusDto GetQuoteStatus(GetClaimsInput input);
        void ChangeQuoteStatusStatus(ActiveDto input);
        Task<FileDto> GetQuoteStatusToExcel();

        //Repair Type
        ListResultDto<RepairTypeDto> GetRepairTypes(GetInput input);
        void CreateOrUpdateRepairType(RepairTypeToDto input);
        RepairTypeDto GetRepairType(GetClaimsInput input);
        void ChangeRepairTypeStatus(ActiveDto input);
        Task<FileDto> GetRepairTypeToExcel();

        //Role Category
        ListResultDto<RoleCategoryDto> GetRoleCategories(GetInput input);
        void CreateOrUpdateRoleCategory(RoleCategoryToDto input);
        RoleCategoryDto GetRoleCategory(GetClaimsInput input);
        void ChangeRoleCategoryStatus(ActiveDto input);
        Task<FileDto> GetRoleCategoryToExcel();

        //SignOnPlan
        ListResultDto<SignOnDto> GetSignOnPlans(GetInputs input);
        void CreateOrUpdateSignOnPlan(SignOnToDto input);
        void ChangePlanStatus(ActiveDto input);
        SignOnDto GetPlanDetail(GetClaimsInput input);
        Task<FileDto> GetSignOnToExcel();

        //TowOperators
        ListResultDto<TowOperatorDto> GetTowOperators(GetInputs input);
        void CreateOrUpdateTowOperator(TowOperatorToDto input);
        TowOperatorDto GetTowOperator(GetClaimsInput input);
        Task<FileDto> GetTowOperatorsExcel();
        void ChangeTowOperatorStatus(ActiveDto input);
    }
}
