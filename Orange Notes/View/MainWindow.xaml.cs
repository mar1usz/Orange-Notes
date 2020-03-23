using Orange_Notes.ViewModel;
using System.IO;
using System.Windows;

namespace Orange_Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(int noteId)
        {
            InitializeComponent();
            this.DataContext = new NoteViewModel(noteId);
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new NoteViewModel();
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
