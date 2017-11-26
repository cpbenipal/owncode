 
using Abp.Domain.Entities.Auditing;
using PanelMasterMVC5Separate.Vendors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Insurer
{
    [Table("tblInsurerMaster")]
    public class InsurerMaster : FullAuditedEntity
    {
        public InsurerMaster() { }
         

        public InsurerMaster(
            //byte[] byteArray, 
            string insurerName, string mask, string logo, int id , int countryId)
        {
           // Bytes = byteArray;
            InsurerName = insurerName;
            Mask = mask;
            LogoPicture = logo;
            Id = id;
            CountryID = countryId;
            IsActive = true;
        }

        [Required]
        public virtual string InsurerName { get; set; }
        [Required]
        public virtual string LogoPicture { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        public virtual int CountryID { get; set; }
        [ForeignKey("CountryID")]
        public virtual Countries Country { get; set; }

        public virtual bool IsActive { get; set; }
    }
    [Table("tblInsurerMasterPics")]
    public class InsurerPics : FullAuditedEntity
    { 

        [Required]
        public virtual byte[] Bytes { get; set; }
        [Required]
        public virtual int InsurerID { get; set; }
        [ForeignKey("InsurerID")]
        public virtual InsurerMaster InsurerMasters { get; set; }
        public InsurerPics(){}
        public InsurerPics(byte[] bytes,int insurerId)
            : this()
        { 
            Bytes = bytes;
            InsurerID = insurerId; 
        } 
    }
}
