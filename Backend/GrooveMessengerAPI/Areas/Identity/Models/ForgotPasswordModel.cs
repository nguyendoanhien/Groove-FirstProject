namespace GrooveMessengerAPI.Areas.Identity.Models
{
    public class ForgotPasswordModel
    {
        public string UserId { get; set; }
        public string Ctoken { get; set; }
        public string NewPassword { get; set; }
    }
}
