using System;
using System.ComponentModel.DataAnnotations;

namespace GrooveMessengerDAL.Models
{
    public abstract class BaseModel<TKey>
    {
        [Required]
        public TKey Id { get; set; }

        [Required]
        public byte[] Timestamp { get; set; }

    }
}
