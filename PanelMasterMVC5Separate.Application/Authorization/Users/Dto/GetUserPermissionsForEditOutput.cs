using System.Collections.Generic;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Authorization.Permissions.Dto;

namespace PanelMasterMVC5Separate.Authorization.Claim.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}