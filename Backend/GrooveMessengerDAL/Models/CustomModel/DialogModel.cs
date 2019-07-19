using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class DialogModel
    {
        public string Who { get; set; }
        public string Message { get; set; }
        public DateTime? Time { get; set; }
    }
}
