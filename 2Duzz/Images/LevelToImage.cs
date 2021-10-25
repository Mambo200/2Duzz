using _2Duzz.Helper;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _2Duzz.Images
{
    public static class LevelToImage
    {
        /// <summary>
        /// Export current Level as image
        /// </summary>
        /// <param name="_width">Width of image</param>
        /// <param name="_height">Height of image</param>
        /// <param name="_absolutePath">Absolute save path of image</param>
        /// <param name="_imageFormat">Format of image in which it will be saved</param>
        public static void ConvertLevelToImage(int _width, int _height, string _absolutePath, ImageFormat _imageFormat)
        {
            int[] visibleIndexes = GetAllVisibleIndex();
            int count = visibleIndexes.Length;
            PngBitmapEncoder[] encoders = new PngBitmapEncoder[count];
            MemoryStream[] memStreams = new MemoryStream[count];
            System.Drawing.Bitmap[] bitmaps = new System.Drawing.Bitmap[count];

            int tempCount = 0;
            foreach (int i in visibleIndexes)
            {
                encoders[tempCount] = ConvertLevelToImage(i);

                memStreams[tempCount] = new MemoryStream();
                encoders[tempCount].Save(memStreams[tempCount]);

                bitmaps[tempCount] = new System.Drawing.Bitmap(memStreams[tempCount]);
                tempCount++;
            }

            using (FileStream filestream = File.Create(_absolutePath))
            {
                using (MemoryStream final = CombineImages((int)(_width), (int)(_height), _imageFormat, bitmaps))
                {
                    final.WriteTo(filestream);
                }
            }

            foreach (System.Drawing.Bitmap bitmap in bitmaps)
            {
                bitmap.Dispose();
            }

        }

        /// <summary>
        /// Get all layer which are Visible
        /// </summary>
        /// <returns>All visible Layer</returns>
        private static int[] GetAllVisibleIndex()
        {
            List<int> tr = new List<int>();
            for (int i = 0; i < ImageDrawingHelper.Get.ImageLayer.Count; i++)
            {
                if (ImageDrawingHelper.Get.IsLayerEnabled(i))
                    tr.Add(i);
            }

            return tr.ToArray();
        }

        /// <summary>
        /// Convert Layer to image
        /// </summary>
        /// <param name="_layer">index of layer</param>
        /// <returns></returns>
        private static PngBitmapEncoder ConvertLevelToImage(int _layer)
        {
            var encoder = new PngBitmapEncoder();
            Drawing drawing = ImageDrawingHelper.Get.GetDrawingImage(_layer).Drawing;
            var drawingVisual = new DrawingVisual();

            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.PushTransform(new ScaleTransform(1, 1));
                drawingContext.PushTransform(new TranslateTransform(-drawing.Bounds.X, -drawing.Bounds.Y));
                drawingContext.DrawDrawing(drawing);
            }

            var width = drawing.Bounds.Width;
            var height = drawing.Bounds.Height;
            var bitmap = new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(drawingVisual);

            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            return encoder;
        }

        /// <summary>
        /// Creates an Image from the level
        /// </summary>
        /// <param name="_width">width of image</param>
        /// <param name="_height">height of image</param>
        /// <param name="_format">Format of image</param>
        /// <param name="_sources">all images to add</param>
        /// <returns><see cref="MemoryStream"/> of merged layer</returns>
        private static MemoryStream CombineImages(int _width, int _height, System.Drawing.Imaging.ImageFormat _format, params System.Drawing.Bitmap[] _sources)
        {
            if (_sources == null)
                return null;

            var target = new System.Drawing.Bitmap(_width, _height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var graphics = System.Drawing.Graphics.FromImage(target);
            graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            foreach (System.Drawing.Bitmap img in _sources)
            {
                if (img == null) continue;
                graphics.DrawImage(img, 0, 0);
            }

            MemoryStream tr = new MemoryStream();
            target.Save(tr, _format);
            target.Dispose();
            graphics.Dispose();
            return tr;
        }

    }
}
