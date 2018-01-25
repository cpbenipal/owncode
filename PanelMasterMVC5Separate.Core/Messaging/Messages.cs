using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelMasterMVC5Separate.Messaging
{
    [Table("tblMessages")]
    public class Messages : FullAuditedEntity
    {
        public long UserId { get; set; }

        public int? TenantId { get; set; }

        public virtual string Subject { get; set; }
         
        public virtual string Body { get; set; }       
    }

    [Table("tblAttachments")]
    public class Attachments : FullAuditedEntity
    {
        public virtual int MessageId { get; set; }
        public virtual Messages Messages { get; set; }

        public virtual byte[] Attachment { get; set; }        
    }

    [Table("tblMessagesUserLinking")]
    public class MessagesUserLinking : FullAuditedEntity
    {
        public virtual int MessageId { get; set; }
        public virtual Messages Messages { get; set; }

        public long ToUserId { get; set; }

        public long FromUserId { get; set; }

        public bool IsRead { get; set; }

        public bool IsTrashed { get; set; }
    }
}
