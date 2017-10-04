using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Authorization.Claim;

namespace PanelMasterMVC5Separate.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}