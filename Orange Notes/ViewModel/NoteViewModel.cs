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
        public static Storage storage { get; set; } = Storage.Json;
        private static Notes notes = new Notes();

        public static List<string> noteIds
        {
            get
            {
                return notes.GetNoteIds();
            }
        }

        public static void SaveNotes()
        {
            switch (storage)
            {
                case Storage.Json:
                    notes.JsonSerialize("Orange Notes.json");
                    break;
                case Storage.GoogleDrive:
                    notes.GoogleDriveUpload("Orange Notes.json", "credentials.json");
                    break;
            }
        }

        public static void LoadNotes()
        {
            switch (storage)
            {
                case Storage.Json:
                    notes.JsonDeserialize("Orange Notes.json");
                    break;
                case Storage.GoogleDrive:
                    notes.GoogleDriveDownload("Orange Notes.json", "credentials.json");
                    break;
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


        public string noteId { get; }

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

        public NoteViewModel(string noteId)
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
