using Orange_Notes.ViewModel;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Orange_Notes.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application_CheckIfUnique();
            Application_Load();
        }

        private void Application_CheckIfUnique()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            int processCount = Process.GetProcesses().Where(p => p.ProcessName == processName).Count();

            if (processCount > 1)
            {
                Environment.Exit(0);
            }
        }

        private void Application_Load()
        {
            NoteViewModel.LoadSettings();
            NoteViewModel.LoadNotes();

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

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Application_Save();
        }

        private void Application_Save()
        {
            NoteViewModel.SaveNotes();
            NoteViewModel.SaveSettings();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application_SaveExceptionDetailsToFile(DateTime.Now, e.Exception.Message, e.Exception.StackTrace);

            Environment.Exit(1);
        }

        private void Application_SaveExceptionDetailsToFile(DateTime time, string exception_message, string exception_stackTrace)
        {
            string fileName = time.ToString("yyyy-MM-ddTHHmmss") + "@Exception.txt";
            string content = exception_message + Environment.NewLine + exception_stackTrace;

            File.WriteAllText(fileName, content);
        }
    }
}
