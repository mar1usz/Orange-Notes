using Orange_Notes.Model;
using Orange_Notes.ViewModel.Helpers;
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
            get => _storage;
            set
            {
                switch (value)
                {
                    case Storage.Json:
                        _storage = Storage.Json;
                        notes.Serializer = new JsonSerializer2<List<Note>>();
                        break;
                    case Storage.GoogleDrive:
                        _storage = Storage.GoogleDrive;
                        notes.Serializer = new GoogleDriveSerializer<List<Note>>();
                        break;
                }
            }
        }
        private static Storage _storage;
        private static Notes notes = new Notes();
        private static readonly string notes_filePath = "notes.json";
        private static readonly string settings_filePath = "settings.json";

        public static List<string> GetNoteIds()
        {
            return notes.GetNoteIds();
        }

        public static async Task SaveNotesAsync()
        {
            await notes.SerializeAsync(notes_filePath);
        }

        public static async Task LoadNotesAsync()
        {
            await notes.DeserializeAsync(notes_filePath);
        }

        public static void SaveSettings()
        {
            JsonSerializer2<Storage> json = new JsonSerializer2<Storage>();
            json.Serialize(Storage, settings_filePath);
        }

        public static void LoadSettings()
        {
            JsonSerializer2<Storage> json = new JsonSerializer2<Storage>();
            Storage = json.Deserialize(settings_filePath);
        }


        public string NoteId { get; }
        public string NoteTitle
        {
            get
            {
                return notes.GetNoteTitle(NoteId);
            }
            set
            {
                notes.SetNoteTitle(NoteId, value);
                NotifyPropertyChanged("NoteTitle");
            }
        }
        public string NoteContent
        {
            get
            {
                return notes.GetNoteContent(NoteId);
            }
            set
            {
                notes.SetNoteContent(NoteId, value);
                NotifyPropertyChanged("NoteContent");
            }
        }
        public ICommand RemoveNote { get; }

        public NoteViewModel(string noteId)
        {
            if (!notes.ContainsNote(noteId))
                throw new ArgumentException();

            NoteId = noteId;
            RemoveNote = new RelayCommand(parameter => notes.RemoveNote(NoteId));
        }

        public NoteViewModel()
        {
            NoteId = notes.AddNote();
            RemoveNote = new RelayCommand(parameter => notes.RemoveNote(NoteId));
        }
    }
}
