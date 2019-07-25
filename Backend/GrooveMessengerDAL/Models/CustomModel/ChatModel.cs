using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class ChatModel
    {
        public Guid Id { get; set; }
        public ICollection<DialogModel> Dialog { get; set; }
    }
}
