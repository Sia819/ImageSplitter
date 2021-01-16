using System.Windows;
using System.Windows.Controls;

namespace ImageSpliter_WPF
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && HorizontalSize_TextBox != null && VerticalSize_TextBox != null)
            {
                if (checkBox.IsChecked == true)
                {
                    HorizontalSize_TextBox.IsReadOnly =
                    VerticalSize_TextBox.IsReadOnly = true;
                }
                else if (checkBox.IsChecked == false)
                {
                    HorizontalSize_TextBox.IsReadOnly =
                    VerticalSize_TextBox.IsReadOnly = false;
                }
            }
        }
    }
}
