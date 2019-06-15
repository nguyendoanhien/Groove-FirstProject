using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrooveNoteDAL.Models.Note
{
    public class EditModel : AuditBaseModel
    {
        [Required(ErrorMessage = "Please input this field")]
        [StringLength(50, ErrorMessage = "50 characters maximum")]
        public string Title { get; set; }
        [StringLength(255, ErrorMessage = "255 characters maximum")]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please input this field")]
        public DateTime Reminder { get; set; }
        [DisplayName("Is done?")]
        public bool IsDone { get; set; }
    }
}
