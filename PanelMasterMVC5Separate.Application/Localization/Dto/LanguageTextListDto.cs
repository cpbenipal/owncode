using Abp.Application.Services.Dto;

namespace PanelMasterMVC5Separate.Localization.Dto
{
    public class LanguageTextListDto
    {
        public string Key { get; set; }
        
        public string BaseValue { get; set; }
        
        public string TargetValue { get; set; }
    }
}