using System.ComponentModel.DataAnnotations;

namespace GrooveNoteDAL.Models
{
    public abstract class BaseModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public byte[] Timestamp { get; set; }
    }
}
