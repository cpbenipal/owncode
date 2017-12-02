using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Abp.Runtime.Validation;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Quotings;
using PanelMasterMVC5Separate.RolesCategories;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.ComponentModel.DataAnnotations;

namespace PanelMasterMVC5Separate.AdminFunctions.Dto
{
    public class functionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public bool Enabled { get; set; }
        public bool ShowSwitch { get; set; }
    }
    public class PlanDto
    {
        public int Id { get; set; }
        public virtual string PlanName { get; set; }
        public virtual double Price { get; set; }
        public virtual string HeaderColor { get; set; }
        public virtual int Members { get; set; }
        public DateTime CreationTime { get; set; }
    }
    public class GetClaimsInput
    {
        public int Id { get; set; }
    }
    public class GetInput
    {
        public string Filter { get; set; }
    }

    public class BankDto
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string CountryCode { get; set; }
        public DateTime CreationTime { get; set; }
        public bool isActive { get; set; }
    }
    public class functionCCDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
    public class GetInputs : PagedAndSortedInputDto, IShouldNormalize
    {
        public int tableIndex { get; set; }
        public string Filter { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "BankName,CountryCode";
            }
        }
    }
    [AutoMapFrom(typeof(Countries))]
    public class CountriesDto : FullAuditedEntityDto
    {
        public virtual string Code { get; set; }

        public virtual string Country { get; set; }
    }

    [AutoMapFrom(typeof(Banks))]
    public class BankDetailDto : FullAuditedEntityDto
    {
        public string BankName { get; set; }
        public int CountryId { get; set; }
    }

    [AutoMapTo(typeof(Banks))]
    public class BankToDto : FullAuditedEntityDto
    {
        public string BankName { get; set; }
        public int CountryId { get; set; }
    }

    public class TableDescriptionDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int tableIndex { get; set; }
    }
    public class CodeDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int tableIndex { get; set; }
        public virtual int CountryID { get; set; }
    }
    public class StatusDto
    {
        public int Id { get; set; }
        public int tableIndex { get; set; }
        public bool Status { get; set; }
    }
    public class ActiveDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
    [AutoMapFrom(typeof(Jobstatus))]
    public class JobStatusDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
    }
    [AutoMapTo(typeof(Jobstatus))]
    public class JobStatusToDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
    }
    [AutoMapFrom(typeof(JobstatusMask))]
    public class JobStatusMaskDto : FullAuditedEntity
    {
        public virtual string Description1 { get; set; }
        public virtual string Description2 { get; set; }
        public virtual string Description3 { get; set; }
        public virtual bool Enabled { get; set; }
    }
    [AutoMapTo(typeof(JobstatusMask))]
    public class JobStatusMaskToDto : FullAuditedEntity
    {
        public virtual string Description1 { get; set; }
        public virtual string Description2 { get; set; }
        public virtual string Description3 { get; set; }
        public virtual bool Enabled { get; set; }
    }
    //Quote Status prob
    [AutoMapFrom(typeof(QuoteStatus))]
    public class QuoteStatusDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }
    [AutoMapTo(typeof(QuoteStatus))]
    public class QuoteStatusToDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }
    [AutoMapTo(typeof(RepairTypes))]
    public class RepairTypeToDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }
    [AutoMapFrom(typeof(RepairTypes))]
    public class RepairTypeDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }
    //RolesCategory
    [AutoMapFrom(typeof(RolesCategory))]
    public class RoleCategoryDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }
    [AutoMapTo(typeof(RolesCategory))]
    public class RoleCategoryToDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual bool Enabled { get; set; }
    }
    //SignOns
    [AutoMapFrom(typeof(SignonPlans))]
    public class SignOnDto : FullAuditedEntity
    {
        public virtual string PlanName { get; set; } 
        public virtual double Price { get; set; }
        public virtual string HeaderColor { get; set; }
        public virtual int Members { get; set; }
        public virtual bool isActive { get; set; }
    }
    [AutoMapTo(typeof(SignonPlans))]
    public class SignOnToDto : FullAuditedEntity
    {
        public virtual string PlanName { get; set; }
        [DataType(DataType.Currency)]
        public virtual double Price { get; set; }
        public virtual string HeaderColor { get; set; }
        public virtual int Members { get; set; }
        public virtual bool isActive { get; set; }
    }
    [AutoMapFrom(typeof(TowOperator))]
    public class TowOperatorDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public virtual string Country { get; set; }
        public virtual int CountryId { get; set; }
        public virtual bool isActive { get; set; }
    }
    [AutoMapTo(typeof(TowOperator))]
    public class TowOperatorToDto : FullAuditedEntity
    {
        public virtual string Description { get; set; }
        public int CountryId { get; set; }
        public virtual bool isActive { get; set; }
    }
}
