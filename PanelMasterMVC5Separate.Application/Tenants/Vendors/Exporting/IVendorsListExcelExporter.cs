using System.Collections.Generic;
using PanelMasterMVC5Separate.Authorization.Claim.Dto;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;

namespace PanelMasterMVC5Separate.Tenants.Vendors.Exporting
{
    public interface IVendorsListExcelExporter
    {
        FileDto ExportToFile(List<GVendorsListDto> claimListDtos);
    }
}