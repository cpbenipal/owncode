using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Insurer
{
    public interface IInsurerAppService : IApplicationService
    {
        ListResultDto<BankDto> GetBanks();
        ListResultDto<CurrencyDto> GetCurrencies();
        ListResultDto<CountriesDto> GetCountry();
        void CreateOrUpdateSubInsurer(InsurersToListDto input);       
        InsurersForListDto GetInsurerSubDetail(GetClaimsInput input);
        Task<InsurerPics> GetOrNullAsync(int id);
        Task<FileDto> GetClaimsToExcel();
        void ChangeStatus(StatusDto input);
        //Host
        ListResultDto<InsurersMasterDto> GetInsurerMasters(GetInsurerInput input);
        void ChangeMasterStatus(MasterStatusDto input);
        Task CreateInsurerMaster(InsurersDto input);
        Task UpdateInsurerMaster(InsurersUDto input);
        InsurersDto GetInsurerMasterDetail(GetClaimsInput input);
        ListResultDto<InsurersListDto> GetInsurers(GetInsurerInput input);
        Task<FileDto> GetInsurersToExcel();
    }
}
