using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace GrooveMessengerDAL.Entities
{
    public abstract class BaseEntity<TKey> : IBaseEntity
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
        public BaseEntity()
        {


        }
    }

    public interface IBaseEntity
    {

    }
}
