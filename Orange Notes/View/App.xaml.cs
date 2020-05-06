using Orange_Notes.Model;
using Orange_Notes.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private Dictionary<string, Rect> restoreBounds;

        #region Application_Startup
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application_CheckIfUnique();
            Application_LoadSettings();
            Application_LoadNotes();
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

        private void Application_LoadSettings()
        {
            NoteViewModel.LoadSettings();
            restoreBounds = JsonSerializer2<Dictionary<string, Rect>>.Deserialize("Orange Notes Restore Bounds.json");
        }

        private void Application_LoadNotes()
        {
            BackgroundWorker backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1_BeforeWork();
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_BeforeWork()
        {
            if (NoteViewModel.storage == Storage.GoogleDrive)
            {
                new ConnectingWindow().Show();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            NoteViewModel.LoadNotes();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw e.Error;

            if (NoteViewModel.noteIds.Count == 0)
            {
                NoteWindow n = new NoteWindow();
                n.Show();
            }
            else
            {
                foreach (string noteId in NoteViewModel.noteIds)
                {
                    NoteWindow n = new NoteWindow(noteId);
                    if (restoreBounds.ContainsKey(noteId))
                    {
                        n.setRestoreBounds(restoreBounds[noteId]);
                    }
                    n.Show();
                }
            }

            this.CloseWindowsOfType<ConnectingWindow>();
        }
        #endregion

        #region Application_Exit
        public void Application_Exit()
        {
            this.HideWindowsOfType<Window>();
            Application_SaveSettings();
            Application_SaveNotes();
        }

        private void Application_SaveSettings()
        {
            NoteViewModel.SaveSettings();

            NoteWindow[] noteWindows = this.GetWindowsOfType<NoteWindow>();
            restoreBounds.Clear();
            foreach (NoteWindow w in noteWindows)
            {
                restoreBounds.Add(w.noteId, w.RestoreBounds);
            }

            JsonSerializer2<Dictionary<string, Rect>>.Serialize(restoreBounds, "Orange Notes Restore Bounds.json");
        }

        private void Application_SaveNotes()
        {
                BackgroundWorker backgroundWorker2 = new BackgroundWorker();
                backgroundWorker2.DoWork += backgroundWorker2_DoWork;
                backgroundWorker2.RunWorkerCompleted += backgroundWorker2_RunWorkerCompleted;
                backgroundWorker2_BeforeWork();
                backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker2_BeforeWork()
        {
            if (NoteViewModel.storage == Storage.GoogleDrive)
            {
                new ConnectingWindow().Show();
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            NoteViewModel.SaveNotes();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw e.Error;

            this.CloseWindowsOfType<ConnectingWindow>();
            Shutdown();
        }
        #endregion

        #region Application_SessionEnding
        private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            e.Cancel = true;
            Application_SaveSettings();
            backgroundWorker2_DoWork(sender, null);
            e.Cancel = false;
        }
        #endregion

        #region Application_Helpers
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
        #endregion
    }
}
