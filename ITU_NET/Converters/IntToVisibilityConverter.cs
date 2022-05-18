using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ITU_NET.Converters
{
    public class IntToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var intValue = (int)value == 0;
            var boolParameter = parameter != null ? bool.Parse((string) parameter) : false;

            if (boolParameter) return intValue ? Visibility.Hidden : Visibility.Visible;
            return intValue ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}