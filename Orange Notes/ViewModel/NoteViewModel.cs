using Orange_Notes.Model;
using System.Collections.ObjectModel;

namespace Orange_Notes.ViewModel
{
    public class NoteViewModel : ViewModelBase
    {
        public ObservableCollection<Note> notes { get; set; }
        public NoteViewModel()
        {
            notes = new ObservableCollection<Note>();
            notes.Add(new Note("Uh, that's nice!"));
            notes.Add(new Note("Now let me see you do the same thing twice"));
        }
    }
}
