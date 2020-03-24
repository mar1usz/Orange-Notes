using Orange_Notes.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Orange_Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new NoteViewModel();
        }

        public MainWindow(int noteId)
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
            new MainWindow().Show();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
