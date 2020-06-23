using System.Windows;
using System.Windows.Input;

namespace Orange_Notes.View
{
    /// <summary>
    /// Interaction logic for ConnectingWindow.xaml
    /// </summary>

    public partial class ConnectingWindow : Window
    {
        public ConnectingWindow()
        {
            InitializeComponent();
            this.Left = SystemParameters.WorkArea.Right - 10 - this.Width;
            this.Top = SystemParameters.WorkArea.Top + 10;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
