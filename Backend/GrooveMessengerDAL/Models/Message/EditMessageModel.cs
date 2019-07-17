using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.Message
{
    public class EditMessageModel : AuditBaseModel
    {
        public string Content { get; set; }
    }
}
