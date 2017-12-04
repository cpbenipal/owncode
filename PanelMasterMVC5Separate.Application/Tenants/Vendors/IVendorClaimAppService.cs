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
        ListResultDto<GVendorsListDto> GetVendors(GetClaimsInput input);
        Task<FileDto> GetClaimsToExcel();
        void AddEditVendor(VendorSaveDto input);
        VendorDto GetMainVendor(GetClaimsInput input);

        ListResultDto<VendorSubListDto> GetSubVendor(GetClaimsInput input);
        void UpdateVendor(VendorSubListDto input);
        void ChangeStatus(VendorSubListDto input);
        void UpdateMainVendor(VendorMainListDto input);

        // Host
        ListResultDto<VendorMainListDto> GetMasterVendors(GetClaimsInput input);
        void ChangeVendorStatus(MasterStatusDto input);
        void AddUpdateVendor(VendorSaveDto input);
        ListResultDto<CountryDto> GetCountry();
       
    }
}
