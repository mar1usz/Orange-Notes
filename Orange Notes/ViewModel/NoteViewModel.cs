using Orange_Notes.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace Orange_Notes.ViewModel
{
    public enum Storage
    {
        Json,
        GoogleDrive
    }

    public class NoteViewModel : ViewModelBase
    {
        public static Notes notes { get; set; } = new Notes();
        public static Storage storage { get; set; } = Storage.Json;

        public static List<int> noteIds
        {
            get
            {
                return notes.GetNoteIds();
            }
        }

    public static void SaveNotes()
        {
            if (storage == Storage.Json)
            {
                notes.JsonSerialize("Orange Notes.json");
            }
            else if (storage == Storage.GoogleDrive)
            {
                notes.GoogleDriveUpload("Orange Notes.json", "credentials.json");
            }
        }
            
        public static void LoadNotes()
        {
            if (storage == Storage.Json)
            {
                notes.JsonDeserialize("Orange Notes.json");
            }
            else if (storage == Storage.GoogleDrive)
            {
                notes.GoogleDriveDownload("Orange Notes.json", "credentials.json");
            }
        }

        public static void SaveSettings()
        {
            JsonSerializer2<Storage>.Serialize(storage, "Orange Notes Settings.json");
        }

        public static void LoadSettings()
        {
            storage = JsonSerializer2<Storage>.Deserialize("Orange Notes Settings.json");
        }


        public int noteId { get; }

        public string noteTitle
        {
            get
            {
                return notes.GetNoteTitle(noteId);
            }
            set
            {
                notes.SetNoteTitle(noteId, value);
                NotifyPropertyChanged("noteTitle");
            }
        }

        public string noteContent
        {
            get
            {
                return notes.GetNoteContent(noteId);
            }
            set
            {
                notes.SetNoteContent(noteId, value);
                NotifyPropertyChanged("noteContent");
            }
        }

        public ICommand removeNote { get; }

        public NoteViewModel(int noteId)
        {
            this.noteId = noteId;
            this.removeNote = new RelayCommand(parameter => notes.RemoveNote(noteId));
        }

        public NoteViewModel()
        {
            this.noteId = notes.AddNote();
            this.removeNote = new RelayCommand(parameter => notes.RemoveNote(noteId));
        }
    }
}
