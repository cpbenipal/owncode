using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Vehicle;
using System.ComponentModel.DataAnnotations;
using System;

namespace PanelMasterMVC5Separate.Tenants.Manufacturing.Dto
{

    public class MakeUpDto
    {
        [Required]
        public virtual string Description { get; set; }       
        public virtual string NewFileName { get; set; }
        public virtual int Id { get; set; }
        public string LogoPicture { get; set; }
    }

    [AutoMapFrom(typeof(VehicleMake))]
    public class VehicleMakeDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string Description { get; set; }

        [Required]
        public virtual string LogoPicture { get; set; }

        [Required]
        public virtual bool IsActive { get; set; }
    }
    [AutoMapFrom(typeof(VehicleModels))]
    public class VehicleFromModelsDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int VehicleMakeID { get; set; }

        [Required]
        public virtual string Model { get; set; }

        [Required]
        public virtual string MMCode { get; set; }
    }
    [AutoMapTo(typeof(VehicleModels))]
    public class VehicleModelsDto : FullAuditedEntityDto
    {
        [Required]
        public virtual int VehicleMakeID { get; set; }

        [Required]
        public virtual string Model { get; set; }

        [Required]
        public virtual string MMCode { get; set; }
    }
    public class GetVehicleInput
    {
        public string Filter { get; set; }
    }

    public class StatusVehicleDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }

    public class ModelMadeListDto
    { 
        public virtual int MadeID { get; set; }         
        public virtual string Model { get; set; }         
        public virtual string MMCode { get; set; } 
        public virtual string Make { get; set; } 
        public DateTime CreationTime { get; set; }
    }
    
    public class MakelListDto 
    {
        public virtual int MakeID { get; set; }
        public virtual string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
    }

    [AutoMapFrom(typeof(VehicleModels))]
    public class VehicleModelsFDto : FullAuditedEntityDto
    { 
        public virtual string Make { get; set; } 
        public virtual string Model { get; set; }         
        public virtual string MMCode { get; set; }
    }
}