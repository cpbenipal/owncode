using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Insurance_Brokers
{
    [Table("tblInsurance")]
    public class Insurance : FullAuditedEntity
    {
        public const int MaxStringlength = 500;

        [Required]
        [MaxLength(MaxStringlength)]
        public virtual string Insurance_Desc { get; set; }
    }
}
