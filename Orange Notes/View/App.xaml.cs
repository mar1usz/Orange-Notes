using Orange_Notes.ViewModel;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Orange_Notes.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AutoResetEvent resetEvent2 = new AutoResetEvent(false);

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

            if (NoteViewModel.storage == Storage.GoogleDrive)
            {
                new ConnectingWindow().Show();
            }

            BackgroundWorker backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            NoteViewModel.LoadNotes();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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

            WindowCollection allWindows = Application.Current.Windows;
            ConnectingWindow[] connectingWindows = allWindows.OfType<ConnectingWindow>().ToArray();

            foreach (ConnectingWindow w in connectingWindows)
            {
                w.Close();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        { }

        public void Application_Save()
        {
            WindowCollection allWindows = Application.Current.Windows;
            foreach (Window w in allWindows)
            {
                w.Hide();
            }

            if (NoteViewModel.storage == Storage.GoogleDrive)
            {
                new ConnectingWindow().Show();
            }

            BackgroundWorker backgroundWorker2 = new BackgroundWorker();
            backgroundWorker2.DoWork += backgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += backgroundWorker2_RunWorkerCompleted;
            backgroundWorker2.RunWorkerAsync();

            NoteViewModel.SaveSettings();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            NoteViewModel.SaveNotes();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WindowCollection allWindows = Application.Current.Windows;
            ConnectingWindow[] connectingWindows = allWindows.OfType<ConnectingWindow>().ToArray();

            foreach (ConnectingWindow w in connectingWindows)
            {
                w.Close();
            }

            Application.Current.Shutdown();
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
