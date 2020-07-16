using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orange_Notes.Model
{
    public class Notes
    {
        public ISerializer<List<Note>> Serializer { get; set; }
        private List<Note> NotesList = new List<Note>();

        public string AddNote()
        {
            if (!NotesList.Any())
            {
                NotesList.Add(new Note("1"));
                return "1";
            }
            else
            {
                int i_ = NotesList.Max(n => int.Parse(n.NoteId));
                i_++;
                string i = i_.ToString();
                NotesList.Add(new Note(i));
                return i;
            }
        }

        public bool AddNote(string noteId)
        {
            int i = NotesList.FindIndex(n => n.NoteId == noteId);
            if (i == -1)
            {
                NotesList.Add(new Note(noteId));
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveNote(string noteId)
        {
            int i = NotesList.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
            {
                NotesList.RemoveAt(i);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetNoteTitle(string noteId)
        {
            int i = NotesList.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
                return NotesList[i].NoteTitle;
            else
                return null;
        }

        public string GetNoteContent(string noteId)
        {
            int i = NotesList.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
                return NotesList[i].NoteContent;
            else
                return null;
        }

        public bool SetNoteTitle(string noteId, string noteTitle)
        {
            int i = NotesList.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
            {
                NotesList[i].NoteTitle = noteTitle;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetNoteContent(string noteId, string noteContent)
        {
            int i = NotesList.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
            {
                NotesList[i].NoteContent = noteContent;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContainsNote(string noteId)
        {
            int i = NotesList.FindIndex(n => n.NoteId == noteId);
            if (i != -1)
                return true;
            else
                return false;
        }

        public List<string> GetNoteIds()
        {
            List<string> ids = new List<string>();
            foreach (Note n in NotesList)
                ids.Add(n.NoteId);
            return ids;
        }

        public void Serialize(string filePath)
        {
            Serializer.Serialize(NotesList, filePath);
        }

        public void Deserialize(string filePath)
        {
            NotesList = Serializer.Deserialize(filePath);
        }

        public async Task SerializeAsync(string filePath)
        {
            await Serializer.SerializeAsync(NotesList, filePath);
        }

        public async Task DeserializeAsync(string filePath)
        {
            NotesList = await Serializer.DeserializeAsync(filePath);
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
