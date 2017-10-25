﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Quotings;
using System.ComponentModel.DataAnnotations;

namespace PanelMasterMVC5Separate.Tenants.Quotes.Dto
{
    public class GetQuoteInput
    {
        public string Filter { get; set; }
    }

    public class GetJobInput
    {
        public int id { get; set; }
        public int jobId { get; set; }
    }
    [AutoMapFrom(typeof(QuoteMaster))]
    public class QuoteMastersDto : FullAuditedEntityDto
    {
        public virtual int JobId { get; set; }

        [Required]
        public virtual string Job { get; set; }

        [Required]
        public virtual string QuoteStatus { get; set; }

        [Required]
        public virtual string QuoteCat { get; set; }

        [Required]
        public virtual string RepairType { get; set; }

        [Required]
        public virtual string Value { get; set; }
    }

    [AutoMapFrom(typeof(QuoteMaster))]
    public class QuoteMasterDto : FullAuditedEntityDto
    { 
        [Required]
        public virtual int TenantId { get; set; }

        [Required]
        public virtual int JobId { get; set; }

        [Required]
        public virtual int QuoteStatusID { get; set; }

        [Required]
        public virtual int QuoteCatID { get; set; }

        [Required]
        public virtual int RepairTypeId { get; set; }

        [Required]
        public virtual bool Pre_Auth { get; set; }
         
        public virtual string Value { get; set; }

        public virtual string Comments { get; set; }

        public string RegNo { get; set; }
    }
    [AutoMapTo(typeof(QuoteMaster))]
    public class QuoteMasterToDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int TenantId { get; set; }

        [Required]
        public virtual int JobId { get; set; }

        [Required]
        public virtual int QuoteStatusID { get; set; }

        [Required]
        public virtual int QuoteCatID { get; set; }

        [Required]
        public virtual int RepairTypeId { get; set; }

        [Required]
        public virtual bool Pre_Auth { get; set; }

        public virtual string Value { get; set; }

        public virtual string Comments { get; set; }

        public string RegNo { get; set; }
    }
    [AutoMapFrom(typeof(QuoteStatus))]
    public class QuoteStatusDto
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
    }

    [AutoMapFrom(typeof(QuoteCategories))]
    public class QuoteCategoriesDto
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
    }

    [AutoMapFrom(typeof(RepairTypes))]
    public class RepairTypeDto
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
    }

    public class QuoteSummaryDto 
    { 
        public virtual int JobId { get; set; }
         
        public virtual string QuoteStatus { get; set; }
         
        public virtual string QuoteCat { get; set; }
         
        public virtual string RepairType { get; set; }
         
        public virtual string Value { get; set; }

        public virtual string Pre_Auth { get; set; } // true:Yes false:No

        public virtual string VehicleYear { get; set; }

        public virtual string VehicleMake { get; set; }

        public virtual string VehicleModal { get; set; }

        public virtual string VehicleColor { get; set; }

        public virtual string VehicleReg { get; set; }

        public virtual string VehicleVin { get; set; }

        public virtual string VehicleCreatedBy { get; set; }

        public virtual string Insurer { get; set; }

        public virtual string Broker { get; set; }

        public virtual string QuoteCreated { get; set; }

        public virtual int Id { get; set; }
    }
}