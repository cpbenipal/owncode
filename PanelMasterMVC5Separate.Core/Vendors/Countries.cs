using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.MultiTenancy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Vendors
{
    [Table("tblCoutries")]
    public class Countries : FullAuditedEntity
    {
        public virtual string Code { get; set; }
       
        public virtual string Country { get; set; }        

    }
}