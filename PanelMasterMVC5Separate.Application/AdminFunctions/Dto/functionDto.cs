using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Vendors;
using System;

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
    public class BankDto
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string CountryCode { get; set; }
        public DateTime CreationTime { get; set; }
    }
    public class functionCCDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }        
    }
    public class GetInputs
    {
        public int tableIndex { get; set; }
        public string Filter { get; set; }
    }
    [AutoMapFrom(typeof(Countries))]
    public class CountriesDto : FullAuditedEntityDto
    {
        public virtual string Code { get; set; }
          
        public virtual string Country { get; set; }
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
}
