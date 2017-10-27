using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.RolesCategories
{
    [Table("tblRolesCategory")]
    public class RolesCategory : FullAuditedEntity
    {
        public virtual string Description { set; get; }
        public virtual bool Enabled { get; set; }
    }
}
