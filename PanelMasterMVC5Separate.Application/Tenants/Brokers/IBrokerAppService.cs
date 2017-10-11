using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Brokers
{
    public interface IBrokerAppService : IApplicationService
    {
        ListResultDto<BankDto> GetBanks();
        ListResultDto<CurrencyDto> GetCurrencies();
        ListResultDto<BrokersListDto> GetBrokers(GetBrokerInput input);        
        Task CreateBrokerMaster(BrokersDto input);
        Task UpdateBrokerMaster(BrokersUDto input);
        void CreateOrUpdateSubBroker(BrokersToListDto input);
        BrokersDto GetBrokerMasterDetail(GetClaimsInput input);
        BrokersForListDto GetBrokerSubMasterDetail(GetClaimsInput input);
        Task<BrokerMasterPics> GetOrNullAsync(int id);
        Task<FileDto> GetClaimsToExcel();
        void ChangeStatus(StatusDto input);
    }
}
