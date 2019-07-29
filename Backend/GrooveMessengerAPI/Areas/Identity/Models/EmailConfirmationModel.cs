namespace GrooveMessengerAPI.Areas.Identity.Models
{
    public class EmailConfirmationModel
    {
        public string UserId { get; set; }
        public string Ctoken { get; set; }
    }
}