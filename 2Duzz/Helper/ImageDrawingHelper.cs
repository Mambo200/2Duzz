using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _2Duzz.Helper
{
    [Obsolete("Do not use please.", false)]
    public class ImageDrawingHelper
    {
        #region Constructor
        private static ImageDrawingHelper m_Instance;
        public static ImageDrawingHelper Get
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new ImageDrawingHelper();
                return m_Instance;
            }
        }

        private ImageDrawingHelper()
        {
            Panels = new List<Image>();
        }

        private MainWindow mainWindow;
        public void Init(MainWindow _main, Panel _panel)
        {
            mainWindow = _main;
            CurrentPanel = _panel;
        }
        #endregion

        public List<Image> Panels { get; private set; }
        public Panel CurrentPanel { get; private set; }


        public Image CreateLayer(int _x, int _y, int _imageSize)
        {
            Image img = CreateNewImageLayer(out DrawingImage _dImage, out DrawingGroup _dGroup);

            Panels.Add(img);

            SetRect(_dGroup, _x, _y, _imageSize);

            CurrentPanel.Children.Add(img);

            return img;
        }

        private void SetRect(DrawingGroup _dg, int _sizeX, int _sizeY, int _imageSize)
        {

            for (int x = 0; x < _sizeX * _imageSize; x = x + _imageSize)
            {
                for (int y = 0; y < _sizeY * _imageSize; y = y + _imageSize)
                {
                    ImageDrawing t = new ImageDrawing();
                    t.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(ImageManager.PLACEHOLDERPATH);
                    //BitmapImage bImage = new BitmapImage();
                    //bImage.BeginInit();
                    //bImage.UriSource = new Uri(ImageManager.PLACEHOLDERPATH);
                    //bImage.EndInit();
                    //t.ImageSource = bImage;
                    t.Rect = new System.Windows.Rect(x, y, _imageSize, _imageSize);
                    _dg.Children.Add(t);
                }
            }
        }

        /// <summary>
        /// Get Drawing Image
        /// </summary>
        /// <param name="_layer">Index of Layer</param>
        /// <returns>Drawing Image</returns>
        public DrawingImage GetDrawingImage(int _layer) { return ((Image)CurrentPanel.Children[_layer]).Source as DrawingImage; }

        /// <summary>
        /// Get Drawing Image
        /// </summary>
        /// <param name="_layer">Layer as Image</param>
        /// <returns>Drawing Image</returns>
        public DrawingImage GetDrawingImage(Image _layer) { return _layer.Source as DrawingImage; }

        /// <summary>
        /// Get Drawing Group
        /// </summary>
        /// <param name="_layer">Index of Layer</param>
        /// <returns>Drawing Group</returns>
        public DrawingGroup GetDrawingGroup(int _layer) { return ((DrawingImage)((Image)CurrentPanel.Children[_layer]).Source).Drawing as DrawingGroup; }


        /// <summary>
        /// Get Drawing Group
        /// </summary>
        /// <param name="_layer">Layer as Image</param>
        /// <returns>Drawing Group</returns>
        public DrawingGroup GetDrawingGroup(Image _layer) { return ((DrawingImage)_layer.Source).Drawing as DrawingGroup; }

        /// <summary>
        /// Get Drawing Group
        /// </summary>
        /// <param name="_layer">Layer as DrawingImage</param>
        /// <returns>Drawing Group</returns>
        public DrawingGroup GetDrawingGroup(DrawingImage _layer) { return _layer.Drawing as DrawingGroup; }

        /// <summary>
        /// Creates Image with DrawingGroup. Does not add anything to <see cref="Panels"/> or <see cref="DrawingGroups"/>
        /// </summary>
        /// <param name="_dImage">DrawingImage as <see cref="Image.Source"/>.</param>
        /// <param name="_dGroup">DrawingGroup as <see cref="DrawingImage.Drawing"/>. Children of this are <see cref="ImageDrawing"/>.</param>
        /// <returns></returns>
        private Image CreateNewImageLayer(out DrawingImage _dImage, out DrawingGroup _dGroup)
        {
            Image tr = new Image();
            _dImage = new DrawingImage();
            _dGroup = new DrawingGroup();

            _dImage.Drawing = _dGroup;
            tr.Source = _dImage;

            return tr;
        }
    }
}
