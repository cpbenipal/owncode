using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using PanelMasterMVC5Separate.Vendors;

namespace PanelMasterMVC5Separate.Tenants.Vendors
{
    public interface IVendorClaimAppService : IApplicationService
    {
        ListResultDto<BankDto> GetBanks();
        ListResultDto<CurrencyDto> GetCurrencies();
        VendorMain AddMainVendor(VendorMainListDto input);
        void AddSubVendor(VendorSubListDto input);
        ListResultDto<VendorMainListDto> GetVendors(GetClaimsInput input);
        Task<FileDto> GetClaimsToExcel();
        ListResultDto<VendorMainListDto> GetMainVendor(GetClaimsInput input);
        ListResultDto<VendorSubListDto> GetSubVendor(GetClaimsInput input);
        void UpdateVendor(VendorSubListDto input);
        void ChangeStatus(VendorSubListDto input);
    }
}
