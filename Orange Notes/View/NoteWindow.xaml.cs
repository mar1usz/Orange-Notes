using Orange_Notes.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Orange_Notes
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
