using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.UserTest
{
    public class uClass : FullAuditedEntity
    {
        public string FN { get; set; }
        public string LN { get; set; }
    }
}
