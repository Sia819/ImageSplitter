using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ImageSplitter.Converter
{
    public class EnumToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType().IsEnum == false)
            {
                return DependencyProperty.UnsetValue;
            }

            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is int))
            {
                return DependencyProperty.UnsetValue;
            }

            return Enum.ToObject(targetType, value);
        }
    }
}