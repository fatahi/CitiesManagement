using System;

namespace Framework.Domain
{
    public abstract class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
    }
}
