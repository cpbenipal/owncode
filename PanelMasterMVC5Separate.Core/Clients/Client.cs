using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Clients
{
    [Table("brClient")]
    public class Client : FullAuditedEntity
    {
                
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Title { get; set; }
        public virtual string Email { get; set; }
        public virtual string Tel { get; set; }
        public virtual string CommunicationType { get; set; }        
        public virtual bool ContactAfterService { get; set; }

        public virtual string IdNumber { get; set; }
        public virtual string OtherInformation { get; set; }
    }
}
