using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Claim.Dto
{
    [AutoMapFrom(typeof(Jobs))]
    public class BranchClaimListDto : FullAuditedEntityDto
    {
        public virtual int ClientID { get; set; }
        public virtual int ManufactureID { get; set; }
        public virtual int ModelID { get; set; }
        public virtual int InsuranceID { get; set; }
        public virtual int BrokerID { get; set; }
        public virtual int BranchID { get; set; }
        public virtual int FinancialID { get; set; }
        public virtual int CSAID { get; set; }
        public virtual int EstimatorID { get; set; }
        public virtual int ProductiveStaffID { get; set; }
        public virtual int ClaimStatusID { get; set; }
        public virtual int ClaimEventID { get; set; }

        public virtual string RegNo { get; set; }
        public virtual string VinNumber { get; set; }
        public virtual string Colour { get; set; }
        public virtual string Year { get; set; }
        public virtual string UnderWaranty { get; set; }
        public virtual bool New_Comeback { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }

        public string Insurance { get; set; }
        public string Broker { get; set; }
        public string Manufacture { get; set; }
        public string Model { get; set; }
        public string ClaimStatusDescription { get; set; }

    }
   
}
