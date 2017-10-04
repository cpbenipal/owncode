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
    [Table("tblManufacture")]
    public class Manufacture : FullAuditedEntity
    {
        public const int MaxLength = 500;
      
        [Required]
        [MaxLength(MaxLength)]
        public virtual string Manufacture_Desc { get; set; }
    }
}
