namespace GrooveMessengerDAL.Entities
{
    public class NoteEntity : AuditBaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}