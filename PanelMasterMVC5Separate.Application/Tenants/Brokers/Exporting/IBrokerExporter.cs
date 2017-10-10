using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Tenants.Brokers.Exporting
{
    public interface IBrokerExporter
    {
        FileDto ExportToFile(List<BrokersListDto> claimListDtos);
    }
}
