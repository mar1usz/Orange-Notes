using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Orange_Notes.Model
{
    public class Notes
    {
        private List<Note> notes;

        public Notes(bool deserialize, string deserializeFilePath)
        {
            notes = new List<Note>();

            if(deserialize == true)
                Deserialize(deserializeFilePath);
        }

        public int AddNote()
        {
            if (!notes.Any())
            {
                notes.Add(new Note(1));
                return 1;
            }
            else
            {
                int i = notes.Max(n => n.noteId);
                i++;

                notes.Add(new Note(i));
                return i;
            }
        }

        public bool RemoveNote(int noteId)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

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
            {
                notes[i].noteTitle = noteTitle;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetNoteContent(int noteId, string noteContent)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

            if (i != -1)
            {
                notes[i].noteContent = noteContent;
                return true;
            }
            else
            {
                return false;
            }            
        }

        public List<int> GetNoteIds()
        {
            List<int> ids = new List<int>();
            foreach (Note n in notes)
                ids.Add(n.noteId);

            return ids;
        }

        public void Serialize(string filePath)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(notes, jsonOptions);
            File.WriteAllText(filePath, jsonString);
        }

        public void Deserialize(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                notes = JsonSerializer.Deserialize<List<Note>>(jsonString);
            }
        }
    }

    public class Note
    {
        public int noteId { get; set; }
        public string noteTitle { get; set; }
        public string noteContent { get; set; }

        public Note()
        { }

        public Note(int noteId)
        {
            this.noteId = noteId;
            this.noteTitle = "Notka" + noteId;
            this.noteContent = "";
        }
    }
}
