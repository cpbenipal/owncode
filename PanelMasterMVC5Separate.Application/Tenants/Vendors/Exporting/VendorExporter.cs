using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PanelMasterMVC5Separate.DataExporting.Excel.EpPlus;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Tenants.Vendors.Exporting
{
    public class VendorExporter : EpPlusExcelExporterBase, IVendorExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public VendorExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GVendorsListDto> claimListDtos)
        {
            return CreateExcelPackage(
                "VendorList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Vendors"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("SupplierCode"),
                    L("SupplierName"),
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
                    L("SupplierAccount"),
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
                        _ => _.SupplierCode,
                        _ => _.SupplierName,
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
                        _ => _.SupplierAccount,
                        _ => _.PaymentTerms,
                         _ => _.AccountNumber,
                          _ => _.Bank,
                        _ => _.Currency,
                        _ => _.Type,
                        _ => _.Branch
                        
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