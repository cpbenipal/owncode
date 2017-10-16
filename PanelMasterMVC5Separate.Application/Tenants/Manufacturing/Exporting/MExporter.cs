using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PanelMasterMVC5Separate.DataExporting.Excel.EpPlus;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Manufacturing.Dto;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Tenants.Manufacturing.Exporting
{
    public class MExporter : EpPlusExcelExporterBase, IMExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportMadesToFile(List<VehicleModelsFDto> listDtos)
        {
            return CreateExcelPackage(
               "VehicleModelsList.xlsx",
               excelPackage =>
               {
                   var sheet = excelPackage.Workbook.Worksheets.Add(L("VehicleModels"));
                   sheet.OutLineApplyStyle = true;

                   AddHeader(
                    sheet,
                   L("VehicleMake"),
                   L("VehicleMade"),
                   L("MMCode"),
                   L("CreationTime")
                );

                   AddObjects(
                       sheet, 2, listDtos,
                       _ => _.Make,
                       _ => _.Model,
                       _ => _.MMCode,
                       _ => _.CreationTime
                   );

                   //Formatting cells

                   var lastLoginTimeColumn = sheet.Column(8);
                   lastLoginTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                   var creationTimeColumn = sheet.Column(10);
                   creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                   for (var i = 1; i <= 10; i++)
                   {
                       sheet.Column(i).AutoFit();
                   }
               });
        }

        public FileDto ExportToFile(List<VehicleMakeDto> claimListDtos)
        {
            return CreateExcelPackage(
                "VehicleMakeList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("VehicleMakes"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("VehicleMake"),
                    L("IsActive"),
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.Description,
                         _ => _.IsActive,
                        _ => _.CreationTime
                    );

                    //Formatting cells

                    var lastLoginTimeColumn = sheet.Column(8);
                    lastLoginTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                    var creationTimeColumn = sheet.Column(10);
                    creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                    for (var i = 1; i <= 10; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
