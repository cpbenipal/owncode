using System.Collections.Generic;
using PanelMasterMVC5Separate.Auditing.Dto;
using PanelMasterMVC5Separate.Dto;

namespace PanelMasterMVC5Separate.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
