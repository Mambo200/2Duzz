////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                                          Code von Robertico                                                    //    
//  https://mycsharp.de/forum/threads/29667/getpixel-und-setpixel-um-laengen-geschlagen-800-mal-schneller?page=1  //
//                                    Abgerufen am 26.08.2021 um 16:55 Uhr                                        //
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace _2Duzz.Images
{
    /// <summary>
    /// A fast way of splitting images
    /// </summary>
    public class EzSplitBitmap : IDisposable
    {
        private byte[] m_ImageData;
        /// <summary>Color data for every pixel</summary>
        private Color[,] m_color;
        /// <summary>Width of image in Pixel</summary>
        public int Width { get; private set; }
        /// <summary>Height of image in Pixel</summary>
        public int Height { get; private set; }
        private Bitmap m_Image;
        /// <summary>Image</summary>
        public Bitmap Image
        {
            set
            {
                m_Image = value;
                Init();
            }
            get
            {
                if (!m_modified) return m_Image;
                switch (m_PixelFormat)
                {
                    case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                        return ReturnFormat32BppArgb();
                    case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                        return ReturnFormat24BppRgb();
                }
                return null;
            }
        }
        /// <summary>Is class disposed?</summary>
        public bool Disposed { get; private set; }

        private Rectangle m_Rect;
        private bool m_modified;
        private int bytes;
        private int stride;
        private System.Drawing.Imaging.PixelFormat m_PixelFormat;
        private System.Drawing.Imaging.ColorPalette m_ColorPalette;
     
        #region Constructor
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_image">image</param>
        public EzSplitBitmap(Bitmap _image)
        {
            m_Image = _image;
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_width">The width in pixels</param>
        /// <param name="_height">The height in pixels</param>
        public EzSplitBitmap(int _width, int _height)
        {
            m_Image = new Bitmap(_width, _height);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_filePath">The absolute path of the image</param>
        public EzSplitBitmap(string _filePath)
        {
            m_Image = new Bitmap(_filePath);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_stream">The data stream used to load the image.</param>
        public EzSplitBitmap(Stream _stream)
        {
            m_Image = new Bitmap(_stream);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_original">The <see cref="System.Drawing.Image"/> from which to create the new <see cref="EzSplitBitmap"/> which uses <see cref="System.Drawing.Bitmap"/></param>
        public EzSplitBitmap(System.Drawing.Image _original)
        {
            m_Image = new Bitmap(_original);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_filePath">The absolute path of the image</param>
        /// <param name="_useIcm">true to use color correction for this Image; otherwise, false.</param>
        public EzSplitBitmap(string _filePath, bool _useIcm)
        {
            m_Image = new Bitmap(_filePath, _useIcm);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_type">The class used to extract the resource.</param>
        /// <param name="_resource">The name of the resource.</param>
        public EzSplitBitmap(Type _type, string _resource)
        {
            m_Image = new Bitmap(_type, _resource);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_stream">The data stream used to load the image.</param>
        /// <param name="_useIcm">true to use color correction for this Image; otherwise, false.</param>
        public EzSplitBitmap(Stream _stream, bool _useIcm)
        {
            m_Image = new Bitmap(_stream, _useIcm);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_original">The <see cref="System.Drawing.Image"/> from which to create the new <see cref="EzSplitBitmap"/> which uses <see cref="System.Drawing.Bitmap"/></param>
        /// <param name="_newSize">The <see cref="System.Drawing.Size"/> structure that represent the size of the Image.</param>
        public EzSplitBitmap(System.Drawing.Image _original, Size _newSize)
        {
            m_Image = new Bitmap(_original, _newSize);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_width">The width in pixels</param>
        /// <param name="_height">The height in pixels</param>
        /// <param name="_format">The pixel format for the Image. This must specify a value that begins with Format.</param>
        public EzSplitBitmap(int _width, int _height, System.Drawing.Imaging.PixelFormat _format)
        {
            m_Image = new Bitmap(_width, _height, _format);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_width">The width in pixels</param>
        /// <param name="_height">The height in pixels</param>
        /// <param name="_graphics">The <see cref="System.Drawing.Graphics"/> object that specifies the resolution for the new Image</param>
        public EzSplitBitmap(int _width, int _height, Graphics _graphics)
        {
            m_Image = new Bitmap(_width, _height, _graphics);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_original">The <see cref="System.Drawing.Image"/> from which to create the new <see cref="EzSplitBitmap"/> which uses <see cref="System.Drawing.Bitmap"/></param>
        /// <param name="_width">The width in pixels</param>
        /// <param name="_height">The height in pixels</param>
        public EzSplitBitmap(System.Drawing.Image _original, int _width, int _height)
        {
            m_Image = new Bitmap(_original, _width, _height);
            Init();
        }
        /// <summary>
        /// Initialize a new instance of <see cref="EzSplitBitmap"/>.
        /// </summary>
        /// <param name="_width">The width in pixels</param>
        /// <param name="_height">The height in pixels</param>
        /// <param name="_stride">Integer that specifies the byte offset between the beginning of one scan line and the next. This is usually (but not necessarily) the number of bytes in the pixel format (for example, 2 for 16 bits per pixel) multiplied by the width of the bitmap. The value passed to this parameter must be a multiple of four.</param>
        /// <param name="_format">The pixel format for the new Image. This must specify a value that begins with Format.</param>
        /// <param name="_scan0">Pointer to an array of bytes that contains the pixel data.</param>
        public EzSplitBitmap(int _width, int _height, int _stride, System.Drawing.Imaging.PixelFormat _format, IntPtr _scan0)
        {
            m_Image = new Bitmap(_width, _height, _stride, _format, _scan0);
            Init();
        }
        #endregion

        /// <summary>
        /// Initialize the <see cref="m_Image"/>
        /// </summary>
        private void Init()
        {
            // set local variables
            m_ColorPalette = m_Image.Palette;
            m_PixelFormat = m_Image.PixelFormat;
            Width = m_Image.Width;
            Height = m_Image.Height;
            m_Rect = new Rectangle(0, 0, Width, Height);
            System.Drawing.Imaging.BitmapData bmpData =
                m_Image.LockBits(m_Rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                m_Image.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            stride = bmpData.Stride;
            bytes = stride * Height;
            m_ImageData = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, m_ImageData, 0, bytes);
            m_Image.UnlockBits(bmpData);
            m_color = new Color[Width, Height];
            switch (m_PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    Format32BppArgb();
                    break;
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    Format24BppRgb();
                    break;
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    Format8BppIndexed();
                    break;
                case System.Drawing.Imaging.PixelFormat.Format4bppIndexed:
                    Format4BppIndexed();
                    break;
                case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
                    Format1BppIndexed();
                    break;
            }
            m_modified = false;
        }
        void Format32BppArgb()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    m_color[x, y] = Color.FromArgb(m_ImageData[y * stride + x * 4 + 3], m_ImageData[y * stride + x * 4 + 2], m_ImageData[y * stride + x * 4 + 1], m_ImageData[y * stride + x * 4]);
        }
        void Format24BppRgb()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    m_color[x, y] = Color.FromArgb(m_ImageData[y * stride + x * 3 + 2], m_ImageData[y * stride + x * 3 + 1], m_ImageData[y * stride + x * 3]);
        }
        void Format8BppIndexed()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    m_color[x, y] = m_ColorPalette.Entries[m_ImageData[y * stride + x]];
        }
        void Format4BppIndexed()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    if (x % 2 == 0)
                        m_color[x, y] = m_ColorPalette.Entries[LowByte(m_ImageData[y * stride + x / 2])];
                    else
                        m_color[x, y] = m_ColorPalette.Entries[HighByte(m_ImageData[y * stride + x / 2])];
        }
        void Format1BppIndexed()
        {
            int rest = Width % 8;
            byte bits;
            int x, y;
            for (y = 0; y < Height; y++)
            {
                for (x = 0; x < Width - 8; x += 8)
                {
                    bits = m_ImageData[y * stride + x / 8];
                    m_color[x, y] = m_ColorPalette.Entries[(bits & 128) / 128];
                    m_color[x + 1, y] = m_ColorPalette.Entries[(bits & 64) / 64];
                    m_color[x + 2, y] = m_ColorPalette.Entries[(bits & 32) / 32];
                    m_color[x + 3, y] = m_ColorPalette.Entries[(bits & 16) / 16];
                    m_color[x + 4, y] = m_ColorPalette.Entries[(bits & 8) / 8];
                    m_color[x + 5, y] = m_ColorPalette.Entries[(bits & 4) / 4];
                    m_color[x + 6, y] = m_ColorPalette.Entries[(bits & 2) / 2];
                    m_color[x + 7, y] = m_ColorPalette.Entries[bits & 1];
                }
                bits = m_ImageData[y * stride + x / 8];
                int teiler = 128;
                for (int i = 0; i < rest; i++)
                {
                    m_color[x + i, y] = m_ColorPalette.Entries[(bits & teiler) / teiler];
                    teiler /= 2;
                }
            }
        }
        
        private int HighByte(byte _number)
        {
            return _number >> 4;
        }
        private int LowByte(byte _number)
        {

            return _number & 15;
        }
        public Color GetPixel(int _x, int _y)
        {
            return m_color[_x, _y];
        }
        public void SetPixel(int _x, int _y, Color _color)
        {
            m_color[_x, _y] = _color;
            m_modified = true;
        }
        private Bitmap ReturnFormat24BppRgb()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    m_ImageData[y * stride + x * 3 + 2] = m_color[x, y].R;
                    m_ImageData[y * stride + x * 3 + 1] = m_color[x, y].G;
                    m_ImageData[y * stride + x * 3] = m_color[x, y].B;
                }
            System.Drawing.Imaging.BitmapData bmpData =
               m_Image.LockBits(m_Rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
               m_Image.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(m_ImageData, 0, ptr, bytes);
            m_Image.UnlockBits(bmpData);
            m_modified = false;
            return m_Image;
        }
        private Bitmap ReturnFormat32BppArgb()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    m_ImageData[y * stride + x * 4 + 3] = m_color[x, y].A;
                    m_ImageData[y * stride + x * 4 + 2] = m_color[x, y].R;
                    m_ImageData[y * stride + x * 4 + 1] = m_color[x, y].G;
                    m_ImageData[y * stride + x * 4] = m_color[x, y].B;
                }
            System.Drawing.Imaging.BitmapData bmpData =
               m_Image.LockBits(m_Rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
               m_Image.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(m_ImageData, 0, ptr, bytes);
            m_Image.UnlockBits(bmpData);
            m_modified = false;
            return m_Image;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                m_ImageData = null;
                m_color = null;
                m_ColorPalette = null;
                m_Image.Dispose();
                Disposed = true;
            }
        }
    }
}