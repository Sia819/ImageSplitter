using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ImageSplitter.Converter
{
    public class SelectedImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2)
            {
                return DependencyProperty.UnsetValue;
            }

            if (values[0] is ObservableCollection<BitmapSource> displayImages && values[1] is int selectedImage)
            {
                if (selectedImage >= 0 && selectedImage < displayImages.Count)
                {
                    return displayImages[selectedImage];
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
