using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ITU_NET.Converters
{
    public class StringToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var stringValue = ((string)value).Length == 0;
            var boolParameter = parameter != null ? bool.Parse((string) parameter) : false;

            if (boolParameter) return stringValue ? Visibility.Hidden : Visibility.Visible;
            return stringValue ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}