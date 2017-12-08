using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Vehicle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Job.Dto
{
    public class Accident
    {
        //Provide Client Details
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Surname { get; set; }
        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual string Email { get; set; }
        [Required]
        public virtual string Tel { get; set; }
        [Required]
        public virtual string IdNumber { get; set; }
        [Required]
        public virtual string CommunicationType { get; set; }
        [Required]
        public virtual bool ContactAfterService { get; set; }
        public virtual string ClientOtherInformation { get; set; }

        //Provide Vehicle Details        
        [Required]
        public virtual int MakeID { get; set; }
        [Required]
        public virtual int ModelID { get; set; }
        [Required]
        public virtual string Colour { get; set; }
        [Required]
        public virtual int PaintTypeID { get; set; }
        [Required]
        public virtual string Year { get; set; }
        [Required]
        public virtual string RegNo { get; set; }
        [Required]
        public virtual string VinNumber { get; set; }
        [Required]
        public virtual string UnderWaranty { get; set; }
        [Required]
        public virtual bool New_Comeback { get; set; }
        [Required]
        public virtual bool IsSpecialisedType { get; set; }
        [Required]
        public virtual bool IsLuxury { get; set; }
        public virtual string VehicleOtherInformation { get; set; }

        //Repair Details
        [Required]
        public virtual string DamangeReason { get; set; }
        [Required]
        public virtual string BranchEntryMethod { get; set; }
        [Required]
        public virtual bool IsUnrelatedDamangeReason { get; set; }
        [Required]
        public virtual string CurrentKMs { get; set; }
        public virtual string RepairOtherInformation { get; set; }
        // Vehicle Insurer Details
        [Required]
        public virtual int InsuranceID { get; set; }
        [Required]
        public virtual int BrokerID { get; set; }
        [Required]
        public virtual string ClaimAdministrator { get; set; }
        [Required]
        public virtual string PolicyNumber { get; set; }
        [Required]
        public virtual string ClaimNumber { get; set; }
        public virtual string InsurerOtherInformation { get; set; }
    }

    [AutoMapFrom(typeof(PaintTypes))]
    public class PaintTypesDto : FullAuditedEntityDto
    {
        public virtual string PaintType { get; set; }
    }
}
