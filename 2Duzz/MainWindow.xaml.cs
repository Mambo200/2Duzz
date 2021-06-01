using _2Duzz.ViewModels;
using System;
using System.Collections.Generic;
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

            ScollViewer_Images.main = this;
        }

        public void ChangeStatusBar(object _content)
        {
            Label_Statusbar.Content = _content;
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
    }
}
