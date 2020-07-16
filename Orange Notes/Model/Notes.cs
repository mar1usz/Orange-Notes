using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orange_Notes.Model
{
    public class Notes
    {
        public ISerializer<List<Note>> Serializer { get; set; }
        private List<Note> notes = new List<Note>();

        public string AddNote()
        {
            if (!notes.Any())
            {
                notes.Add(new Note("1"));
                return "1";
            }
            else
            {
                int i_ = notes.Max(n => int.Parse(n.NoteId));
                i_++;
                string i = i_.ToString();
                notes.Add(new Note(i));
                return i;
            }
        }

        public bool AddNote(string noteId)
        {
            int i = notes.FindIndex(n => n.NoteId == noteId);
            if (i == -1)
            {
                notes.Add(new Note(noteId));
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveNote(string noteId)
        {
            int i = notes.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
            {
                notes.RemoveAt(i);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetNoteTitle(string noteId)
        {
            int i = notes.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
                return notes[i].NoteTitle;
            else
                return null;
        }

        public string GetNoteContent(string noteId)
        {
            int i = notes.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
                return notes[i].NoteContent;
            else
                return null;
        }

        public bool SetNoteTitle(string noteId, string noteTitle)
        {
            int i = notes.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
            {
                notes[i].NoteTitle = noteTitle;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetNoteContent(string noteId, string noteContent)
        {
            int i = notes.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
            {
                notes[i].NoteContent = noteContent;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContainsNote(string noteId)
        {
            int i = notes.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
                return true;
            else
                return false;
        }

        public List<string> GetNoteIds()
        {
            List<string> ids = new List<string>();
            foreach (Note n in notes)
                ids.Add(n.NoteId);
            return ids;
        }

        public async Task SerializeAsync(string filePath)
        {
            await Serializer.SerializeAsync(notes, filePath);
        }

        public async Task DeserializeAsync(string filePath)
        {
            notes = await Serializer.DeserializeAsync(filePath);
        }
    }

    public class Note
    {
        public string NoteId { get; set; }
        public string NoteTitle { get; set; }
        public string NoteContent { get; set; }

        public Note()
        { }

        public Note(string noteId)
        {
            NoteId = noteId;
            NoteTitle = "OrangeNote" + noteId;
            NoteContent = "";
        }
    }
}
