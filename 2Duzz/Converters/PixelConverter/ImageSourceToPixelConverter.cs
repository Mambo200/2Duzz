using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace _2Duzz.Converters
{
    public class ImageSourceToPixelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // We use "System.Drawing.Point" instead of "System.Windows.Point" because its properties are ints which we want
            Point tr;

            BitmapSource input = value as BitmapSource;
            if (input != null)
            {
                tr = new Point(input.PixelWidth, input.PixelHeight);
                return tr;
            }
            return "No Image Data";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
