using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using PanelMasterMVC5Separate.Authorization.Claim.Dto;
using PanelMasterMVC5Separate.DataExporting.Excel.EpPlus;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Claim.Dto;

namespace PanelMasterMVC5Separate.Tenants.Claim.Exporting
{
    public class ClaimsListExcelExporter : EpPlusExcelExporterBase, IClaimsListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ClaimsListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<BranchClaimListDto> claimListDtos)
        {
            return CreateExcelPackage(
                "ClaimsList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Claims"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Surname"),
                        L("EmailAddress")
                    );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.BranchID,
                        _ => _.CSAID,
                        _ => _.Colour
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
                "JobStatusList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("JobStatus"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Jobstatus"),
                        L("JobstatusMask"),
                        L("Sortorder"),
                        L("ShowAwaiting"),
                        L("ShowSpeedbump"),
                        L("IsActive")
                    );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.Jobstatus,
                        _ => _.JobstatusMask,
                        _ => _.Sortorder,
                        _ => _.ShowAwaiting,
                        _ => _.ShowSpeedbump,
                        _ => _.IsActive
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


        public FileDto ExportToFile(List<TowOperatorDto> claimListDtos)
        {
            return CreateExcelPackage(
                "TowOperatorList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("TowOperators"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Description"),
                        L("ContactNumber"),
                        L("ContactPerson"),
                        L("EmailAddress"),                        
                        L("IsActive")
                    );

                    AddObjects(
                        sheet, 2, claimListDtos,
                        _ => _.Description,
                        _ => _.ContactNumber,
                        _ => _.ContactPerson,
                        _ => _.EmailAddress,                         
                        _ => _.isActive
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
