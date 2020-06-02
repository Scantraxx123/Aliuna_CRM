using System;

namespace Aliuna.Model.Subclasses
{
    public class Note
    {
        public string NoteText { get; set; }

        public DateTime NoteDate { get; } = DateTime.Now;

        public Note(string NoteText)
        {
            this.NoteText = NoteText;
        }

    }
}
