using System.Collections.Generic;
using Abp.Dependency;
using Abp.RealTime;

namespace PanelMasterMVC5Separate.Authorization.Users
{
    public interface IUserLogoutInformer
    {
        void InformClients(IReadOnlyList<IOnlineClient> clients);
    }
}
