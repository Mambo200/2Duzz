using _2Duzz.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2Duzz
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string[] Args;
        public MainViewModel GetMainViewModel { get => (MainViewModel)this.DataContext; }
        public MainWindow()
        {
            InitializeComponent();

            BindingHelper.Get.Init(this);
            ScollViewer_Images.MainW = this;
            AddImageToPanel(new Uri("E:\\Tobias\\Bilder\\ebf5__150_player_emotes_by_kupogames-dbn7dy7\\emo0001.jpg"));
        }

        /// <summary>
        /// Add Image to Wrappanel
        /// </summary>
        /// <param name="_source">Uri source</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Uri _source)
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
            WrapPanel_Images.Children.Add(img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        /// <summary>
        /// Add Image to Wrappanel
        /// </summary>
        /// <param name="_source">Stream source. Stream does not get disposed automatically so you need to save stream and dispose if afterwards if you do not need in anymore!</param>
        /// <returns>added Image</returns>
        public Image AddImageToPanel(Stream _source)
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
            WrapPanel_Images.Children.Add(img);

            // Set Binding
            BindingOperations.SetBinding(img, Image.WidthProperty, BindingHelper.Get.BindingImageSizeWidth);
            BindingOperations.SetBinding(img, Image.HeightProperty, BindingHelper.Get.BindingImageSizeHeight);

            return img;
        }

        public void ChangeStatusBar(object _content)
        {
            GetMainViewModel.StatusBarContent = _content;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ImageClick(object sender, MouseButtonEventArgs e)
        {
            Point currentPosition = e.GetPosition(this);
            HitTestResult result = VisualTreeHelper.HitTest(this, currentPosition);
            object o = result.VisualHit.GetValue(Image.TagProperty);
            if (o != null)
            {
                ChangeStatusBar($"{ DateTime.Now} | {o}");
            }

        }

        private void Zoom_MouseWheelWithCtrl(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                GetMainViewModel.WPScale = Math.Max(0.1, GetMainViewModel.WPScale + e.Delta * 0.001);
            }
        }

        private void Zoom_MouseWheelWithoutCtrl(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            GetMainViewModel.WPScale = Math.Max(0.01, GetMainViewModel.WPScale + e.Delta * 0.001);            
        }
    }
}
