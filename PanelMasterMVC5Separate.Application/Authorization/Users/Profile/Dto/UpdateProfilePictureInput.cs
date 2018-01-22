using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace PanelMasterMVC5Separate.Authorization.Claim.Profile.Dto
{
    public class UpdateProfilePictureInput
    {
        [Required]
        [MaxLength(400)]
        public string FileName { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }

    [AutoMapFrom(typeof(MultiTenancy.TenantProfile))]
    public class MyInfo : FullAuditedEntityDto
    {
        [Required]
        public string FullName { get; set; }        
        [Required]
        [Phone]
        public string CellNumber { get; set; }
        public string CompanyName { get; set; }
    }
}