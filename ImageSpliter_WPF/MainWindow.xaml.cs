using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if (sender is CheckBox && HorizontalSize_TextBox != null && VerticalSize_TextBox != null)
            {
                if ((sender as CheckBox).IsChecked == true)
                {
                    HorizontalSize_TextBox.IsReadOnly =
                    VerticalSize_TextBox.IsReadOnly = true;
                }
                else if ((sender as CheckBox).IsChecked == false)
                {
                    HorizontalSize_TextBox.IsReadOnly =
                    VerticalSize_TextBox.IsReadOnly = false;
                }
            }
        }
    }
}
