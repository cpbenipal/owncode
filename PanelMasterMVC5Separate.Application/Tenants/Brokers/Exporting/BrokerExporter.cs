﻿using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PanelMasterMVC5Separate.DataExporting.Excel.EpPlus;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Tenants.Brokers.Exporting
{
    public class BrokerExporter : EpPlusExcelExporterBase, IBrokerExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public BrokerExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }
        public FileDto ExportToFile(List<BrokerMasterDto> claimListDtos)
        {
            return CreateExcelPackage(
                "MasterBrokers.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("MasterBrokers"));
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
                        _ => _.BrokerName,
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
        public FileDto ExportToFile(List<BrokersListDto> claimListDtos)
        {
            return CreateExcelPackage(
                "BrokersList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Brokers"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("BrokerName"),
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
                    L("BrokerAccount"),
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
                        _ => _.BrokerName,
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
                        _ => _.BrokerAccount,
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
