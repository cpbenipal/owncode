﻿using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Vendors
{
    [Table("tblCurrency")]
    public class Currencies : FullAuditedEntity
    {
        [Required]
        public virtual string CurrencyCode { get; set; }
        [Required]
        public virtual string CurrencyType { get; set; }
    }
}
