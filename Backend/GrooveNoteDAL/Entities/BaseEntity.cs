using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrooveNoteDAL.Entities
{
    public abstract class BaseEntity<TKey>: IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Column("Deleted")]
        public bool? Deleted { get; set; }

        [Column("CreatedBy")]
        [Required]
        public string CreatedBy { get; set; }
        
        [Column("CreatedOn")]
        [Required]
        public DateTime? CreatedOn { get; set; }
    }

    public interface IBaseEntity
    {

    }
}
