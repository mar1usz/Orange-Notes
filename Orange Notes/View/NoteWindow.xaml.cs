using Orange_Notes.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Orange_Notes.View
{
    /// <summary>
    /// Interaction logic for NoteWindow.xaml
    /// </summary>

    public partial class NoteWindow : Window
    {
        public string noteId {
            get
            {
                NoteViewModel vm = DataContext as NoteViewModel;
                return vm.noteId;
            }
        }

        public NoteWindow(string noteId)
        {
            InitializeComponent();
            DataContext = new NoteViewModel(noteId);
        }

        public NoteWindow()
        {
            InitializeComponent();
            DataContext = new NoteViewModel();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int noteWindowsCount = Application.Current.CountWindowsOfType<NoteWindow>();
            if (noteWindowsCount == 1)
            {
                CloseButton_Click(sender, e);
            }
            else
            {
                Close();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new NoteWindow().Show();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            bool settingsWindowExists = Application.Current.WindowOfTypeExists<SettingsWindow>();
            if (!settingsWindowExists)
                new SettingsWindow().Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).Application_Exit();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
