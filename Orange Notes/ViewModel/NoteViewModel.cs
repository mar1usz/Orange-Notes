using Orange_Notes.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace Orange_Notes.ViewModel
{
    public class NoteViewModel : ViewModelBase
    {
        private static Notes allNotes = new Notes(true, "json.json");
        public static List<int> allNotesIds
        {
            get
            {
                return allNotes.GetNoteIds();
            }
        }

        private int noteId;
        public string noteTitle
        {
            get
            {
                return allNotes.GetNoteTitle(noteId);
            }
            set
            {
                allNotes.SetNoteTitle(noteId, value);
                NotifyPropertyChanged("noteTitle");
            }
        }
        public string noteContent
        {
            get
            {
                return allNotes.GetNoteContent(noteId);
            }
            set
            {
                allNotes.SetNoteContent(noteId, value);
                NotifyPropertyChanged("noteContent");
            }
        }
        public ICommand removeNote { get; set; }
        public ICommand serialize { get; set; }
        public ICommand deserialzie { get; set; }

        public NoteViewModel()
        {
            noteId = allNotes.AddNote();

            this.removeNote = new RelayCommand(parameter => allNotes.RemoveNote(noteId));
            this.serialize = new RelayCommand(parameter => allNotes.Serialize("json.json"));
            this.deserialzie = new RelayCommand(paramater => allNotes.Deserialize("json.json"));   
        }

        public NoteViewModel(int noteId)
        {
            this.noteId = noteId;

            this.removeNote = new RelayCommand(parameter => allNotes.RemoveNote(noteId));
            this.serialize = new RelayCommand(parameter => allNotes.Serialize("json.json"));
            this.deserialzie = new RelayCommand(paramater => allNotes.Deserialize("json.json"));
        }
    }
}
