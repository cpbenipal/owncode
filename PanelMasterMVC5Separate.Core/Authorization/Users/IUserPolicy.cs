using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace PanelMasterMVC5Separate.Authorization.Claim
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
