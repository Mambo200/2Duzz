using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace _2Duzz.Converters
{
    public class KeyGestureToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            KeyBinding input = value as KeyBinding;
            if (input != null)
            {
                string modifier = input.Modifiers.ToString().Replace(" ", "");
                modifier = modifier.Replace(',', '+');
                modifier = modifier.Replace("Control", "Ctrl");
                return $"{modifier}+{input.Key}"; 
            }
            return "error";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
