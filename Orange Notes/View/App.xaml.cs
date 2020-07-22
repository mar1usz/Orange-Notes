using Orange_Notes.Model;
using Orange_Notes.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly string restoreBounds_filePath = "Orange Notes Restore Bounds.json";

        private async void Application_StartupAsync(object sender, StartupEventArgs e)
        {
            Application_ExitIfNotUnique();
            Application_LoadSettings();
            await Application_LoadNotesAsync();
        }

        private void Application_ExitIfNotUnique()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            int processCount = Process.GetProcesses().Where(p => p.ProcessName == processName).Count();
            if (processCount > 1)
                Environment.Exit(1);
        }

        private void Application_LoadSettings()
        {
            NoteViewModel.LoadSettings();
            JsonSerializer2<Dictionary<string, Rect>> json = new JsonSerializer2<Dictionary<string, Rect>>();
            restoreBounds = json.Deserialize(restoreBounds_filePath);
        }

        private async Task Application_LoadNotesAsync()
        {
            if (NoteViewModel.Storage == Storage.GoogleDrive)
            {
                ConnectingWindow connectingWindow = new ConnectingWindow();
                connectingWindow.Show();
            }
            await NoteViewModel.LoadNotesAsync();
            if (!NoteViewModel.GetNoteIds().Any())
            {
                NoteWindow n = new NoteWindow();
                n.Show();
            }
            else
            {
                foreach (string noteId in NoteViewModel.GetNoteIds())
                {
                    NoteWindow n = new NoteWindow(noteId);
                    if (restoreBounds.ContainsKey(noteId))
                        n.setRestoreBounds(restoreBounds[noteId]);
                    n.Show();
                }
            }
            this.CloseWindowsOfType<ConnectingWindow>();
        }

        public async Task Application_ExitAsync()
        {
            await Application_CommonExitRoutineAsync();
            Shutdown();
        }

        private async Task Application_CommonExitRoutineAsync()
        {
            Application_HideAllWindows();
            Application_SaveSettings();
            await Application_SaveNotesAsync();
        }

        private void Application_HideAllWindows()
        {
            this.HideWindowsOfType<Window>();
        }

        private void Application_SaveSettings()
        {
            NoteViewModel.SaveSettings();
            IEnumerable<NoteWindow> noteWindows = this.GetWindowsOfType<NoteWindow>();
            restoreBounds.Clear();
            foreach (NoteWindow w in noteWindows)
                restoreBounds.Add(w.NoteId, w.RestoreBounds);
            JsonSerializer2<Dictionary<string, Rect>> json = new JsonSerializer2<Dictionary<string, Rect>>();
            json.Serialize(restoreBounds, restoreBounds_filePath);
        }

        private async Task Application_SaveNotesAsync()
        {
            if (NoteViewModel.Storage == Storage.GoogleDrive)
            {
                ConnectingWindow connectingWindow = new ConnectingWindow();
                connectingWindow.Show();
            }
            await NoteViewModel.SaveNotesAsync();
            this.CloseWindowsOfType<ConnectingWindow>();
        }

        private async void Application_SessionEndingAsync(object sender, SessionEndingCancelEventArgs e)
        {
            e.Cancel = true;
            await Application_CommonExitRoutineAsync();
            e.Cancel = false;
            Shutdown();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application_SaveExceptionDetailsToFile(DateTime.Now, e.Exception);
            Environment.Exit(1);
        }

        private void Application_SaveExceptionDetailsToFile(DateTime time, Exception exception)
        {
            string fileName = time.ToString("yyyy-MM-ddTHHmmss") + "-Exception.txt";
            string content = exception.Message + Environment.NewLine + exception.StackTrace;
            File.WriteAllText(fileName, content);
        }
    }
}
