using System.Threading.Tasks;
using Abp.Application.Services;
using PanelMasterMVC5Separate.Sessions.Dto;

namespace PanelMasterMVC5Separate.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
