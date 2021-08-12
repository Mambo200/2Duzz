using _2Duzz.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        /// <summary>
        /// Initialized the <see cref="SplitPicWindow"/>.
        /// </summary>
        /// <param name="_source">Source of image</param>
        public SplitPicWindow(Uri _source)
        {
            InitializeComponent();
            ApplyImage(_source);
        }

        /// <summary>
        /// This has to be called before the window gets opened
        /// </summary>
        /// <param name="_source">source of Image</param>
        private void ApplyImage(Uri _source)
        {
            ImageSource source = new BitmapImage(_source);
            GetMainViewModel.SelectedImageSource = source;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetMainViewModel.ConvertButtonPressedCommand = new RelayCommand((r) => ConvertCommand(Button_Convert));
        }

        private void ConvertCommand(object obj)
        {
            m_splitCountWidth = GetMainViewModel.CountW;
            m_splitCountHeight = GetMainViewModel.CountH;
            
            m_splitWorker = new BackgroundWorker();
            m_splitWorker.WorkerReportsProgress = true;
            m_splitWorker.DoWork += BackgroundWorker_SplitImage_DoWork;
            m_splitWorker.ProgressChanged += BackgroundWorker_SplitImage_ProgressChaned;
            SplitImageProgress.Value = SplitImageProgress.Minimum;
            m_splitWorker.RunWorkerAsync(
                new object[]
                { 
                    GetMainViewModel.Split, 
                    int.Parse(GetMainViewModel.SplitPixelWidthText), 
                    int.Parse(GetMainViewModel.SplitPixelHeightText) 
                }
                );
        }

        #region BackgroundWorker
        private BackgroundWorker m_splitWorker;
        private System.Drawing.Bitmap[,] m_splittedImages;
        private int m_splitCountWidth = 0;
        private int m_splitCountHeight = 0;
        private void BackgroundWorker_SplitImage_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = e.Argument as object[];
            SplitPic pic = args[0] as SplitPic;
            int w = (int)args[1];
            int h = (int)args[2];

            m_splittedImages = pic.SplitImage(
                w,
                h,
                sender as BackgroundWorker
                );
        }

        private void BackgroundWorker_SplitImage_ProgressChaned(object sender, ProgressChangedEventArgs e)
        {
            
            int currentImageCount = e.ProgressPercentage;
            int maximumImageCount = m_splitCountWidth * m_splitCountHeight;

            double percentage = ((double)currentImageCount / (double)maximumImageCount) * SplitImageProgress.Maximum;

            SplitImageProgress.Value = percentage;
        }
        #endregion
    }
}
