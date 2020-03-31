using Orange_Notes.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace Orange_Notes.ViewModel
{
    public class NoteViewModel : ViewModelBase
    {
        private static Notes notes;
        public static string filePath { get; set; } = "Orange Notes.json";

        static NoteViewModel()
        {
            notes = new Notes(true, filePath);
        }

        public static List<int> noteIds => notes.GetNoteIds();
        public static void SerializeNotes() => notes.Serialize(filePath);
        public static void DeserializeNotes() => notes.Deserialize(filePath);
        public static void UploadNotesToGoogleDrive() => GoogleDriveService.UploadOrUpdateFile(filePath);


        private int noteId;
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
