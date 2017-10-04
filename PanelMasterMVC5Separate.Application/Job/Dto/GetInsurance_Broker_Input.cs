using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Insurance_Brokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Job.Dto
{
    public class GetInsurance_Broker_Input
    {
        public string Filter { get; set; }
    }

    [AutoMapFrom(typeof(Insurance))]
    public class InsuranceListDto : FullAuditedEntityDto
    {
        public string Insurance_Desc { get; set; }
    }
    [AutoMapFrom(typeof(Broker))]
    public class BrokerListDto : FullAuditedEntityDto
    {
        public string Broker_Desc { get; set; }
    }

}
