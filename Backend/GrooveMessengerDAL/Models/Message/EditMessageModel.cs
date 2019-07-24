using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.Message
{
    public class EditMessageModel 
    {
        public string Content { get; set; }
        public Guid Id { get; set; }
    }
}
