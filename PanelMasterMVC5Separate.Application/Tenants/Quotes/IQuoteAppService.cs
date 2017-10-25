using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Tenants.Quotes.Dto;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Quotes
{
    public interface IQuoteAppService: IApplicationService
    {
        ListResultDto<QuoteMastersDto> ViewQuotations(GetQuoteInput input);

        ListResultDto<QuoteStatusDto> GetQuoteStatus();

        ListResultDto<QuoteCategoriesDto> GetQuoteCategories();

        ListResultDto<RepairTypeDto> GetRepairTypes();

        Task<QuoteMasterDto> GetQuoteForNewQuotation(GetJobInput input);

        int CreateOrUpdateQuotation(QuoteMasterToDto input);

        QuoteSummaryDto GetQuoteJobSummary(GetQuoteInput input);
    }
}
