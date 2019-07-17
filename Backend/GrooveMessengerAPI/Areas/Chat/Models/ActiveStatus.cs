using System.ComponentModel;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class ActiveStatus
    {
        public string From { get; set; }
        public StatusName Status { get; set; }
    }
    public enum StatusName
    {
        Online,
        Away,
        [Description("Do not disturb")]
        DoNotDisturb,
        Offline
    }
}
