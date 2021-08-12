using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Duzz.Images
{
    public class SplitPic : IDisposable
    {
        private SplitPic() { }

        /// <summary>Current Image</summary>
        public Bitmap ImageData { get; private set; }
        private Stream m_imageStream;

        #region Constructor
        /// <summary>
        /// Load Bitmap from Path
        /// </summary>
        /// <param name="_path">Path of file</param>
        public SplitPic(string _path)
        {
            m_imageStream = new StreamReader(_path).BaseStream;
            ImageData = new Bitmap(m_imageStream);
        }

        /// <summary>
        /// Load Bitmap from Image
        /// </summary>
        /// <param name="_original">Image</param>
        public SplitPic(Image _original)
        {
            ImageData = new Bitmap(_original);
        }

        /// <summary>
        /// Load Bitmap from Stream
        /// </summary>
        /// <param name="_stream">Stream of image</param>
        public SplitPic(Stream _stream)
        {
            m_imageStream = _stream;
            ImageData = new Bitmap(m_imageStream);
        }
        #endregion

        /// <summary>
        /// Split actual Image
        /// </summary>
        /// <param name="_width">Width of split size</param>
        /// <param name="_height">Height of split size</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"/>
        public Bitmap[,] SplitImage(int _width, int _height, BackgroundWorker _worker = null)
        {
            if (!CanSplit(_width, _height))
                throw new NotImplementedException("You tried to split an image, but the split size cannot be used for splitting.");

            // check how many pictures are made by splitting
            int xPictureCount = ImageData.Width / _width;
            int yPictureCount = ImageData.Height / _height;

            // create stream array
            int imgCount = 0;
            Bitmap[,] toReturn = new Bitmap[xPictureCount, yPictureCount];

            for (int streamX = 0; streamX < xPictureCount; streamX++)
            {
                for (int streamY = 0; streamY < yPictureCount; streamY++)
                {
                    toReturn[streamX, streamY] = new Bitmap(_width, _height);

                    for (int imageX = streamX * _width; imageX < _width * (streamX + 1); imageX++)
                    {
                        for (int imageY = streamY * _height; imageY < _height * (streamY + 1); imageY++)
                        {
                            toReturn[streamX, streamY].SetPixel(imageX % _width, imageY % _height, ImageData.GetPixel(imageX, imageY));
                        }
                    }

                    if (_worker != null)
                    {
                        _worker.ReportProgress(++imgCount);

                        // We need to sleep here for the main window to react.
                        System.Threading.Thread.Sleep(1);
                    }
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Check for left pixel
        /// </summary>
        /// <param name="_width">Width of split size</param>
        /// <param name="_height">Height of split size</param>
        /// <param name="_xPixelleft">Amount of how much pixel left in width size</param>
        /// <param name="_yPixelLeft">Amount of how much pixel left in heigth size</param>
        public void CanSplit(int _width, int _height, out int _xPixelleft, out int _yPixelLeft)
        {
            _xPixelleft = ImageData.Width % _width;
            _yPixelLeft = ImageData.Height % _height;
        }

        /// <summary>
        /// Check if Image can be splitted without complications
        /// </summary>
        /// <param name="_width">Width of split size</param>
        /// <param name="_height">Height of split size</param>
        /// <returns></returns>
        public bool CanSplit(int _width, int _height)
        {
            CanSplit(_width, _height, out int xLeft, out int yLeft);

            return (xLeft == 0 && yLeft == 0);
        }

        /// <summary>
        /// Check if image can be splitted by its width
        /// </summary>
        /// <param name="_width">width</param>
        /// <returns></returns>
        public bool CanSplitWidth(int _width)
        {
            if (_width <= 0)
                return false;
            return ImageData.Width % _width == 0;
        }

        public bool CanSplitHeight(int _height)
        {
            if (_height <= 0)
                return false;
            return ImageData.Height % _height == 0;
        }

        public void Dispose()
        {
            if (m_imageStream != null)
                m_imageStream.Dispose();

            ImageData.Dispose();
        }
    }
}
