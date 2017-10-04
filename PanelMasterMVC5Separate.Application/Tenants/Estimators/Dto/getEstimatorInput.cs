using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Estimations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Estimators.Dto
{
    public class GetEstimatorInput
    {
        public string Filter { get; set; }
    }
    [AutoMapFrom(typeof(Estimator))]
    public class EstimatorListDto : FullAuditedEntityDto
    {
        public string Estimator_Desc { get; set; }
    }
}
