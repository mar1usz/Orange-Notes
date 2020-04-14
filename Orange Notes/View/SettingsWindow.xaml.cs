using Microsoft.Win32;
using Orange_Notes.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Orange_Notes.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>

    public partial class SettingsWindow : Window
    {
        private const string startupReg = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

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
            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(startupReg, true))
            {
                if (startupCheckbox.IsChecked == true)
                {
                    regKey.SetValue("Orange Notes", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                }
                else
                {
                    regKey.DeleteValue("Orange Notes", true);
                }
            }
        }

        private void StartupCheckBox_Refresh()
        {
            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(startupReg, true))
            {
                if (regKey.GetValue("Orange Notes") == null)
                {
                    startupCheckbox.IsChecked = false;
                }
                else
                {
                    startupCheckbox.IsChecked = true;
                }
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
            if(e.Key == Key.Escape)
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
