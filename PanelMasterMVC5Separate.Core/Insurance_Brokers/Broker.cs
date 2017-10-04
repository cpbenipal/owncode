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
    [Table("tblBroker")]
    public class Broker : FullAuditedEntity
    {
        public const int MaxStringlength = 500;

        [Required]
        [MaxLength(MaxStringlength)]
        public virtual string Broker_Desc { get; set; }
    }
}
