using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace _2Duzz.Helper
{
    public static class MyConverter
    {
        public static string ToBase64(BitmapSource _img)
        {
            byte[] data;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(_img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            // Convert byte[] to Base64 String
            return Convert.ToBase64String(data);

        }

        //public static string ToBase64(BitmapFrame _img)
        //{
        //    byte[] data;
        //    PngBitmapEncoder encoder = new PngBitmapEncoder();
        //    encoder.Frames.Add(BitmapFrame.Create(_img));
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        encoder.Save(ms);
        //        data = ms.ToArray();
        //    }
        //
        //    // Convert byte[] to Base64 String
        //    return Convert.ToBase64String(data);
        //
        //}
    }
}
