using Abp.Domain.Entities.Auditing;
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
}
