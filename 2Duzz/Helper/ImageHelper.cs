using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;

namespace _2Duzz.Helper
{
    public class ImageHelper
    {
        private static ImageHelper m_Instance;
        public static ImageHelper Get
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new ImageHelper();
                return m_Instance;
            }
        }

        private MainWindow mainWindow;
        public void Init(MainWindow _main)
        {
            mainWindow = _main;
        }

        #region Add Image to Panel
        /// <summary>
        /// Add Image to Panel
        /// </summary>
        /// <param name="_source">Uri source</param>
        /// <param name="_layer">Layer</param>
        /// <param name="_position">Position of Image</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Uri _source, int _layer, int _position)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.UriSource = _source;
            bImg.EndInit();

            // create Image for UI Visibility
            Image img = new Image();
            img.BeginInit();
            img.Source = bImg;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            ((Panel)mainWindow.GridContent_Images.Children[_layer]).Children.Insert(_position, img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add Image to Panel
        /// </summary>
        /// <param name="_source">Uri source</param>
        /// <param name="_layer">Layer</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Uri _source, int _layer)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.UriSource = _source;
            bImg.EndInit();

            // create Image for UI Visibility
            Image img = new Image();
            img.BeginInit();
            img.Source = bImg;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            ((Panel)mainWindow.GridContent_Images.Children[_layer]).Children.Add(img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }


        /// <summary>
        /// Add Image to Panel
        /// </summary>
        /// <param name="_source">Stream source. Stream does not get disposed automatically so you need to save stream and dispose if afterwards if you do not need in anymore!</param>
        /// <param name="_layer">Layer</param>
        /// <param name="_position">Position of Image</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Stream _source, int _layer, int _position)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = _source;
            bImg.EndInit();

            // create Image for UI Visibility
            Image img = new Image();
            img.BeginInit();
            img.Source = bImg;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            ((Panel)mainWindow.GridContent_Images.Children[_layer]).Children.Insert(_position, img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add Image to Panel
        /// </summary>
        /// <param name="_source">Stream source. Stream does not get disposed automatically so you need to save stream and dispose if afterwards if you do not need in anymore!</param>
        /// <param name="_layer">Layer</param>
        /// <param name="_position">Position of Image</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Stream _source, int _layer)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = _source;
            bImg.EndInit();

            // create Image for UI Visibility
            Image img = new Image();
            img.BeginInit();
            img.Source = bImg;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            ((Panel)mainWindow.GridContent_Images.Children[_layer]).Children.Add(img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add Image to Panel
        /// </summary>
        /// <param name="_source">Uri source</param>
        /// <param name="_panel">Panel where the image gets inserted</param>
        /// <param name="_position">Position of Image</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Uri _source, Panel _panel, int _position)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.UriSource = _source;
            bImg.EndInit();

            // create Image for UI Visibility
            Image img = new Image();
            img.BeginInit();
            img.Source = bImg;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            _panel.Children.Insert(_position, img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add Image to Panel
        /// </summary>
        /// <param name="_source">Uri source</param>
        /// <param name="_panel">Panel where the image gets inserted</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Uri _source, Panel _panel)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.UriSource = _source;
            bImg.EndInit();

            // create Image for UI Visibility
            Image img = new Image();
            img.BeginInit();
            img.Source = bImg;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            _panel.Children.Add(img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add Image to Panel
        /// </summary>
        /// <param name="_source">Stream source. Stream does not get disposed automatically so you need to save stream and dispose if afterwards if you do not need in anymore!</param>
        /// <param name="_panel">Panel where the image gets inserted</param>
        /// <param name="_position">Position of Image</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Stream _source, Panel _panel, int _position)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = _source;
            bImg.EndInit();

            // create Image for UI Visibility
            Image img = new Image();
            img.BeginInit();
            img.Source = bImg;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            _panel.Children.Insert(_position, img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add Image to Panel
        /// </summary>
        /// <param name="_source">Stream source. Stream does not get disposed automatically so you need to save stream and dispose if afterwards if you do not need in anymore!</param>
        /// <param name="_panel">Panel where the image gets inserted</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Stream _source, Panel _panel)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = _source;
            bImg.EndInit();

            // create Image for UI Visibility
            Image img = new Image();
            img.BeginInit();
            img.Source = bImg;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            _panel.Children.Add(img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }


        #endregion

    }
}
