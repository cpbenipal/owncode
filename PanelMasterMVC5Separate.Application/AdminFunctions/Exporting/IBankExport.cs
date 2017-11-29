using PanelMasterMVC5Separate.AdminFunctions.Dto;
using PanelMasterMVC5Separate.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.AdminFunctions.Exporting
{
   public  interface IBankExport
    {
        FileDto ExportToFile(List<BankDto> listDtos);
        FileDto ExportToFile(List<JobStatusDto> listDtos);
        FileDto ExportToFile(List<JobStatusMaskDto> listDtos);
    }
}
