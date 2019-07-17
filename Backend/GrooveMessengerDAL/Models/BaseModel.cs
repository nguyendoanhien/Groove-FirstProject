using System.ComponentModel.DataAnnotations;

namespace GrooveMessengerDAL.Models
{
    public abstract class BaseModel
    {
        [Required]
        public int Id { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
