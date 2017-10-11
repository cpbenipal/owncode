 
using Abp.Domain.Entities.Auditing;
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
            string insurerName, string mask, string logo, int id)
        {
           // Bytes = byteArray;
            InsurerName = insurerName;
            Mask = mask;
            LogoPicture = logo;
            Id = id;
        }

        [Required]
        public virtual string InsurerName { get; set; }
        [Required]
        public virtual string LogoPicture { get; set; }
        [Required]
        public virtual string Mask { get; set; }
        //[Required]
        //public virtual byte[] Bytes { get; set; }
         
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
