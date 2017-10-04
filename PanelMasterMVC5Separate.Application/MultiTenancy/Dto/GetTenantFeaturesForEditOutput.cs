using System.Collections.Generic;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Editions.Dto;

namespace PanelMasterMVC5Separate.MultiTenancy.Dto
{
    public class GetTenantFeaturesForEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}