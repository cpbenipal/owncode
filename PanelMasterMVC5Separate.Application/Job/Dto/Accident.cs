using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Vehicle;
using System.ComponentModel.DataAnnotations;

namespace PanelMasterMVC5Separate.Job.Dto
{
    public class Accident
    {
        //Provide Client Details
        public int Id { get; set; }
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
        public virtual int VId { get; set; }
        [Required]
        public virtual int MakeId { get; set; }
        [Required]
        public virtual int ModelId { get; set; }
        [Required]
        public virtual string Colour { get; set; }
        [Required]
        public virtual int PaintTypeId { get; set; }
        [Required]
        public virtual string Year { get; set; }
        [Required]
        public virtual string RegistrationNumber { get; set; }
        [Required]
        public virtual string VinNumber { get; set; }
        [Required]
        public virtual bool UnderWaranty { get; set; }
        [Required]
        public virtual bool IsSpecialisedType { get; set; }
        [Required]
        public virtual bool IsLuxury { get; set; }
        public virtual string VehicleOtherInformation { get; set; }
        public virtual bool New_Comeback { get; set; }

        //Repair Details

        [Required]
        public virtual int CSAID { get; set; }

        [Required]
        public virtual int JobStatusID { get; set; }
        [Required]
        public virtual int ClaimHandlerID { get; set; }
        [Required]
        public virtual int PartsBuyerID { get; set; }
        [Required]
        public virtual int KeyAccountManagerID { get; set; }
        [Required]
        public virtual int EstimatorID { get; set; }

        public virtual string NewComeback { get; set; }

        [Required]
        public virtual string DamangeReason { get; set; }
        [Required]
        public virtual string BranchEntryMethod { get; set; }
        //   [Required]
        public virtual bool IsUnrelatedDamangeReason { get; set; }
        [Required]
        public virtual string CurrentKMs { get; set; }

        public virtual string RepairOtherInformation { get; set; }

        // Vehicle Insurer Details
        [Required]
        public virtual int InsurerId { get; set; }
        [Required]
        public virtual int BrokerId { get; set; }
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
        public string PaintType { get; set; }
    }

    public class ImportDto
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual string Title { get; set; }

        public virtual string Email { get; set; }

        public virtual string Tel { get; set; }

        public virtual string IdNumber { get; set; }

        public virtual string CommunicationType { get; set; }

        public virtual bool ContactAfterService { get; set; }
        public virtual string ClientOtherInformation { get; set; }

        //Provide Vehicle Details        
        public virtual int VId { get; set; }

        public virtual int MakeId { get; set; }

        public virtual int ModelId { get; set; }

        public virtual string Colour { get; set; }

        public virtual int PaintTypeId { get; set; }

        public virtual string Year { get; set; }

        public virtual string RegistrationNumber { get; set; }

        public virtual string VinNumber { get; set; }

        public virtual bool UnderWaranty { get; set; }

        public virtual bool IsSpecialisedType { get; set; }

        public virtual bool IsLuxury { get; set; }

        public virtual string VehicleOtherInformation { get; set; }
    }

    [AutoMapTo(typeof(VehicleInsurance))]
    public class QuoteDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int InsurerId { get; set; }
        [Required]
        public virtual int BrokerId { get; set; }
        [Required]
        public virtual string ClaimAdministrator { get; set; }
        [Required]
        public virtual string PolicyNumber { get; set; }
        [Required]
        public virtual string ClaimNumber { get; set; }
    }
    [AutoMapTo(typeof(Jobs))]
    public class RepairDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string DamangeReason { get; set; }
        [Required]
        public virtual string BranchEntryMethod { get; set; }
        [Required]
        public virtual bool IsUnrelatedDamangeReason { get; set; }
        [Required]
        public virtual string CurrentKMs { get; set; }

        public virtual string OtherInformation { get; set; }
    }
    [AutoMapTo(typeof(BrVehicle))]
    public class VehicleDto : FullAuditedEntityDto
    {
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
        public virtual string RegistrationNumber { get; set; }
        [Required]
        public virtual string VinNumber { get; set; }
        [Required]
        public virtual string UnderWaranty { get; set; }
        [Required]
        public virtual bool IsSpecialisedType { get; set; }
        [Required]
        public virtual bool IsLuxury { get; set; }
        public virtual string OtherInformation { get; set; }
    }
    [AutoMapTo(typeof(Client))]
    public class ClientDto : FullAuditedEntityDto
    {
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
        public virtual string OtherInformation { get; set; }
    }
    public class GetClaimsInput
    {
        public string FilterText { get; set; }
        public string FilterText1 { get; set; }
    }

    //public class CreateJobDto : FullAuditedEntityDto
    //{
    //    public virtual ClientDto clientdto { get; set; }
    //    public virtual VehicleDto vehicledto { get; set; }
    //    public virtual RepairDto repairdto { get; set; }
    //    public virtual QuoteDto quotedto { get; set; }
    //}
}
