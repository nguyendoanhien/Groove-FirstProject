using System;

namespace GrooveNoteDAL.Models
{
    public abstract class AuditBaseModel : BaseModel
    {
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
