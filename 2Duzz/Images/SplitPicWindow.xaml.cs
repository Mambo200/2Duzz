﻿using _2Duzz.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace _2Duzz.Images
{
    /// <summary>
    /// Interaktionslogik für SplitPicWindow.xaml
    /// </summary>
    public partial class SplitPicWindow : Window
    {
        public SplitPicViewModel GetMainViewModel { get => this.DataContext as SplitPicViewModel; }
        public readonly string filePath;
        public string folderPath;
        /// <summary>
        /// Initialized the <see cref="SplitPicWindow"/>.
        /// </summary>
        /// <param name="_source">Source of image</param>
        public SplitPicWindow(Uri _source)
        {
            InitializeComponent();
            ApplyImage(_source);
            filePath = GetMainViewModel.RemoveFileAtBeginning(_source.ToString());

            GridLines.Width = GetMainViewModel.SelectedImageSource.Width;
            GridLines.Height = GetMainViewModel.SelectedImageSource.Height;
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {

            if (m_splitWorker != null
                && m_splitWorker.IsBusy)
            {
                e.Cancel = true;
                return;
            }

            // Disposing streams from splitted images
            if (m_splittedImages == null)
                return;

            int xCount = m_splittedImages.GetLength(0);
            int yCount = m_splittedImages.GetLength(1);

            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    m_splittedImages[x, y].Dispose();
                }
            }

        }

        private void ConvertCommand(object obj)
        {
            m_splitCountWidth = GetMainViewModel.CountW;
            m_splitCountHeight = GetMainViewModel.CountH;
            SplitWidth.IsEnabled = false;
            SplitHeight.IsEnabled = false;

            // preventing user to click on Button multiple times
            Button_Convert.IsEnabled = false;
            folderPath = filePath + " Split" + GetMainViewModel.SplitPixelWidthText + "-" + GetMainViewModel.SplitPixelHeightText;

            m_splitWorker = new BackgroundWorker();
            m_splitWorker.WorkerReportsProgress = true;
            m_splitWorker.DoWork += BackgroundWorker_SplitImage_DoWork;
            m_splitWorker.ProgressChanged += BackgroundWorker_SplitImage_ProgressChaned;
            m_splitWorker.RunWorkerCompleted += BackgroundWorkerSplitImage_RunWorkerCompleted;
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
            BackgroundWorker bgw = sender as BackgroundWorker;

            // Phase 1: splitting
            m_splittedImages = pic.SplitImage(
                w,
                h,
                bgw
                );

            // Phase 2: saving
            // create Folder
            DirectoryInfo dInfo = Directory.CreateDirectory(folderPath);
            //DirectoryInfo dInfo = new DirectoryInfo(filePath);

            int count = m_splittedImages.GetLength(0) * m_splittedImages.GetLength(1);
            for (int y = 0; y < m_splittedImages.GetLength(1); y++)
            {
                for (int x = 0; x < m_splittedImages.GetLength(0); x++)
                {
                    string savepath = System.IO.Path.Combine(dInfo.FullName, $"{y.ToString()}-{x.ToString()}.png");
                    m_splittedImages[x, y].Save(
                        savepath,
                        System.Drawing.Imaging.ImageFormat.Png
                        );

                    if (bgw != null)
                    {
                        bgw.ReportProgress(++count);
                        System.Threading.Thread.Sleep(1);
                    }
                }
            }
        }
        private void BackgroundWorker_SplitImage_ProgressChaned(object sender, ProgressChangedEventArgs e)
        {
            int currentImageCount = e.ProgressPercentage;
            int maximumImageCount = m_splitCountWidth * m_splitCountHeight;

            // We divide the maximum with 2 because splitting the images consists of two phases: splitting and saving.
            double percentage = (((double)currentImageCount / (double)maximumImageCount) * (SplitImageProgress.Maximum / 2));

            SplitImageProgress.Value = SplitImageProgress.Maximum - percentage;

            // set TextBlock text
            SetPercentageTextBlock(percentage);
            this.TaskbarItemInfo.ProgressValue = percentage / 100;

        }
        private void BackgroundWorkerSplitImage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Check folderpath for the case splitting went wrong
            if (!Directory.Exists(folderPath))
            {
                // Folder was not created --> something went wrong
                MessageBox.Show("Something went wrong. Image was not splitted.", "Splitting error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Folder was created --> everything fine
                MessageBoxResult result = MessageBox.Show(
                    $"Image was splitted successfully into {(GetMainViewModel.CountH * GetMainViewModel.CountW).ToString()} smaller images. Copied to\n{folderPath}\n\nOpen Folder?",
                    "Convertion complete!",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    ProcessStartInfo StartInformation = new ProcessStartInfo();
                    StartInformation.FileName = folderPath;
                    Process process = Process.Start(StartInformation);
                }
            }

            Close();
        }
        #endregion

        #region Percentage TextBlock
        private void SetPercentageTextBlock(double _percent)
        {
            if (_percent < SplitImageProgress.Maximum / 2)
                // Phase 1
                SetPercentageTextBlockPhaseOne(_percent);
            else
                // Phase 2
                SetPercentageTextBlockPhaseTwo(_percent);
        }

        private void SetPercentageTextBlockPhaseOne(double _percent)
        {
            const string convertingType = "Split Image";
            double rounded = Math.Round(_percent, 2);
            SplitImageTextBlock.Text = $"{convertingType} - {rounded}%";
        }
        private void SetPercentageTextBlockPhaseTwo(double _percent)
        {
            const string convertingType = "Save Images";
            double rounded = Math.Round(_percent, 2);
            SplitImageTextBlock.Text = $"{convertingType} - {rounded}%";
        }
        #endregion

        private void SplitWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            GridLines.ColumnDefinitions.Clear();
            int w = GetMainViewModel.CountW;
            if (w < 0)
                return;

            for (int i = 0; i < w; i++)
                GridLines.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void SplitHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            GridLines.RowDefinitions.Clear();
            int h = GetMainViewModel.CountH;
            if (h < 0)
                return;

            for (int i = 0; i < h; i++)
                GridLines.RowDefinitions.Add(new RowDefinition());
        }
    }
}
