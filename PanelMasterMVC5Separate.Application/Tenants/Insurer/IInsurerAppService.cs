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
        ListResultDto<InsurersListDto> GetInsurers(GetInsurerInput input);

        Task CreateInsurerMaster(InsurersDto input);
        Task UpdateInsurerMaster(InsurersUDto input);

        void CreateOrUpdateSubInsurer(InsurersToListDto input);

        InsurersDto GetInsurerMasterDetail(GetClaimsInput input);
        InsurersForListDto GetInsurerSubDetail(GetClaimsInput input);

        Task<InsurerPics> GetOrNullAsync(int id);
        Task<FileDto> GetClaimsToExcel();
        void ChangeStatus(StatusDto input);
    }
}
