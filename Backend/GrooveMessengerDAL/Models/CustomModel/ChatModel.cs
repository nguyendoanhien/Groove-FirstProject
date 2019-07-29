using System;
using System.Collections.Generic;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class ChatModel
    {
        public string Id { get; set; }
        public ICollection<DialogModel> Dialog { get; set; }
    }
}