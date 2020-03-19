using System.Collections.Generic;
using System.Linq;

namespace Orange_Notes.Model
{
    public class Notes
    {
        private List<Note> notes;

        public Notes()
        {
            notes = new List<Note>();
        }

        public void AddNote()
        {
            if (!notes.Any())
            {
                notes.Add(new Note(1));
            }
            else
            {
                int i = notes.Max(n => n.noteId);
                i++;

                notes.Add(new Note(i));
            }
        }

        public bool RemoveNote(int noteId)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

            if (i != -1)
                notes.RemoveAt(i);
            else
                return false;

            return true;
        }

        public string GetNoteTitle(int noteId)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

            if (i != -1)
                return notes[i].noteTitle;
            else
                return null;
        }

        public string GetNoteContent(int noteId)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

            if (i != -1)
                return notes[i].noteContent;
            else
                return null;
        }

        public bool SetNoteTitle(int noteId, string noteTitle)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

            if (i != -1)
                notes[i].noteTitle = noteTitle;
            else
                return false;

            return true;
        }

        public bool SetNoteContent(int noteId, string noteContent)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

            if (i != -1)
                notes[i].noteContent = noteContent;
            else
                return false;

            return true;
        }
    }

    public class Note
    {
        public int noteId { get; set; }
        public string noteTitle { get; set; }
        public string noteContent { get; set; }

        public Note(int noteId)
        {
            this.noteId = noteId;
            this.noteTitle = "Notka" + " " + "#" + noteId;
        }
    }
}
