using Orange_Notes.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace Orange_Notes.ViewModel
{
    public class NoteViewModel : ViewModelBase
    {
        private static Notes notes = new Notes();
        public static List<int> noteIds => notes.GetNoteIds();
        //public static void SaveNotes() => notes.JsonSerialize("Orange Notes.json");
        public static void SaveNotes() => notes.GoogleDriveUpload("Orange Notes.json", "credentials.json");
        //public static void LoadNotes() => notes.JsonDeserialize("Orange Notes.json");
        public static void LoadNotes() => notes.GoogleDriveDownload("Orange Notes.json", "credentials.json");


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
