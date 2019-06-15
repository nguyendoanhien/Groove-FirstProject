using System;
using System.ComponentModel.DataAnnotations;

namespace GrooveNoteDAL.Models.Note
{
    public class CreateModel
    {
        [Required(ErrorMessage = "Please input this field")]
        [StringLength(50, ErrorMessage = "50 characters maximum")]
        public string Title { get; set; }
        [StringLength(255, ErrorMessage = "255 characters maximum")]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please input this field")]
        public DateTime Reminder { get; set; }
    }
}
