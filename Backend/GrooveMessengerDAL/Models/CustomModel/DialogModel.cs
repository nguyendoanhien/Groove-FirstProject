using System;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class DialogModel
    {
        public Guid Id { get; set; }
        public string Who { get; set; }
        public string Message { get; set; }
        public DateTime? Time { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
    }
}