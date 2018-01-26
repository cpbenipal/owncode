using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.UserTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.UserTest.Dto
{
    [AutoMapFrom(typeof(uClass))]
    public class uDto : FullAuditedEntityDto
    {
        public virtual string FN { get; set; }
        public virtual string LN { get; set; }
    }

    public class GetBrokerInput
    {
        public string Filter { get; set; }
    }

}
