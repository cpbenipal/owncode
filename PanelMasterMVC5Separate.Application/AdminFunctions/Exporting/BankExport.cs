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
        public FileDto ExportToFile(List<JobStatusDto> claimListDtos)
        {
            return CreateExcelPackage(
                "JobStatus.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("JobStatus"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("JobStatus"), 
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.Description, 
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

        public FileDto ExportToFile(List<JobStatusMaskDto> claimListDtos)
        {
            return CreateExcelPackage(
                "JobStatusMask.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("JobStatusMask"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("JobStatus"),
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.Description1,
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

        public FileDto ExportToFile(List<QuoteStatusDto> claimListDtos)
        {
            return CreateExcelPackage(
                "QuoteStatus.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("QuoteStatus"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("Description"),
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.Description,
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

        public FileDto ExportToFile(List<RepairTypeDto> claimListDtos)
        {
            return CreateExcelPackage(
                "RepairType.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("RepairType"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("Description"),
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.Description,
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

        public FileDto ExportToFile(List<RoleCategoryDto> claimListDtos)
        {
            return CreateExcelPackage(
                "RoleCategory.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("RoleCategory"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("Description"),
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.Description,
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

        public FileDto ExportToFile(List<SignOnDto> claimListDtos)
        {
            return CreateExcelPackage(
                "SignOnPlans.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("SignOnPlans"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                     sheet,
                    L("Planname"),
                    L("Price"),
                    L("Members"),
                    L("HeaderColor"),
                    L("CreationTime")
                 );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.PlanName,
                        _ => _.Price,
                        _ => _.Members,
                        _ => _.HeaderColor,                         
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
