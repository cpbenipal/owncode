using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Vendors
{
    [Table("tblBanks")]
    public class Banks : FullAuditedEntity
    {
        public virtual string BankName { get; set; }
    }
}
