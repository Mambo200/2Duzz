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
using System.Windows.Shapes;

namespace _2Duzz.Images
{
    /// <summary>
    /// Interaktionslogik für SplitPicWindow.xaml
    /// </summary>
    public partial class SplitPicWindow : Window
    {
        public SplitPicViewModel GetMainViewModel { get => this.DataContext as SplitPicViewModel; }

        public SplitPicWindow()
        {
            InitializeComponent();
        }

        public void ApplyImage(Uri _source)
        {
            ImageSource source = new BitmapImage(_source);
            GetMainViewModel.SelectedImageSource = source;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapSource bs = ImageShow.Source as BitmapSource;
        }
    }
}
