using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using Abp.Extensions;
using PanelMasterMVC5Separate.Authorization.Permissions;
using PanelMasterMVC5Separate.Authorization.Permissions.Dto;
using PanelMasterMVC5Separate.Authorization.Roles.Dto;
using Abp.Domain.Repositories;

namespace PanelMasterMVC5Separate.Authorization.Roles
{
    /// <summary>
    /// Application service that is used by 'role management' page.
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_Roles)]
    public class RoleAppService : PanelMasterMVC5SeparateAppServiceBase, IRoleAppService
    {
        private readonly RoleManager _roleManager;
       
        private readonly IRepository<RolesCategories.RolesCategory>  _roleCategoriesRepository;

        public RoleAppService(RoleManager roleManager, IRepository<RolesCategories.RolesCategory> roleCategories)
        {
            _roleManager = roleManager;
            _roleCategoriesRepository = roleCategories;           
        }

        public async Task<ListResultDto<RoleListDto>> GetRoles(GetRolesInput input)
        {
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(roles.MapTo<List<RoleListDto>>());
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Create, AppPermissions.Pages_Administration_Roles_Edit)]
        public async Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var grantedPermissions = new Permission[0];
            RoleEditDto roleEditDto;

            if (input.Id.HasValue) //Editing existing role?
            {
                var role = await _roleManager.GetRoleByIdAsync(input.Id.Value);
                grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                roleEditDto = role.MapTo<RoleEditDto>();
            }
            else
            {
                roleEditDto = new RoleEditDto();
            }

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = permissions.MapTo<List<FlatPermissionDto>>().OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        public ListResultDto<RoleCategoriesDto> GetRolesCategories(NullableIdDto input)
        {
            var newList = new List<RoleCategoriesDto>();

            if (input.Id.HasValue)
            {

                var role = _roleManager.Roles.Where(r => r.Id.Equals(input.Id.Value)).FirstOrDefault();

                var selected_role_category = _roleCategoriesRepository.GetAll()
                 .Where(p => p.Enabled.Equals(true) && p.Id.Equals(role.RoleCategoryID.Value))
                 .FirstOrDefault();

                var role_categories = _roleCategoriesRepository.GetAll()
                 .Where(p => p.Enabled.Equals(true) && !p.Id.Equals(role.RoleCategoryID.Value))
                 .ToList();

                newList.Add(new RoleCategoriesDto{ID = selected_role_category.Id,Description = selected_role_category.Description});

                foreach (RolesCategories.RolesCategory cat_obj in role_categories)
                {
                    newList.Add(new RoleCategoriesDto
                    {
                        ID = cat_obj.Id,
                        Description = cat_obj.Description
                    });
                }
            }
            else
            {
                var role_categories = _roleCategoriesRepository.GetAll()
                .Where(p => p.Enabled.Equals(true))
                .ToList();

                foreach (RolesCategories.RolesCategory cat_obj in role_categories)
                {
                    newList.Add(new RoleCategoriesDto
                    {
                        ID = cat_obj.Id,
                        Description = cat_obj.Description
                    });
                }
            }

            return new ListResultDto<RoleCategoriesDto>(newList);
        }

        public async Task CreateOrUpdateRole(CreateOrUpdateRoleInput input)
        {
            if (input.Role.Id.HasValue)
            {
                await UpdateRoleAsync(input);
            }
            else
            {
                await CreateRoleAsync(input);
            }
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Delete)]
        public async Task DeleteRole(EntityDto input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Edit)]
        protected virtual async Task UpdateRoleAsync(CreateOrUpdateRoleInput input)
        {
            Debug.Assert(input.Role.Id != null, "input.Role.Id should be set.");

            var role = await _roleManager.GetRoleByIdAsync(input.Role.Id.Value);
            role.DisplayName = input.Role.DisplayName;
            role.IsDefault = input.Role.IsDefault;
            role.RoleCategoryID = input.Role.RolesCategoryID;

            await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
        }

        
        [AbpAuthorize(AppPermissions.Pages_Administration_Roles_Create)]
        protected virtual async Task CreateRoleAsync(CreateOrUpdateRoleInput input)
        {
            var role = new Role(AbpSession.TenantId, input.Role.DisplayName) { IsDefault = input.Role.IsDefault, RoleCategoryID = input.Role.RolesCategoryID };
            CheckErrors(await _roleManager.CreateAsync(role));
            await CurrentUnitOfWork.SaveChangesAsync(); //It's done to get Id of the role.
            await UpdateGrantedPermissionsAsync(role, input.GrantedPermissionNames);
        }

        private async Task UpdateGrantedPermissionsAsync(Role role, List<string> grantedPermissionNames)
        {
            var grantedPermissions = PermissionManager.GetPermissionsFromNamesByValidating(grantedPermissionNames);
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }
    }
}
