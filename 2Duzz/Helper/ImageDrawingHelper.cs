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
            ImageLayer = new List<Image>();
        }

        private MainWindow mainWindow;
        public void Init(MainWindow _main, Panel _panel)
        {
            mainWindow = _main;
            CurrentPanel = _panel;
        }
        #endregion

        public List<Image> ImageLayer { get; private set; }
        public Panel CurrentPanel { get; private set; }

        #region Add Images
        /// <summary>
        /// Add Image
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSize">Size of Sprite</param>
        /// <param name="_layer">Layer to insert Image</param>
        /// <returns></returns>
        public ImageDrawing AddImage(int _xPosition, int _yPosition, double _imageSize, int _layer)
        {
            DrawingGroup dg = GetDrawingGroup(_layer);
            ImageDrawing t = new ImageDrawing();
            t.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(ImageManager.PLACEHOLDERPATH);
            t.Rect = new System.Windows.Rect(_xPosition * _imageSize, _yPosition * _imageSize, _imageSize, _imageSize);
            dg.Children.Add(t);

            return t;
        }

        /// <summary>
        /// Add Image
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSize">Size of Sprite</param>
        /// <param name="_layer">Layer to insert Image</param>
        /// <param name="_source">source of Image</param>
        /// <returns></returns>
        public ImageDrawing AddImage(int _xPosition, int _yPosition, double _imageSize, int _layer, string _source)
        {
            DrawingGroup dg = GetDrawingGroup(_layer);
            ImageDrawing t = new ImageDrawing();
            t.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(_source);
            t.Rect = new System.Windows.Rect(_xPosition * _imageSize, _yPosition * _imageSize, _imageSize, _imageSize);
            dg.Children.Add(t);

            return t;
        }

        /// <summary>
        /// Add Image
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSize">Size of Sprite</param>
        /// <param name="_dg">Drawinggroup to insert Image</param>
        /// <returns></returns>
        public ImageDrawing AddImage(int _xPosition, int _yPosition, double _imageSize, DrawingGroup _dg)
        {
            ImageDrawing t = new ImageDrawing();
            t.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(ImageManager.PLACEHOLDERPATH);
            t.Rect = new System.Windows.Rect(_xPosition * _imageSize, _yPosition * _imageSize, _imageSize, _imageSize);
            _dg.Children.Add(t);

            return t;
        }

        /// <summary>
        /// Add Image
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSize">Size of Sprite</param>
        /// <param name="_dg">Drawinggroup to insert Image</param>
        /// <returns></returns>
        public ImageDrawing AddImage(int _xPosition, int _yPosition, double _imageSize, DrawingGroup _dg, string _source)
        {
            ImageDrawing t = new ImageDrawing();
            t.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(_source);
            t.Rect = new System.Windows.Rect(_xPosition * _imageSize, _yPosition * _imageSize, _imageSize, _imageSize);
            _dg.Children.Add(t);

            return t;
        }
        #endregion

        /// <summary>
        /// Create Layer on Field
        /// </summary>
        /// <param name="_x">Amount of Images in width</param>
        /// <param name="_y">Amount of Images in height</param>
        /// <param name="_imageSize">Size of Image in Pixels</param>
        /// <returns>Layer as Image</returns>
        public Image CreateLayer(int _x, int _y, int _imageSize)
        {
            Image img = CreateNewImageLayer(out DrawingImage _dImage, out DrawingGroup _dGroup);

            ImageLayer.Add(img);

            SetRect(_x, _y, _imageSize, _dGroup);

            CurrentPanel.Children.Add(img);

            return img;
        }

        /// <summary>
        /// Create Layer on Field
        /// </summary>
        /// <param name="_x">Amount of Images in width</param>
        /// <param name="_y">Amount of Images in height</param>
        /// <param name="_imageSize">Size of Image in Pixels</param>
        /// <param name="_layerIndex">Index of Layer to insert</param>
        /// <returns>Layer as Image</returns>
        public Image CreateLayer(int _x, int _y, int _imageSize, int _layerIndex)
        {
            Image img = CreateNewImageLayer(out DrawingImage _dImage, out DrawingGroup _dGroup);

            ImageLayer.Insert(_layerIndex, img);

            SetRect(_x, _y, _imageSize, _dGroup);

            CurrentPanel.Children.Insert(_layerIndex, img);

            return img;
        }

        public void RemoveLayer(int _layer)
        {
            ImageLayer.RemoveAt(_layer);
            CurrentPanel.Children.RemoveAt(_layer);
        }

        public void RemoveLayer(Image _image)
        {
            ImageLayer.Remove(_image);
            CurrentPanel.Children.Remove(_image);
        }

        public void ClearLayer()
        {
            ImageLayer.Clear();
            CurrentPanel.Children.Clear();
        }

        /// <summary>
        /// Fill Layer with Placeholder Image
        /// </summary>
        /// <param name="_sizeX">Amount of Images on X-Axis</param>
        /// <param name="_sizeY">Amount of Images on Y-Axis</param>
        /// <param name="_imageSize">Size of Images in Pixel</param>
        /// <param name="_layer">Layerindex to insert Images</param>
        private void SetRect(int _sizeX, int _sizeY, int _imageSize, int _layer)
        {

            for (int x = 0; x < _sizeX; x = x++)
            {
                for (int y = 0; y < _sizeY; y = y++)
                {
                    AddImage(x, y, _imageSize, _layer);
                }
            }
        }

        /// <summary>
        /// Fill Layer with Placeholder Image
        /// </summary>
        /// <param name="_sizeX">Amount of Images on X-Axis</param>
        /// <param name="_sizeY">Amount of Images on Y-Axis</param>
        /// <param name="_imageSize">Size of Images in Pixel</param>
        /// /// <param name="_dg">Drawinggroup to insert Images</param>
        private void SetRect(int _sizeX, int _sizeY, int _imageSize, DrawingGroup _dg)
        {

            for (int x = 0; x < _sizeX; x++)
            {
                for (int y = 0; y < _sizeY; y++)
                {
                    AddImage(x, y, _imageSize, _dg);
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
        /// Creates Image with DrawingGroup. Does not add anything to <see cref="ImageLayer"/> or <see cref="DrawingGroups"/>
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
