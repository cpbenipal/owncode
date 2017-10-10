using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Vendors.Exporting
{
    public interface IVendorExporter
    {
        FileDto ExportToFile(List<GVendorsListDto> claimListDtos);
    }
}
