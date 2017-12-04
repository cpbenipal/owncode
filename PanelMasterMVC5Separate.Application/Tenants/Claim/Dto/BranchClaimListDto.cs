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
using System.ComponentModel.DataAnnotations;
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
        public virtual int ProductiveStaffID { get; set; }       
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
       
    }

    public class JobStatusDto
    {
        public virtual int Id { get; set; }
        public virtual int JobStatusId { get; set; }
        public virtual string Jobstatus { get; set; }
        public virtual Boolean IsActive { get; set; }
        public virtual string ShowSpeedbump { get; set; }
        public virtual string ShowAwaiting { get; set; }
        public virtual string JobstatusMask { get; set; }
        public virtual int Sortorder { get; set; }
        public virtual string CreationTime { get; set; }
        public int pkId { get; set; }
    }

    [AutoMapFrom(typeof(JobstatusTenant))]
    public class JobstatusTenantDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int JobStatusID { get; set; }
        [Required]
        public virtual int Tenant { get; set; }
        [Required]
        public virtual bool isActive { get; set; }
        [Required]
        public virtual bool ShowSpeedbump { get; set; }
        [Required]
        public virtual bool ShowAwaiting { get; set; }
        [Required]
        public virtual int Sortorder { get; set; }
        [Required]
        public virtual int Mask { get; set; }
        public virtual string JobStatus { get; set; }
    }

    [AutoMapFrom(typeof(JobstatusMask))]
    public class JobStatusMasksDto
    {
        public virtual int Id { get; set; }
        public virtual string Description1 { get; set; }
        public virtual string Description2 { get; set; }
        public virtual string Description3 { get; set; }       
    }

    public class GetJobInput
    {
        public int id { get; set; } 
    }

    [AutoMapTo(typeof(JobstatusTenant))]
    public class JobstatusTenantToDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int JobStatusID { get; set; }
        [Required]
        public virtual int Tenant { get; set; }
        [Required]
        public virtual bool isActive { get; set; }
        [Required]
        public virtual bool ShowSpeedbump { get; set; }
        [Required]
        public virtual bool ShowAwaiting { get; set; }
        [Required]
        public virtual int Sortorder { get; set; }
        [Required]
        public virtual int Mask { get; set; } 
    }
     
    public class SortOrder
    { 
        public virtual int Order { get; set; } 
    }
    [AutoMapFrom(typeof(TowSubOperator))]
    public class TowOperatorDto : FullAuditedEntityDto
    { 
        public virtual string Description { get; set; }
        public virtual string ContactNumber { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string EmailAddress { get; set; } 
        public virtual Boolean isActive { get; set; }        
        public virtual string Country { get; set; }
        public virtual int SubpkId { get; set; }
        public virtual int TenantId { get; set; }
        public virtual int TowOperatorId { get; set; }
    }

    [AutoMapTo(typeof(TowSubOperator))]
    public class TowTenantDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int TenantId { get; set; }
        [Required]
        public virtual string Description { get; set; }
        [Phone]
        public virtual string ContactNumber { get; set; }        
        public virtual string ContactPerson { get; set; }
        [EmailAddress]
        public virtual string EmailAddress { get; set; }
        [Required]
        public virtual bool isActive { get; set; }
        [Required]
        public virtual int CountryID { get; set; }
        public virtual int TowOperatorId { get; set; }
    }
    [AutoMapFrom(typeof(TowOperator))]
    public class TowOperatorMainDto : FullAuditedEntity
    {
        public virtual string Description { get; set; } 
        public virtual bool isActive { get; set; }
    }
    [AutoMapTo(typeof(TowOperator))]
    public class TowOperatorMainToDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }       
        public virtual bool isActive { get; set; }
    }
}
