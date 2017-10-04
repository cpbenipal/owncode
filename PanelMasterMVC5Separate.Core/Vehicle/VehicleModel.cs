using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Vehicle
{
    [Table("tblVehicleModel")]
    public class VehicleModel : FullAuditedEntity
    {
        public const int MaxLength = 500;

        [Required]        
        public virtual int ManufactureID { get; set; }

        [Required]
        [MaxLength(MaxLength)]
        public virtual string Model_Desc { get; set; }
    }
}
