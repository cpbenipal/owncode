using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PanelMasterMVC5Separate.AdminFunctions.Dto;
using PanelMasterMVC5Separate.DataExporting.Excel.EpPlus;
using PanelMasterMVC5Separate.Dto;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.AdminFunctions.Exporting
{
    public class BankExport : EpPlusExcelExporterBase, IBankExport
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public BankExport(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<BankDto> claimListDtos)
        {
            return CreateExcelPackage(
                "Banks.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Banks"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("BankName"),
                    L("Country"),
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.BankName,
                         _ => _.CountryCode,
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
