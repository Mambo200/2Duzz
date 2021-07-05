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
        private Image CurrentSelectedImage { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            BindingHelper.Get.Init(this);
            ImageManager.Get.Init(this);
            PanelManager.Get.Init(this, GridContent_Images);
            TabItemManager.Get.Init(this, TabControl_Sprites);
            ScollViewer_Images.MainW = this;

            #region Testing Only
            // TESTING PURPOSES!
            TabItemManager.Get.AddTabItem("Papagei");
            TabItemManager.Get.AddTabItem("Affe");
            TabItemManager.Get.AddTabItem("Urangutan");
            TabItemManager.Get.AddImageToTabItem(0, new Uri("E:\\Tobias\\Bilder\\ebf5__150_player_emotes_by_kupogames-dbn7dy7\\emo0010.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("E:\\Tobias\\Bilder\\ebf5__150_player_emotes_by_kupogames-dbn7dy7\\emo0010.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("E:\\Tobias\\Bilder\\ebf5__150_player_emotes_by_kupogames-dbn7dy7\\emo0010.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("E:\\Tobias\\Bilder\\ebf5__150_player_emotes_by_kupogames-dbn7dy7\\emo0010.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("E:\\Tobias\\Bilder\\ebf5__150_player_emotes_by_kupogames-dbn7dy7\\emo0010.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/XTiny.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/XTiny.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/XTiny.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/XTiny.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);

            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X2.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X2.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X2.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X2.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(1, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/X2.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);

            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Debuf Mode.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(2, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/Outline.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            #endregion

        }

        public void ChangeStatusBar(object _content)
        {
            GetMainViewModel.StatusBarContent = _content;
        }

        [Obsolete("We scroll without STRG now")]
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

            // We Use Math.Max because if scale is negative, the level does flip.
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
        private void GridSplitter_CheckWidth(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
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

        /// <summary>
        /// When Gripsplitter moves, check for min and max values of row above it. PLEASE REWORK LATER!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridSplitter_CheckHeight(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
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
            GetMainViewModel.StatusBarContent = g.RowDefinitions[Grid.GetRow((FrameworkElement)sender) - 1].Height.Value;

            if (g.RowDefinitions[Grid.GetRow((FrameworkElement)sender) - 1].Height.Value <= 200)
            {
                g.RowDefinitions[Grid.GetRow((FrameworkElement)sender) - 1].Height = new GridLength(200);
            }
            else if (g.RowDefinitions[Grid.GetRow((FrameworkElement)sender) - 1].Height.Value >= 1000)
            {
                g.RowDefinitions[Grid.GetRow((FrameworkElement)sender) - 1].Height = new GridLength(1000);
            }
        }


        private void Img_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image tmp = (Image)sender;
            ChangeStatusBar($"{tmp.Tag.ToString()}");
        }

        private void Img_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image tmp = (Image)sender;
            
            System.Windows.Media.Effects.BlurBitmapEffect ef = new System.Windows.Media.Effects.BlurBitmapEffect();
            CurrentSelectedImage = tmp;
            ChangeStatusBar($"Selected Image: {tmp.Tag.ToString()}");
        }

    }
}
