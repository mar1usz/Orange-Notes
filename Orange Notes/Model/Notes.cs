using System;
using System.Collections.Generic;
using System.Linq;

namespace Orange_Notes.Model
{
    public class Notes
    {
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
                int i_ = notes.Max(n => Int32.Parse(n.noteId));
                i_++;

                string i = i_.ToString();
                notes.Add(new Note(i));
                return i;
            }
        }

        public bool AddNote(string noteId)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

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

        public string GetNoteTitle(string noteId)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

            if (i != -1)
                return notes[i].noteTitle;
            else
                return null;
        }

        public string GetNoteContent(string noteId)
        {
            int i = notes.FindIndex(n => n.noteId == noteId);

            if (i != -1)
                return notes[i].noteContent;
            else
                return null;
        }

        public bool SetNoteTitle(string noteId, string noteTitle)
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

        public bool SetNoteContent(string noteId, string noteContent)
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

        public List<string> GetNoteIds()
        {
            List<string> ids = new List<string>();
            foreach (Note n in notes)
                ids.Add(n.noteId);

            return ids;
        }

        public void JsonSerialize(string filePath)
        {
            JsonSerializer2<List<Note>>.Serialize(notes, filePath);
        }

        public void JsonDeserialize(string filePath)
        {
            notes = JsonSerializer2<List<Note>>.Deserialize(filePath);
        }

        public void GoogleDriveUpload(string fileName, string credentialsFilePath)
        {
            GoogleDrive<List<Note>>.Authorize(credentialsFilePath);
            GoogleDrive<List<Note>>.UploadFile(notes, fileName);
        }

        public void GoogleDriveDownload(string fileName, string credentialsFilePath)
        {
            GoogleDrive<List<Note>>.Authorize(credentialsFilePath);
            notes = GoogleDrive<List<Note>>.DownloadFile(fileName);
        }
    }

    public class Note
    {
        public string noteId { get; set; }
        public string noteTitle { get; set; }
        public string noteContent { get; set; }

        public Note()
        { }

        public Note(string noteId)
        {
            this.noteId = noteId;
            this.noteTitle = "OrangeNote" + noteId;
            this.noteContent = "";
        }
    }
}
