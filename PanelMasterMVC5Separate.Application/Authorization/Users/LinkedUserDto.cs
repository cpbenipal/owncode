using System;
using Abp.Application.Services.Dto;

namespace PanelMasterMVC5Separate.Authorization.Claim
{
    public class LinkedUserDto : EntityDto<long>
    {
        public int? TenantId { get; set; }

        public string TenancyName { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Occupation { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public object GetShownLoginName(bool multiTenancyEnabled)
        {
            if (!multiTenancyEnabled)
            {
                return Username;
            }

            return string.IsNullOrEmpty(TenancyName)
                ? ".\\" + Username
                : TenancyName + "\\" + Username;
        }
    }
}