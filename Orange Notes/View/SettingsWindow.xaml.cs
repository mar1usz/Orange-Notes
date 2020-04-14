using IWshRuntimeLibrary;
using Orange_Notes.ViewModel;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using File = System.IO.File;

namespace Orange_Notes.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>

    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            StartupCheckBox_Refresh();
            StorageCheckboxes_Refresh();
            this.KeyDown += Window_KeyDown;
            this.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width / 2 - this.Width / 2;
            this.Top = SystemParameters.WorkArea.Top + 10;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void StartupCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (startupCheckbox.IsChecked == true)
            {
                WshShell shell = new WshShell();
                string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Orange Notes.lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(filepath);
                shortcut.WorkingDirectory = Directory.GetCurrentDirectory();
                shortcut.TargetPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                shortcut.Save();
            }
            else
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Orange Notes.lnk";
                File.Delete(filePath);
            }
        }

        private void StartupCheckBox_Refresh()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Orange Notes.lnk"))
            {
                startupCheckbox.IsChecked = true;
            }
            else
            {
                startupCheckbox.IsChecked = false;
            }
        }

        private void JsonCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (jsonCheckbox.IsChecked == true)
            {
                NoteViewModel.storage = Storage.Json;
            }

            StorageCheckboxes_Refresh();
        }

        private void GoogleDriveCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (jsonCheckbox.IsChecked == true)
            {
                NoteViewModel.storage = Storage.GoogleDrive;
            }

            StorageCheckboxes_Refresh();
        }

        private void StorageCheckboxes_Refresh()
        {
            jsonCheckbox.IsChecked = false;
            googleDriveCheckbox.IsChecked = false;

            if (NoteViewModel.storage == Storage.Json)
            {
                jsonCheckbox.IsChecked = true;
            }
            else if (NoteViewModel.storage == Storage.GoogleDrive)
            {
                googleDriveCheckbox.IsChecked = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
