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
        public static Storage Storage { get; set; } = Storage.Json;
        public static Notes Notes { get; } = new Notes();

        public static List<string> NoteIds { get => Notes.GetNoteIds(); }

        private static string notesFilepath = "Orange Notes.json";
        private static string credentialsFilepath = "credentials.json";
        private static string settingsFilepath = "Orange Notes Settings.json";


        public static void SaveNotes()
        {
            switch (Storage)
            {
                case Storage.Json:
                    Notes.JsonSerialize(notesFilepath);
                    break;
                case Storage.GoogleDrive:
                    Notes.GoogleDriveUpload(notesFilepath, credentialsFilepath);
                    break;
            }
        }

        public static void LoadNotes()
        {
            switch (Storage)
            {
                case Storage.Json:
                    Notes.JsonDeserialize(notesFilepath);
                    break;
                case Storage.GoogleDrive:
                    Notes.GoogleDriveDownload(notesFilepath, credentialsFilepath);
                    break;
            }
        }

        public static void SaveSettings()
        {
            JsonSerializer2<Storage>.Serialize(Storage, settingsFilepath);
        }

        public static void LoadSettings()
        {
            Storage = JsonSerializer2<Storage>.Deserialize(settingsFilepath);
        }


        public string noteId { get; }

        public string noteTitle
        {
            get
            {
                return Notes.GetNoteTitle(noteId);
            }
            set
            {
                Notes.SetNoteTitle(noteId, value);
                NotifyPropertyChanged("noteTitle");
            }
        }

        public string noteContent
        {
            get
            {
                return Notes.GetNoteContent(noteId);
            }
            set
            {
                Notes.SetNoteContent(noteId, value);
                NotifyPropertyChanged("noteContent");
            }
        }

        public ICommand removeNote { get; }

        public NoteViewModel(string noteId)
        {
            this.noteId = noteId;
            removeNote = new RelayCommand(parameter => Notes.RemoveNote(noteId));
        }

        public NoteViewModel()
        {
            noteId = Notes.AddNote();
            removeNote = new RelayCommand(parameter => Notes.RemoveNote(noteId));
        }
    }
}
