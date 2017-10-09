using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Tenants.Insurer.Exporting
{
    public interface IInsurerExporter
    {
        FileDto ExportToFile(List<InsurersListDto> claimListDtos);
    }
}
