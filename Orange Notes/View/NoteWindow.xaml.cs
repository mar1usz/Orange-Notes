using Orange_Notes.ViewModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;

namespace Orange_Notes.View
{
    /// <summary>
    /// Interaction logic for NoteWindow.xaml
    /// </summary>

    public partial class NoteWindow : Window
    {
        public NoteWindow()
        {
            InitializeComponent();
            this.DataContext = new NoteViewModel();
        }

        public NoteWindow(int noteId)
        {
            InitializeComponent();
            this.DataContext = new NoteViewModel(noteId);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new NoteWindow().Show();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            WindowCollection allWindows = Application.Current.Windows;
            IEnumerable<SettingsWindow> settingsWindows = allWindows.OfType<SettingsWindow>();

            if (settingsWindows.Count() < 1)
                new SettingsWindow().Show();
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).Application_Save();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
