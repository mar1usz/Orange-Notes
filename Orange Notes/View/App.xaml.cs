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
            Process proc = Process.GetCurrentProcess();
            if (Process.GetProcesses().Where(p => p.ProcessName == proc.ProcessName).Count() > 1)
                Environment.Exit(0);

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

        public void Application_Exit(object sender, ExitEventArgs e)
        {
            NoteViewModel.SerializeNotes();
            NoteViewModel.UploadNotesToGoogleDrive();
        }
    }
}
