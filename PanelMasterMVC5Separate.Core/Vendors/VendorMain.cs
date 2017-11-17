using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Vendors
{
    [Table("tblVendorMain")]
    public class VendorMain : FullAuditedEntity
    {
        public virtual Guid SupplierCode { get; set; }
        public virtual string SupplierName { get; set; }        
        public virtual string RegistrationNumber { get; set; }
        public virtual string TaxRegistrationNumber { get; set; }
        public virtual int CountryID { get; set; }
        [ForeignKey("CountryID")]
        public virtual Countries Country { get; set; }
    }
}
