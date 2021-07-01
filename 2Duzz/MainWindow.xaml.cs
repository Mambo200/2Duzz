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
using LevelData;


namespace _2Duzz
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IStatusBar
    {
        public static string[] Args;
        public MainViewModel GetMainViewModel { get => this.DataContext as MainViewModel; }
        public static Level CurrentLevel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            BindingHelper.Get.Init(this);
            ImageManager.Get.Init(this);
            PanelManager.Get.Init(this, GridContent_Images);

            //ImageDrawingHelper.Get.Init(this, GridContent_Images);
            //ImageDrawingHelper.Get.CreateLayer(100, 100, 128);

            ScollViewer_Images.MainW = this;

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
            double scrollValue = 0.05d;
            e.Handled = true;
            if (e.Delta < 0)
                scrollValue *= -1;
            GetMainViewModel.GridContentScale = Math.Max(0.1, GetMainViewModel.GridContentScale + scrollValue);
        }

        /// <summary>
        /// Header New Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderNewClick(object _parameter)
        {
            // Create and open new Window
            WindowsXAML.NewMap newMap = new WindowsXAML.NewMap();
            newMap.ShowDialog();

            // Check DialogResult. If not true then stop.
            if (newMap.DialogResult != true)
                return;


            ChangeStatusBar("Create Level ...");

            // returned true, create new Level
            CurrentLevel = new Level(
                newMap.LevelName,
                newMap.LevelSizeX,
                newMap.LevelSizeY,
                newMap.SpriteSizeX,
                newMap.SpriteSizeY
                );

            // Reset Panel
            PanelManager.Get.ClearPanels();
            PanelManager.Get.CreatePanel();

            // Set grid size
            PanelManager.Get.SetFieldSize(CurrentLevel.SpriteSizeX, CurrentLevel.SpriteSizeY, CurrentLevel.LevelSizeX, CurrentLevel.LevelSizeY, GetMainViewModel);

            // Set image size
            GetMainViewModel.ImageSizeX = CurrentLevel.SpriteSizeX;
            GetMainViewModel.ImageSizeY = CurrentLevel.SpriteSizeY;

            // Add dummy images
            for (int x = 0; x < CurrentLevel.LevelSizeX; x++)
            {
                for (int y = 0; y < CurrentLevel.LevelSizeY; y++)
                {
                    ImageManager.Get.AddImageToPanel(0);
                }
            }
            ChangeStatusBar("Level Created!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetMainViewModel.HeaderNewClickCommand = new RelayCommand((r) => ExecuteHeaderNewClick(sender));
        }

        /// <summary>
        /// When Gripsplitter moves, check for min and max values of column above it. PLEASE REWORK LATER!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            FrameworkElement parent = sender as FrameworkElement;
            //Type t = sender.GetType();
            //Type t2 = typeof(FrameworkElement);
            // check if Type of Sender is type of FrameworkElement. If Value is null, sender was not a FrameworkElement.
            if (parent == null)
                return;

            // check if Parent is null and if parent is type of Grid
            // If parent is null, escape loop
            while (parent != null && parent.Parent.GetType() != typeof(Grid))
            {
                parent = parent.Parent as FrameworkElement;
            }

            if (parent == null)
                return;

            // We have Grid, now we can check Values
            Grid g = parent.Parent as Grid;
            GetMainViewModel.StatusBarContent = g.ColumnDefinitions[Grid.GetColumn((FrameworkElement)sender) - 1].Width.Value;

            if (g.ColumnDefinitions[Grid.GetColumn((FrameworkElement)sender) - 1].Width.Value <= 200)
            {
                g.ColumnDefinitions[Grid.GetColumn((FrameworkElement)sender) - 1].Width = new GridLength(200);
            }
            else if (g.ColumnDefinitions[Grid.GetColumn((FrameworkElement)sender) - 1].Width.Value >= 1000)
            {
                g.ColumnDefinitions[Grid.GetColumn((FrameworkElement)sender) - 1].Width = new GridLength(1000);
            }
        }
    }
}
