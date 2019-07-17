using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrooveMessengerDAL.Entities
{
    public abstract class BaseEntity<TKey>: IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        [Column("Deleted")]
        public bool? Deleted { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }
        
        [Column("CreatedOn")]
        [Required]
        public DateTime? CreatedOn { get; set; }
    }

    public interface IBaseEntity
    {

    }
}
