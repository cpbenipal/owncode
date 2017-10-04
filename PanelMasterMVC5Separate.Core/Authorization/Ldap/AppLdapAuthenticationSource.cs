using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.MultiTenancy;

namespace PanelMasterMVC5Separate.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
