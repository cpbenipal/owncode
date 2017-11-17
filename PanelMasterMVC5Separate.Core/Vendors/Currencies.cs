using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.MultiTenancy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Vendors
{
    [Table("tblCurrency")]
    public class Currencies : FullAuditedEntity
    {       
        public virtual string CurrencyCode { get; set; }        
        public virtual string CurrencyType { get; set; }
    }

    [Table("tblCountryCurrency")]
    public class CountryandCurrency : FullAuditedEntity
    {
        public virtual string CountryAndCurrency { get; set; }     
        public virtual string CurrencyCode { get; set; }
        public virtual string GraphicImage { get; set; }
        public virtual string FontCode2000 { get; set; }
        public virtual string FontArialUnicodeMS { get; set; }
        public virtual string UnicodeDecimal { get; set; }
        public virtual string UnicodeHex { get; set; }        
    }
}
