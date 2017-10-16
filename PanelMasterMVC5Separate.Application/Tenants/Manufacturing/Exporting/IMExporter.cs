using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using PanelMasterMVC5Separate.Tenants.Manufacturing.Dto;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Tenants.Manufacturing.Exporting
{
    public interface IMExporter
    { 
        FileDto ExportToFile(List<VehicleMakeDto> listDtos);
        FileDto ExportMadesToFile(List<VehicleModelsFDto> listDtos);
    }
}
