using System;

namespace GrooveNoteDAL.Models.Note
{
    public class IndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public bool IsDone { get; set; }
    }
}
