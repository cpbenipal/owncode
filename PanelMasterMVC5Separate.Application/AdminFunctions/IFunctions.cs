using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.AdminFunctions.Dto;

namespace PanelMasterMVC5Separate.AdminFunctions
{
    public interface IFunctions : IApplicationService
    {
        ListResultDto<functionDto> GetMasterRecords(GetInputs input);
        ListResultDto<functionCCDto> GetCountryOrCurrency(GetInputs input);
        ListResultDto<BankDto> GetBanks(GetInputs input);
        void CreateOrUpdateDescription(TableDescriptionDto input);
        void CreateOrUpdateCodes(CodeDto input);
        void CreateOrUpdateBank(CodeDto input);
        ListResultDto<CountriesDto> GetCountry();
        ListResultDto<PlanDto> GetSignOnPlans(GetInputs input);
        void CreateOrUpdateSignOnPlan(PlanDto f);
        void ChangeStatus(StatusDto input);
    }
}