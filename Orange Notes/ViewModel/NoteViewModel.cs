using Orange_Notes.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public static Storage Storage
        {
            get
            {
                return _storage;
            }
            set
            {
                switch (value)
                {
                    case Storage.Json:
                        _storage = Storage.Json;
                        Notes.Serializer = new JsonSerializer2<List<Note>>();
                        break;
                    case Storage.GoogleDrive:
                        _storage = Storage.GoogleDrive;
                        Notes.Serializer = new GoogleDriveSerializer<List<Note>>();
                        break;
                }
            }
        }
        private static Storage _storage;
        private static Notes Notes = new Notes();
        private static readonly string NotesFilepath = "Orange Notes.json";
        private static readonly string SettingsFilepath = "Orange Notes Settings.json";

        public static List<string> GetNoteIds()
        {
            return Notes.GetNoteIds();
        }

        public static void SaveNotes()
        {
            Notes.Serialize(NotesFilepath);
        }

        public static void LoadNotes()
        {
            Notes.Deserialize(NotesFilepath);
        }

        public static async Task SaveNotesAsync()
        {
            await Notes.SerializeAsync(NotesFilepath);
        }

        public static async Task LoadNotesAsync()
        {
            await Notes.DeserializeAsync(NotesFilepath);
        }

        public static void SaveSettings()
        {
            JsonSerializer2<Storage> json = new JsonSerializer2<Storage>();
            json.Serialize(Storage, SettingsFilepath);
        }

        public static void LoadSettings()
        {
            JsonSerializer2<Storage> json = new JsonSerializer2<Storage>();
            Storage = json.Deserialize(SettingsFilepath);
        }


        public string NoteId { get; }
        public string NoteTitle
        {
            get
            {
                return Notes.GetNoteTitle(NoteId);
            }
            set
            {
                Notes.SetNoteTitle(NoteId, value);
                NotifyPropertyChanged("NoteTitle");
            }
        }
        public string NoteContent
        {
            get
            {
                return Notes.GetNoteContent(NoteId);
            }
            set
            {
                Notes.SetNoteContent(NoteId, value);
                NotifyPropertyChanged("NoteContent");
            }
        }
        public ICommand RemoveNote { get; }

        public NoteViewModel(string noteId)
        {
            if (!Notes.ContainsNote(noteId))
                throw new ArgumentException();

            NoteId = noteId;
            RemoveNote = new RelayCommand(parameter => Notes.RemoveNote(NoteId));
        }

        public NoteViewModel()
        {
            NoteId = Notes.AddNote();
            RemoveNote = new RelayCommand(parameter => Notes.RemoveNote(NoteId));
        }
    }
}
