using Orange_Notes.ViewModel;
using System;
using System.Diagnostics;
using System.Linq;
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
            bool applicationUnique = Application_Unique();
            if (!applicationUnique)
            {
                Environment.Exit(0);
            }


            NoteViewModel.ReadNotes();
            if (NoteViewModel.noteIds.Count == 0)
            {
                new NoteWindow().Show();
            }
            else
            {
                foreach (int noteId in NoteViewModel.noteIds)
                {
                    new NoteWindow(noteId).Show();
                }
            }
        }

        private bool Application_Unique()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            int processCount = Process.GetProcesses().Where(p => p.ProcessName == processName).Count();
            
            return processCount > 1 ? false : true;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            NoteViewModel.SaveNotes();
        }
    }
}
