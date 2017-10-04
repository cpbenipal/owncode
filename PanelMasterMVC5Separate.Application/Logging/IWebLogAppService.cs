using Abp.Application.Services;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Logging.Dto;

namespace PanelMasterMVC5Separate.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
