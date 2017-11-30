using PanelMasterMVC5Separate.AdminFunctions.Dto;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.MultiTenancy;
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
        FileDto ExportToFile(List<QuoteStatusDto> listDtos);
        FileDto ExportToFile(List<RepairTypeDto> listDtos);
        FileDto ExportToFile(List<RoleCategoryDto> listDtos);
        FileDto ExportToFile(List<SignOnDto> listDtos);
    }
}
