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
            if (NoteViewModel.noteIds.Count == 0)
            {
                new MainWindow().Show();
            }
            else
            {
                foreach (int noteId in NoteViewModel.noteIds)
                    new MainWindow(noteId).Show();
            }
        }

        public void Application_Exit(object sender, ExitEventArgs e)
        {
            NoteViewModel.Serialize();
        }
    }
}
