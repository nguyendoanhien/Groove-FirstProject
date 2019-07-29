using System;

namespace GrooveMessengerDAL.Models
{
    public abstract class AuditBaseModel<Tkey> : BaseModel<Tkey>
    {
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}