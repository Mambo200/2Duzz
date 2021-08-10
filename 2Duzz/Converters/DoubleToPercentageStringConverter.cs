
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _2Duzz.Converters
{
    public class DoubleToPercentageStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double? input = value as double?;
            if(input != null)
            {
                input *= 100;
                return $"{(int)input}%";
            }
            return "error";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value as string;
            if(input != null)
            {
                if (input.Length > 0 && input[input.Length - 1] == '%')
                {
                    input = input.Remove(input.Length - 1, 1);
                    bool work = double.TryParse(input, out double result);
                    if (work)
                    {
                        return result;
                    }
                }
            }
            return -1;
        }
    }
}
