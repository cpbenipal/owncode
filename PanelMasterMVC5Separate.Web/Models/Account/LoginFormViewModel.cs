using PanelMasterMVC5Separate.MultiTenancy;
using System.Collections.Generic;

namespace PanelMasterMVC5Separate.Web.Models.Account
{
    public class LoginFormViewModel
    {
        public string TenancyName { get; set; }

        public string SuccessMessage { get; set; }

        public string UserNameOrEmailAddress { get; set; }

        public bool IsSelfRegistrationEnabled { get; set; }

        public List<Tenant> listTenancyNames { get; set; }
    }     
}