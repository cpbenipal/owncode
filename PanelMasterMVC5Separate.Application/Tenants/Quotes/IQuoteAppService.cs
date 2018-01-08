using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Job.Dto;
using PanelMasterMVC5Separate.Tenants.Quotes.Dto;
using System.Collections.Generic;
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

        Task<QuoteMasterDto> QuoteDetailToEdit(GetJobInput input);

        int CreateOrUpdateQuotation(QuoteMasterToDto input);

        QuoteSummaryDto GetQuoteJobSummary(GetQuoteInput input);

        ListResultDto<PaintTypesDto> GetPaintType();

        string GetActions();

        string GetLocations();

        string GetHeaders();

        ListResultDto<QuoteDetailDto> GetQuotes(GetQuoteInput input);

        void SaveQuote(string quote);

        void DeleteQuote(GetQuoteInput input);

    }
}
