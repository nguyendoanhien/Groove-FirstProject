using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrooveNoteDAL.Entities
{
    public abstract class AuditBaseEntity<TKey> : BaseEntity<TKey>, IAuditBaseEntity
    {
        [Column("UpdatedBy")]
        public string UpdatedBy { get; set; }

        [Column("UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }
    }

    public interface IAuditBaseEntity
    {
    }
}
