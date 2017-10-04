using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace PanelMasterMVC5Separate.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}