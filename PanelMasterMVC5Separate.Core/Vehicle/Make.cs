using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Insurer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Vehicle
{
    [Table("tblVehicleMakes")]

    public class VehicleMake : FullAuditedEntity
    {
        public VehicleMake() { }

        public const int MaxLength = 500;

        [Required]
        [MaxLength(MaxLength)]
        public virtual string Description { get; set; }

        [Required]
        public virtual string LogoPicture { get; set; }

        [Required]
        public virtual bool IsActive { get; set; }

        public VehicleMake(string description, string logoPicture, int id)
        {
            Description = description;
            LogoPicture = logoPicture;
            IsActive = true;
            Id = id;
        }
    }

    [Table("tblVehicleModels")]
    public class VehicleModels : FullAuditedEntity
    {
        [Required]
        public virtual int VehicleMakeID { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }

        [Required]
        public virtual string Model { get; set; }

        [Required]
        public virtual string MMCode { get; set; }

    }

    [Table("tblVehiclemodelLogos")]
    public class VehicleModelLogos : FullAuditedEntity
    {
        [Required]
        public virtual byte[] Bytes { get; set; }

        [Required]
        public virtual int VehicleMakeID { get; set; }
        public virtual VehicleMake Make { get; set; }

        public VehicleModelLogos() { }
        public VehicleModelLogos(byte[] bytes, int vehiclemakeid)
            : this()
        {
            Bytes = bytes;
            VehicleMakeID = vehiclemakeid;
        }
    }

    [Table("tblPaintType")]
    public class PaintTypes : FullAuditedEntity
    {
        public virtual string PaintType { get; set; }
    }

    [Table("brVehicle")]
    public class BrVehicle : FullAuditedEntity
    {
        [Required]
        public virtual int TenantId { get; set; }

        public virtual int MakeId { get; set; }
        [ForeignKey("MakeId")]
        public virtual VehicleMake VehicleMake { get; set; }
         
        public virtual int ModelId { get; set; }
        [ForeignKey("ModelId")]
        public virtual VehicleModels VehicleModels { get; set; }
        [Required]
        public virtual string Color { get; set; }
         
        public virtual int PaintTypeId { get; set; }
        [ForeignKey("PaintTypeId")]
        public virtual PaintTypes PaintTypes { get; set; }
        [StringLength(4)]
        public virtual string Year { get; set; }
        public virtual string RegistrationNumber { get; set; }
        public virtual string VinNumber { get; set; }
        public virtual bool UnderWaranty { get; set; }
        public virtual bool IsSpecialisedType { get; set; }
        public virtual bool IsLuxury { get; set; }
        public virtual string OtherInformation { get; set; }
    }

    [Table("brINS")]
    public class VehicleInsurance : FullAuditedEntity
    { 
        public virtual int InsurerId { get; set; }
        [ForeignKey("InsurerId")]
        public virtual InsurerMaster InsurerMaster { get; set; }
      
        public virtual int BrokerId { get; set; }
        [ForeignKey("BrokerId")]
        public virtual BrokerMaster BrokerMaster { get; set; }

        public virtual string ClaimAdministrator { get; set; }
        public virtual string PolicyNumber { get; set; }
        public virtual string ClaimNumber { get; set; }
        public virtual string OtherInformation { get; set; }
    }
}
