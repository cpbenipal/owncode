using Abp.Domain.Entities.Auditing;
using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Claim
{
    //[StoredProcedure("JobDetails_StoredProc")]
    public class JobDetails_StoredProc : FullAuditedEntity
    {        
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Title { get; set; }        
        public virtual string Email { get; set; }
        public virtual string Tel { get; set; }
        public virtual string CommunicationType { get; set; }
        public virtual string ContactAfterService { get; set; }        
        public virtual string RegNo { get; set; }
        public virtual string VinNumber { get; set; }
        public virtual string Colour { get; set; }
        public virtual string Year { get; set; }
        public virtual string UnderWaranty { get; set; }
        public virtual string New_Comeback { get; set; }

        
    }
}
