using System.Collections.Generic;
using PanelMasterMVC5Separate.Authorization.Claim.Dto;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Claim.Dto;

namespace PanelMasterMVC5Separate.Tenants.Claim.Exporting
{
    public interface IClaimsListExcelExporter
    {
        FileDto ExportToFile(List<BranchClaimListDto> claimListDtos);
    }
}