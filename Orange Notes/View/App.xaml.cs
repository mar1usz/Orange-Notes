using Orange_Notes.ViewModel;
using System.Windows;

namespace Orange_Notes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (NoteViewModel.allNotesIds.Count == 0)
            {
                new MainWindow().Show();
            }
            else
            {
                foreach (int noteId in NoteViewModel.allNotesIds)
                    new MainWindow(noteId).Show();
            }
        }
    }
}
