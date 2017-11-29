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

    }
}
