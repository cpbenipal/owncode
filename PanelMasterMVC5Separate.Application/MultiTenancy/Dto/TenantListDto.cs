using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace PanelMasterMVC5Separate.MultiTenancy.Dto
{
    [AutoMapFrom(typeof (Tenant))]
    public class TenantListDto : EntityDto, IPassivable, IHasCreationTime
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }

        public string EditionDisplayName { get; set; }

        public string ConnectionString { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }
    }
    [AutoMapFrom(typeof(SignonPlans))]
    public class SignonPlansDto
    {
        public virtual int Id { get; set; }
        public virtual string PlanName { get; set; }
        public virtual double Price { get; set; }
        public virtual string HeaderColor { get; set; }
        public virtual int Members { get; set; }
    }
}