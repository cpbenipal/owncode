using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace PanelMasterMVC5Separate.Authorization.Roles.Dto
{
    [AutoMapFrom(typeof(Role))]
    public class RoleListDto : EntityDto, IHasCreationTime
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsStatic { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreationTime { get; set; }

        public int? RoleCategoryID { get; set; }
    }

    public class RoleCategoriesDto
    {
        public int? ID { get; set; }
        public string Description { get; set; }
        public int? RolesCategoryID { get; set; }
    }
   
}