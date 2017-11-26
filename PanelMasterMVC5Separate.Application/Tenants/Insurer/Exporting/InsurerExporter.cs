using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PanelMasterMVC5Separate.DataExporting.Excel.EpPlus;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Tenants.Insurer.Exporting
{
    public class InsurerExporter : EpPlusExcelExporterBase, IInsurerExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public InsurerExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<InsurersMasterDto> claimListDtos)
        {
            return CreateExcelPackage(
                "MasterInsurers.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("MasterInsurers"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("InsurerName"),
                     L("Mask"),
                      L("Country"),
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.InsurerName,
                        _ => _.Mask,
                        _ => _.Country,
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

        public FileDto ExportToFile(List<InsurersListDto> claimListDtos)
        {
            return CreateExcelPackage(
                "InsurerList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Insurers"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("InsurerName"),
                     L("Mask"),
                    L("ContactName"),
                    L("ContactPhone"),
                    L("ContactFax"),
                    L("ContactEmail"),
                    L("Address1"),
                    L("Address2"),
                    L("Address3"),
                    L("Location"),
                    L("RegistrationNumber"),
                    L("TaxRegistrationNumber"),
                    L("InsurerAccount"),
                    L("PaymentTerms"),
                    L("AccountNumber"),
                    L("Bank"),
                    L("CurrencyCode"),
                    L("Type"),
                    L("Branch"),
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.InsurerName,
                        _ => _.Mask,
                        _ => _.ContactName,
                        _ => _.ContactPhone,
                        _ => _.ContactFax,
                        _ => _.ContactEmail,
                        _ => _.Address1,
                        _ => _.Address2,
                        _ => _.Address3,
                        _ => _.Location,
                        _ => _.RegistrationNumber,
                        _ => _.TaxRegistrationNumber,
                        _ => _.InsurerAccount,
                        _ => _.PaymentTerms,
                         _ => _.AccountNumber,
                          _ => _.Bank,
                        _ => _.Currency,
                        _ => _.Type,
                        _ => _.Branch,
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
