using Microsoft.Win32;
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
            StartupCheckBox_Set();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void StartupCheckBox_Set()
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
    }
}
