using System.Windows;
using System.Windows.Input;

namespace FileExplorer.Views
{
    /// <summary>
    /// Interaction logic for MainPageView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            this.InitializeComponent();
        }

        private void OnDragMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MiniOnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
