using _2Duzz.Helper;
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
    public partial class MainWindow : Window, IStatusBar
    {
        public static string[] Args;
        public MainViewModel GetMainViewModel { get => this.DataContext as MainViewModel; }
        public MainWindow()
        {
            InitializeComponent();

            BindingHelper.Get.Init(this);
            ImageHelper.Get.Init(this);
            PanelManager.Get.Init(this, GridContent_Images);
            ScollViewer_Images.MainW = this;
            //Image i = ImageHelper.Get.AddImageToPanel(new Uri("E:\\Tobias\\Bilder\\ebf5__150_player_emotes_by_kupogames-dbn7dy7\\emo0001.jpg"), 2);
            //ChangeStatusBar(((Panel)GridContent_Images.Children[2]).Children.IndexOf(i));

            PanelManager.Get.CreatePanel();
            ImageHelper.Get.AddImageToPanel(new Uri("E:\\Tobias\\Bilder\\ebf5__150_player_emotes_by_kupogames-dbn7dy7\\emo0001.jpg"), PanelManager.Get.Panels[0]);
            ImageHelper.Get.AddImageToPanel(new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Outline.png"), PanelManager.Get.Panels[0]);
            ImageHelper.Get.AddImageToPanel(new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X.png"), PanelManager.Get.Panels[0]);
            ImageHelper.Get.AddImageToPanel(new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), PanelManager.Get.Panels[0]);

            Panel p2 = PanelManager.Get.CreatePanel();
            ImageHelper.Get.AddImageToPanel(new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X2.png"), p2);
        }

        public void ChangeStatusBar(object _content)
        {
            GetMainViewModel.StatusBarContent = _content;
        }

        private void Zoom_MouseWheelWithCtrl(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                GetMainViewModel.GridContentScale = Math.Max(0.1, GetMainViewModel.GridContentScale + e.Delta * 0.001);
            }
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

        private void Zoom_MouseWheelWithoutCtrl(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            GetMainViewModel.GridContentScale = Math.Max(0.01, GetMainViewModel.GridContentScale + e.Delta * 0.001);            
        }
    }
}
