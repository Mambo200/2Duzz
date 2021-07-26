﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace _2Duzz.Helper
{
    public class ImageManager
    {
        //public const string PLACEHOLDERPATH = "pack://application:,,,/2Duzz;component/Ressources/TestImages/XTiny.png";
        public const string PLACEHOLDERPATH = "pack://application:,,,/2Duzz;component/Ressources/TestImages/AlphaDot.png";

        private static ImageManager m_Instance;
        public static ImageManager Get
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new ImageManager();
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
        /// Add placeholder Image to Panel
        /// </summary>
        /// <param name="_layer">Layer</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(int _layer)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = PrepareBitmapImage(new Uri(PLACEHOLDERPATH));

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
            ((Panel)mainWindow.GridContent_Images.Children[_layer]).Children.Add(img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add placeholder Image to Panel
        /// </summary>
        /// <param name="_layer">Layer</param>
        /// <param name="_position">Position of Image</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(int _layer, int _position)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = PrepareBitmapImage(new Uri(PLACEHOLDERPATH));

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
            ((Panel)mainWindow.GridContent_Images.Children[_layer]).Children.Insert(_position, img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add placeholder Image to Panel
        /// </summary>
        /// <param name="_panel">Panel where the image gets inserted</param>
        /// <param name="_position">Position of Image</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Panel _panel, int _position)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = PrepareBitmapImage(new Uri(PLACEHOLDERPATH));

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
            _panel.Children.Insert(_position, img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add placeholder Image to Panel
        /// </summary>
        /// <param name="_panel">Panel where the image gets inserted</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Panel _panel)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = PrepareBitmapImage(new Uri(PLACEHOLDERPATH));

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
            _panel.Children.Add(img);

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
        /// <param name="_position">Position of Image</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Uri _source, int _layer, int _position)
        {
            // create BitmapImage for Image source
            BitmapImage bImg = PrepareBitmapImage(_source);

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
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
            BitmapImage bImg = PrepareBitmapImage(_source);

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
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
            BitmapImage bImg = PrepareBitmapImage(_source);

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
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
            BitmapImage bImg = PrepareBitmapImage(_source);

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
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
            BitmapImage bImg = PrepareBitmapImage(_source);

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
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
            BitmapImage bImg = PrepareBitmapImage(_source);

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
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
            BitmapImage bImg = PrepareBitmapImage(_source);

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
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
            BitmapImage bImg = PrepareBitmapImage(_source);

            // create Image for UI Visibility
            Image img = PrepareImageForUI(bImg);
            _panel.Children.Add(img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }
        #endregion

        public void RemoveImageFromPanel(int _layer, int _position)
        {
            ((Panel)mainWindow.GridContent_Images.Children[_layer]).Children.RemoveAt(_position);
        }
        public int RemoveImageFromPanel(int _layer, Image _image)
        {
            int index = ((Panel)mainWindow.GridContent_Images.Children[_layer]).Children.IndexOf(_image);

            if (index >= 0)
                RemoveImageFromPanel(_layer, index);

            return index;
        }

        #region Image Preperation
        /// <summary>
        /// Create <see cref="BitmapImage"/>
        /// </summary>
        /// <param name="_source">source</param>
        /// <returns><see cref="BitmapImage"/></returns>
        private BitmapImage PrepareBitmapImage(Uri _source)
        {
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.UriSource = _source;
            bImg.EndInit();

            return bImg;
        }

        /// <summary>
        /// Create <see cref="BitmapImage"/>
        /// </summary>
        /// <param name="_source">source</param>
        /// <returns><see cref="BitmapImage"/></returns>
        private BitmapImage PrepareBitmapImage(Stream _source)
        {
            BitmapImage bImg = new BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = _source;
            bImg.EndInit();

            return bImg;
        }

        /// <summary>
        /// Create <see cref="Image"/>
        /// </summary>
        /// <param name="_source">Image source to display</param>
        /// <returns>Fully created <see cref="Image"/> with Stats</returns>
        private Image PrepareImageForUI(ImageSource _source)
        {
            Image img = new Image();
            img.BeginInit();
            img.Source = _source;
            img.Stretch = Stretch.Fill;
            img.MouseLeftButtonDown += ImageClick;
            img.Tag = img.Source;
            img.EndInit();

            return img;
        }
        #endregion

        private void ImageClick(object sender, MouseButtonEventArgs e)
        {
            Point currentPosition = e.GetPosition(mainWindow);
            HitTestResult result = VisualTreeHelper.HitTest(mainWindow, currentPosition);
            //object o = result.VisualHit.GetValue(Image.TagProperty);
            //if (o.GetType() == typeof(Image))
            //{
            //    mainWindow.ChangeStatusBar($"{ DateTime.Now} | {o}");
            //}

            if (result.VisualHit.GetType() == typeof(Image)
                && mainWindow.CurrentSelectedImage != null)
            {

                int index = RemoveImageFromPanel(mainWindow.CurrentLayer, result.VisualHit as Image);
                
                BitmapImage i = PrepareBitmapImage(new Uri(mainWindow.CurrentSelectedImage.Tag.ToString()));
                Image newImg = PrepareImageForUI(i);

                AddImageToPanel(new Uri(mainWindow.CurrentSelectedImage.Tag.ToString()), mainWindow.CurrentLayer, index);


                    mainWindow.ChangeStatusBar($"{ DateTime.Now}");
            }
        }

    }
}
