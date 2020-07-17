using Orange_Notes.ViewModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Orange_Notes.View
{
    /// <summary>
    /// Interaction logic for NoteWindow.xaml
    /// </summary>
    public partial class NoteWindow : Window
    {
        public string NoteId
        {
            get
            {
                if (DataContext is NoteViewModel vm)
                    return vm.NoteId;
                else
                    return null;
            }
        }

        public NoteWindow()
        {
            InitializeComponent();
            DataContext = new NoteViewModel();
            KeyDown += NoteWindow_KeyDown;
        }

        public NoteWindow(string noteId)
        {
            InitializeComponent();
            DataContext = new NoteViewModel(noteId);
            KeyDown += NoteWindow_KeyDown;
        }

        private async void RemoveButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (Application.Current.CountWindowsOfType<NoteWindow>() == 1)
                await Application_ExitAsync();
            else
                Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NoteWindow note = new NoteWindow();
            note.Show();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.WindowOfTypeExists<SettingsWindow>())
            {
                SettingsWindow settings = new SettingsWindow();
                settings.Show();
            }
        }

        private async void CloseButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            await Application_ExitAsync();
        }

        private async Task Application_ExitAsync()
        {
            if (Application.Current is App app)
                await app.Application_ExitAsync();
        }

        private async void NoteWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt && e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
                await Application_ExitAsync();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
