using System.Threading.Tasks;
using PanelMasterMVC5Separate.Sessions.Dto;

namespace PanelMasterMVC5Separate.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
