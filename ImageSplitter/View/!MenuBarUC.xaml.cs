using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

namespace ImageSplitter.View
{
    /// <summary>
    /// MenuBarUC.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuBarUC : UserControl
    {
        public MenuBarUC()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), MethodBase.GetCurrentMethod()!.DeclaringType);
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty SelectedPageIndexProperty = DependencyProperty.Register("SelectedPageIndex", typeof(int), MethodBase.GetCurrentMethod()!.DeclaringType);
        public int SelectedPageIndex
        {
            get => (int)GetValue(SelectedPageIndexProperty);
            set => SetValue(SelectedPageIndexProperty, value);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = !IsOpen;
        }

        private void GridButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedPageIndex = 0;
        }

        private void CropButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedPageIndex = 1;
        }
    }
}
