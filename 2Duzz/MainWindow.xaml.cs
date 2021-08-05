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
        public Image CurrentSelectedImage { get; private set; }
        public int CurrentLayer { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            BindingHelper.Get.Init(this);
            PanelManager.Get.Init(this, GridContent_Images);
            TabItemManager.Get.Init(this, TabControl_Sprites);
            ImageDrawingHelper.Get.Init(this, GridContent_Images);
            ScollViewer_Images.MainW = this;

            #region Testing Only
            // TESTING PURPOSES!
            TabItemManager.Get.AddTabItem("Papagei");
            TabItemManager.Get.AddTabItem("Affe");
            TabItemManager.Get.AddTabItem("Urangutan");
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/emo0004.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/emo0004.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/emo0004.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/emo0004.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/emo0004.jpg"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/XTiny.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/XTiny.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/XTiny.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
            TabItemManager.Get.AddImageToTabItem(0, new Uri("pack://application:,,,/2Duzz;component/Ressources/TestImages/AlphaDot.png"), Img_MouseLeftButtonDown, Img_MouseRightButtonDown);
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

        private PointHitTestResult ItemAtCursor(MouseEventArgs _mouseEvent)
        {
            Point currentPosition = _mouseEvent.GetPosition(this);
            return VisualTreeHelper.HitTest(this, currentPosition) as PointHitTestResult;
        }

        private void Zoom_MouseWheelWithoutCtrl(object sender, MouseWheelEventArgs e)
        {
            decimal scrollValue = 0.05m;
            //double scroll value if Control is hold
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                scrollValue *= 2;
            e.Handled = true;
            if (e.Delta < 0)
                scrollValue *= -1;

            // We use decimal here to have a more precise scaling, so scale from 100 to 106 should be no longer possible
            decimal newScaleValue = (decimal)GetMainViewModel.GridContentScale + (decimal)scrollValue;

            // We Use Math.Max because if scale is negative, the level does flip.
            GetMainViewModel.GridContentScale = (double)Math.Max(new decimal(0.1), newScaleValue);
        }

        #region Execute buttons
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
                )
            {

                // Set string array for images. We cannot set this yet because the first dimension will be the amount of layer and the second dimension will be the amount of images.
                LevelImages = new int[0, 0]
            };

            // Reset Panel
            ImageDrawingHelper.Get.ClearLayer();
            ImageDrawingHelper.Get.CreateLayer(CurrentLevel.LevelSizeX, CurrentLevel.LevelSizeY, CurrentLevel.SpriteSizeX, CurrentLevel.SpriteSizeY);

            // Set grid size
            GetMainViewModel.GridContentWidth = CurrentLevel.LevelSizeX * CurrentLevel.SpriteSizeX;
            GetMainViewModel.GridContentHeight = CurrentLevel.LevelSizeY * CurrentLevel.SpriteSizeY;

            // Set image size
            GetMainViewModel.ImageSizeX = CurrentLevel.SpriteSizeX;
            GetMainViewModel.ImageSizeY = CurrentLevel.SpriteSizeY;


            ChangeStatusBar("Level Created!");

            // Add Layer to List
            LayerList.Items.Clear();

            LayerList.Items.Add(0);

            CurrentLayer = 0;
            LayerList.SelectedIndex = 0;
        }

        /// <summary>
        /// Header Save Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteSaveClick(object _parameter)
        {
            if (CurrentLevel == null) return;

            string path = FileHelper.LastValidFile;

            // Check if path is valid
            if (string.IsNullOrEmpty(path))
            {
                // path not valid. Let user decide new path
                ExecuteSaveAsClick(_parameter);

                // we return here because if Method "ExecuteSaveAsClick(object)" which we use above, the file will be saved there.
                return;
            }


            SetLevelImagesStringArray();

            FileHelper.FileDialogSaveStatusText(path, CurrentLevel.SaveJson(path), this);

            ImageLoader.SaveLevelImagesFromFileToDirectory(CurrentLevel.LevelImagesData, path);
        }

        /// <summary>
        /// Header Save Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteSaveAsClick(object _parameter)
        {
            if (CurrentLevel == null) return;

            string path = Helper.FileHelper.GetSavePath();

            // Check if string is valid or not
            if (string.IsNullOrEmpty(path))
            {
                ChangeStatusBar($"File save aborted by user");
                return;
            }

            SetLevelImagesStringArray();

            FileHelper.FileDialogSaveStatusText(path, CurrentLevel.SaveJson(path), this);

            ImageLoader.SaveLevelImagesFromFileToDirectory(CurrentLevel.LevelImagesData, path);
        }


        /// <summary>
        /// Header Save Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteOpenClick(object _parameter)
        {
            string path = Helper.FileHelper.GetOpenPath();

            // Check if string is valid or not
            if (string.IsNullOrEmpty(path))
            {
                ChangeStatusBar($"File save aborted by user");
                return;
            }

            CurrentLevel = Level.ReadJSON(path);
            ImageLoader.SaveLevelImagesFromFileToDirectory(CurrentLevel.LevelImagesData, path);
            string[] imagesPaths = ImageLoader.LoadImagesFromLevelFolderToTabItem(path, Img_MouseLeftButtonDown, Img_MouseRightButtonDown, out TabItem addedTo);
            OpenLevel(CurrentLevel, addedTo);


            FileHelper.FileDialogOpenStatusText(path, CurrentLevel != null, this);
        }

        private void OpenLevel(Level _l, TabItem _tabItem)
        {
            // Reset Image Panel
            ImageDrawingHelper.Get.ClearLayer();
            int layerCount = _l.LevelImages.GetLength(0);
            ImageDrawingHelper.Get.CreateLayer(_l.LevelSizeX, _l.LevelSizeY, _l.SpriteSizeX, _l.SpriteSizeY);
            LayerList.Items.Clear();
            LayerList.Items.Add(0);
            CurrentLayer = 0;

            // create layer
            for (int i = 1; i < layerCount; i++)
            {
                ExecuteAddLayerClick(this);
            }

            // Set grid size
            GetMainViewModel.GridContentWidth = CurrentLevel.LevelSizeX * CurrentLevel.SpriteSizeX;
            GetMainViewModel.GridContentHeight = CurrentLevel.LevelSizeY * CurrentLevel.SpriteSizeY;

            // Set image size
            GetMainViewModel.ImageSizeX = CurrentLevel.SpriteSizeX;
            GetMainViewModel.ImageSizeY = CurrentLevel.SpriteSizeY;

            // replace images
            for (int l = 0; l < layerCount; l++)
            {
                for (int i = 0; i < _l.LevelSizeX * _l.LevelSizeY; i++)
                {
                    Image img = TabItemManager.Get.GetImage(_tabItem, _l.LevelImages[l, i]);
                    ImageDrawingHelper.Get.ReplaceImage(
                        i,
                        _l.SpriteSizeX,
                        _l.SpriteSizeY,
                        _l.LevelSizeX,
                        _l.LevelSizeY,
                        l,
                        img.Source.ToString()
                        );
                }
            }

            CurrentLayer = 0;
            LayerList.SelectedIndex = 0;
            ChangeStatusBar("Level Created!");
        }

        /// <summary>
        /// Set <see cref="Level.LevelImages"/> and <see cref="Level.LevelImagesData"/>
        /// </summary>
        public void SetLevelImagesStringArray()
        {
            // create string array
            CurrentLevel.LevelImages = new int
                [
                ImageDrawingHelper.Get.ImageLayer.Count,
                CurrentLevel.LevelSizeX * CurrentLevel.LevelSizeY
                ];

            List<string> base64Images = new List<string>();
            // we go here with GetLength(0) because CurrentLevel.LevelImages is a two dimensional array which first dimension stands for the different layer.
            for (int lyr = 0; lyr < CurrentLevel.LevelImages.GetLength(0); lyr++)
            {
                foreach (KeyValuePair<int, ImageDrawing> kv in ImageDrawingHelper.Get.ImagesAtLayer[lyr])
                {
                    // save Base64 Value
                    string currentB64Image = Helper.MyConverter.ToBase64((BitmapSource)kv.Value.ImageSource);

                    // Check if Image was already used.
                    int index = base64Images.IndexOf(currentB64Image);

                    if (index < 0)
                    {
                        // No index found. Add string to list and update index
                        base64Images.Add(currentB64Image);
                        index = base64Images.Count - 1;
                    }
                    CurrentLevel.LevelImages[lyr, kv.Key] = index;
                }
            }

            // save list with Base64 Images to Level data
            CurrentLevel.LevelImagesData = base64Images.ToArray();
        }

        /// <summary>
        /// Add Layer Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteAddLayerClick(object _parameter)
        {
            if (CurrentLevel == null) return;
            ImageDrawingHelper.Get.CreateLayer(CurrentLevel.LevelSizeX, CurrentLevel.LevelSizeY, CurrentLevel.SpriteSizeX, CurrentLevel.SpriteSizeY, LayerList.SelectedIndex + 1);


            LayerList.Items.Insert(LayerList.SelectedIndex + 1, LayerList.SelectedIndex + 1);

            LayerList.SelectedIndex++;
            CurrentLayer = LayerList.SelectedIndex;

            ChangeStatusBar($"Current Index: {CurrentLayer}");
        }

        /// <summary>
        /// Remove Layer Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteRemoveLayerClick(object _parameter)
        {
            if (CurrentLevel == null
                || LayerList.Items.Count <= 1) return;
            ImageDrawingHelper.Get.RemoveLayer(LayerList.SelectedIndex);

            int tempIndex = LayerList.SelectedIndex;
            LayerList.Items.RemoveAt(LayerList.SelectedIndex);
            LayerList.SelectedIndex = MathHelper.Between(tempIndex, 0, LayerList.Items.Count - 1);
            CurrentLayer = LayerList.SelectedIndex;

            ChangeStatusBar($"Current Index: {CurrentLayer}");
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetMainViewModel.HeaderNewClickCommand = new RelayCommand((r) => ExecuteHeaderNewClick(HeaderNew));
            GetMainViewModel.HeaderOpenClickCommand = new RelayCommand((r) => ExecuteOpenClick(HeaderOpen));
            GetMainViewModel.HeaderSaveClickCommand = new RelayCommand((r) => ExecuteSaveClick(HeaderSave));
            GetMainViewModel.HeaderSaveAsClickCommand = new RelayCommand((r) => ExecuteSaveAsClick(HeaderSaveAs));
            GetMainViewModel.ButtonAddLayerClickCommand = new RelayCommand((r) => ExecuteAddLayerClick(ButtonAddLayer));
            GetMainViewModel.ButtonRemoveLayerClickCommand = new RelayCommand((r) => ExecuteRemoveLayerClick(ButtonRemoveLayer));
        }

        /// <summary>
        /// When Gripsplitter moves, check for min and max values of column above it. PLEASE REWORK LATER!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridSplitter_CheckWidth(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            //Type t = sender.GetType();
            //Type t2 = typeof(FrameworkElement);
            // check if Type of Sender is type of FrameworkElement. If Value is null, sender was not a FrameworkElement.
            if (!(sender is FrameworkElement parent))
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
            //Type t = sender.GetType();
            //Type t2 = typeof(FrameworkElement);
            // check if Type of Sender is type of FrameworkElement. If Value is null, sender was not a FrameworkElement.
            if (!(sender is FrameworkElement parent))
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
            ChangeStatusBar($"{tmp.Tag}");
        }

        /// <summary>
        /// Select an Image with Border. Requires the sender to be an <see cref="Image"/> with a parent of <see cref="Border"/>/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Img_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image tmp = (Image)sender;
            Border b = (Border)(tmp.Parent);

            // unhighlight border of current selected image
            TabItemManager.Get.Unhighlight(CurrentSelectedImage);

            // highlight border of new selected image
            TabItemManager.Get.Highlight(b);

            // set current selected Image
            CurrentSelectedImage = tmp;
            ChangeStatusBar($"Selected Image: {tmp.Tag}");
        }

        private void GridContent_Images_SwitchImage(object sender, MouseEventArgs e, Point oldPosition, Point newPosition)
        {
            if (CurrentSelectedImage == null
                || e.LeftButton != MouseButtonState.Pressed)
                return;

            ImageDrawingHelper.Get.ReplaceImage(
                (int)newPosition.X,
                (int)newPosition.Y,
                CurrentLevel.SpriteSizeX,
                CurrentLevel.SpriteSizeY,
                CurrentLevel.LevelSizeX,
                CurrentLevel.LevelSizeY,
                CurrentLayer,
                CurrentSelectedImage.Source.ToString()
                );

            ChangeStatusBar(newPosition);
        }

        private void GridContent_Images_OnClickImage(object sender, MouseEventArgs e, Point imagePosition)
        {
            if (CurrentSelectedImage == null)
                return;

            ImageDrawingHelper.Get.ReplaceImage(
                (int)imagePosition.X,
                (int)imagePosition.Y,
                CurrentLevel.SpriteSizeX,
                CurrentLevel.SpriteSizeY,
                CurrentLevel.LevelSizeX,
                CurrentLevel.LevelSizeY,
                CurrentLayer,
                CurrentSelectedImage.Source.ToString()
                );
        }



        private void LayerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentLayer = LayerList.SelectedIndex;
            ChangeStatusBar($"Selected Index: {CurrentLayer}");
        }
    }
}
