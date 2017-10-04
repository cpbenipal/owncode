using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Tenants.Vendors
{
    public interface IVendorClaimAppService : IApplicationService
    {
        ListResultDto<BankDto> GetBanks();
        ListResultDto<CurrencyDto> GetCurrencies();
        void AddVendor(GVendorsListDto input);
        ListResultDto<VendorsListDto> GetVendors(GetClaimsInput input);
        Task<FileDto> GetClaimsToExcel();    
        VendorsListDto GetVendor(GetClaimsInput input);
        void UpdateVendor(GVendorsListDto input);
        void ChangeStatus(StatusDto input);
    }
}
