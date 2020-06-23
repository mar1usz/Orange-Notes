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
                if (vm != null)
                {
                    return vm.noteId;
                }
                else
                {
                    return null;
                }
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
            if (Application.Current.CountWindowsOfType<NoteWindow>() == 1)
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
            if (!Application.Current.WindowOfTypeExists<SettingsWindow>())
                new SettingsWindow().Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App app = Application.Current as App;
            if(app != null)
            {
                app.Application_Exit();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
