using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageSpliter.View
{
    /// <summary>
    /// MainWindow2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox combo && combo.SelectedItem != null && ContentFrame is not null && MenuFrame is not null)
            {
                switch (combo.SelectedIndex)
                {
                    case 0:
                        ContentFrame.Source = new Uri("pack://application:,,,/View/GridPage.xaml");
                        MenuFrame.Source = new Uri("pack://application:,,,/View/CropMenuPage.xaml");
                        break;
                    case 1:
                        ContentFrame.Source = new Uri("pack://application:,,,/View/RemoveWhitePage.xaml");
                        MenuFrame.Source = new Uri("pack://application:,,,/View/RemoveWhiteMenuPage.xaml");
                        break;
                    case 2:
                        ContentFrame.Source = new Uri("pack://application:,,,/View/CropPage.xaml");
                        MenuFrame.Source = new Uri("pack://application:,,,/View/CropMenuPage.xaml");
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
