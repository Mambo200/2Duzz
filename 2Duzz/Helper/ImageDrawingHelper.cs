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
        public const string PLACEHOLDERPATH = "pack://application:,,,/2Duzz;component/Ressources/A.png";

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
            ImagesAtLayer = new List<Dictionary<int, ImageDrawing>>();
        }

        private MainWindow mainWindow;
        public void Init(MainWindow _main, Panel _panel)
        {
            mainWindow = _main;
            CurrentPanel = _panel;
        }
        #endregion

        public List<Image> ImageLayer { get; private set; }
        public List<Dictionary<int, ImageDrawing>> ImagesAtLayer { get; private set; }
        public Panel CurrentPanel { get; private set; }

        #region Add Images
        /// <summary>
        /// Add Image
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSizeX">X Size of Sprite</param>
        /// <param name="_imageSizeY">Y Size of Sprite</param>
        /// <param name="_imageCountX">Width image count</param>
        /// <param name="_imageCountY">Height image count</param>
        /// <param name="_layer">Layer to insert Image</param>
        /// <returns></returns>
        private ImageDrawing AddImage(int _xPosition, int _yPosition, double _imageSizeX, double _imageSizeY, int _layer, int _imageCountX, int _imageCountY)
        {
            DrawingGroup dg = GetDrawingGroup(_layer);
            ImageDrawing t = new ImageDrawing
            {
                ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(PLACEHOLDERPATH),
                Rect = new System.Windows.Rect(_xPosition * _imageSizeX, _yPosition * _imageSizeY, _imageSizeX, _imageSizeY)
            };
            dg.Children.Add(t);

            AddToDictionary(_xPosition, _yPosition, _imageCountX, _layer, t);

            return t;
        }

        /// <summary>
        /// Add Image
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSizeX">X Size of Sprite</param>
        /// <param name="_imageSizeY">Y Size of Sprite</param>
        /// <param name="_imageCountX">Width image count</param>
        /// <param name="_imageCountY">Height image count</param>
        /// <param name="_layer">Layer to insert Image</param>
        /// <param name="_source">source of Image</param>
        /// <returns></returns>
        private ImageDrawing AddImage(int _xPosition, int _yPosition, double _imageSizeX, double _imageSizeY, int _layer, int _imageCountX, int _imageCountY, string _source)
        {
            DrawingGroup dg = GetDrawingGroup(_layer);
            ImageDrawing t = new ImageDrawing
            {
                ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(_source),
                Rect = new System.Windows.Rect(_xPosition * _imageSizeX, _yPosition * _imageSizeY, _imageSizeX, _imageSizeY)
            };
            dg.Children.Add(t);

            AddToDictionary(_xPosition, _yPosition, _imageCountX, _layer, t);

            return t;
        }

        /// <summary>
        /// Add Image
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSizeX">X Size of Sprite</param>
        /// <param name="_imageSizeY">Y Size of Sprite</param>
        /// <param name="_imageCountX">Width image count</param>
        /// <param name="_imageCountY">Height image count</param>
        /// <param name="_dg">Drawinggroup to insert Image</param>
        /// <returns></returns>
        private ImageDrawing AddImage(int _xPosition, int _yPosition, double _imageSizeX, double _imageSizeY, int _imageCountX, int _imageCountY, DrawingGroup _dg)
        {
            ImageDrawing t = new ImageDrawing
            {
                ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(ImageDrawingHelper.PLACEHOLDERPATH),
                //t.ImageSource = null;
                Rect = new System.Windows.Rect(_xPosition * _imageSizeX, _yPosition * _imageSizeY, _imageSizeX, _imageSizeY)
            };
            _dg.Children.Add(t);

            AddToDictionary(_xPosition, _yPosition, _imageCountX, GetLayerFromDrawingGroup(_dg), t);

            return t;
        }

        /// <summary>
        /// Add Image
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSizeX">X Size of Sprite</param>
        /// <param name="_imageSizeY">Y Size of Sprite</param>
        /// <param name="_imageCountX">Width image count</param>
        /// <param name="_imageCountY">Height image count</param>
        /// <param name="_dg">Drawinggroup to insert Image</param>
        /// <returns></returns>
        private ImageDrawing AddImage(int _xPosition, int _yPosition, double _imageSizeX, double _imageSizeY, DrawingGroup _dg, int _imageCountX, int _imageCountY, string _source)
        {
            ImageDrawing t = new ImageDrawing
            {
                ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString(_source),
                Rect = new System.Windows.Rect(_xPosition * _imageSizeX, _yPosition * _imageSizeY, _imageSizeX, _imageSizeY)
            };
            _dg.Children.Add(t);

            AddToDictionary(_xPosition, _yPosition, _imageCountX, GetLayerFromDrawingGroup(_dg), t);

            return t;
        }
        #endregion

        #region Replace Image
        /// <summary>
        /// Replace existing Image.
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSizeX">X Size of Sprite</param>
        /// <param name="_imageSizeY">Y Size of Sprite</param>
        /// <param name="_imageCountX">Width image count</param>
        /// <param name="_imageCountY">Height image count</param>
        /// <param name="_layer">Layer to insert Image</param>
        /// <param name="_source">source of Image</param>
        /// <returns></returns>
        public ImageDrawing ReplaceImage(int _xPosition, int _yPosition, double _imageSizeX, double _imageSizeY, int _imageCountX, int _imageCountY, int _layer, string _source)
        {
            // Get 1D Position
            int position = ChangeDimensions(_xPosition, _yPosition, (int)_imageCountX);
            
            // Get DrawingGroup
            DrawingGroup dg = GetDrawingGroup(_layer);
            
            // Get index of DrawingGroup
            int index = dg.Children.IndexOf(ImagesAtLayer[_layer][position]);
            
            // Remove ImageDrawing from DrawingGroup and Dictionary
            dg.Children.RemoveAt(index);
            RemoveFromDictionary(_xPosition, _yPosition, (int)_imageCountX, _layer);
            
            return AddImage(_xPosition, _yPosition, _imageSizeX, _imageSizeY, _layer, _imageCountX, _imageCountY, _source);
        }

        /// <summary>
        /// Replace existing Image.
        /// </summary>
        /// <param name="_position">One dimensional position</param>
        /// <param name="_imageSizeX">X Size of Sprite</param>
        /// <param name="_imageSizeY">Y Size of Sprite</param>
        /// <param name="_imageCountX">Width image count</param>
        /// <param name="_imageCountY">Height image count</param>
        /// <param name="_layer">Layer to insert Image</param>
        /// <param name="_source">source of Image</param>
        /// <returns></returns>
        public ImageDrawing ReplaceImage(int _position, double _imageSizeX, double _imageSizeY, int _imageCountX, int _imageCountY, int _layer, string _source)
        {
            // Get 2D Position
            ChangeDimensions(_position, (int)_imageCountX, out int xPosition, out int yPosition);

            // Get DrawingGroup
            DrawingGroup dg = GetDrawingGroup(_layer);

            // Get index of DrawingGroup
            int index = dg.Children.IndexOf(ImagesAtLayer[_layer][_position]);

            // Remove ImageDrawing from DrawingGroup and Dictionary
            dg.Children.RemoveAt(index);
            RemoveFromDictionary(xPosition, yPosition, (int)_imageCountX, _layer);

            return AddImage(xPosition, yPosition, _imageSizeX, _imageSizeY, _layer, _imageCountX, _imageCountY, _source);
        }


        /// <summary>
        /// Replace existing Image.
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSizeX">X Size of Sprite</param>
        /// <param name="_imageSizeY">Y Size of Sprite</param>
        /// <param name="_imageCountX">Width image count</param>
        /// <param name="_imageCountY">Height image count</param>
        /// <param name="_dg"><see cref="DrawingGroup"/> to insert image</param>
        /// <param name="_source"></param>
        /// <returns></returns>
        public ImageDrawing ReplaceImage(int _xPosition, int _yPosition, double _imageSizeX, double _imageSizeY, int _imageCountX, int _imageCountY, DrawingGroup _dg, string _source)
        {
            // Get 1D Position
            int position = ChangeDimensions(_xPosition, _yPosition, _imageCountX);

            // Get layer from DrawingGroup for better performance
            int layer = GetLayerFromDrawingGroup(_dg);

            // Get index of DrawingGroup
            int index = _dg.Children.IndexOf(ImagesAtLayer[layer][position]);

            // Remove ImageDrawing from DrawingGroup and Dictionary
            _dg.Children.RemoveAt(index);
            RemoveFromDictionary(_xPosition, _yPosition, (int)_imageCountX, layer);
            ImagesAtLayer[layer].Remove(position);

            return AddImage(_xPosition, _yPosition, _imageSizeX, _imageSizeY, layer, _imageCountX, _imageCountY, _source);
        }

        /// <summary>
        /// Replace existing Image.
        /// </summary>
        /// <param name="_xPosition">X-index of Image</param>
        /// <param name="_yPosition">Y-index of Image</param>
        /// <param name="_imageSizeX">X Size of Sprite</param>
        /// <param name="_imageSizeY">Y Size of Sprite</param>
        /// <param name="_imageCountX">Width image count</param>
        /// <param name="_imageCountY">Height image count</param>
        /// <param name="_dg"><see cref="DrawingGroup"/> to insert image</param>
        /// <param name="_source"></param>
        /// <returns></returns>
        public ImageDrawing ReplaceImage(int _position, double _imageSizeX, double _imageSizeY, int _imageCountX, int _imageCountY, DrawingGroup _dg, string _source)
        {
            // Get 1D Position
            ChangeDimensions(_position, _imageCountX, out int xPosition, out int yPosition);

            // Get layer from DrawingGroup for better performance
            int layer = GetLayerFromDrawingGroup(_dg);

            // Get index of DrawingGroup
            int index = _dg.Children.IndexOf(ImagesAtLayer[layer][_position]);

            // Remove ImageDrawing from DrawingGroup and Dictionary
            _dg.Children.RemoveAt(index);
            RemoveFromDictionary(xPosition, yPosition, (int)_imageCountX, layer);
            ImagesAtLayer[layer].Remove(_position);

            return AddImage(xPosition, yPosition, _imageSizeX, _imageSizeY, layer, _imageCountX, _imageCountY, _source);
        }

        #endregion

        /// <summary>
        /// Create Layer on Field
        /// </summary>
        /// <param name="_x">Amount of Images in width</param>
        /// <param name="_y">Amount of Images in height</param>
        /// <param name="_imageSizeX">X size of Image in Pixels</param>
        /// <param name="_imageSizeY">Y size of Image in Pixels</param>
        /// <returns>Layer as Image</returns>
        public Image CreateLayer(int _x, int _y, int _imageSizeX, int _imageSizeY)
        {
            CurrentPanel.Children.Add(new Border() { Width = _x * _imageSizeX, Height = _y * _imageSizeY, BorderThickness = new System.Windows.Thickness(5), BorderBrush = Brushes.Black });
            ImagesAtLayer.Add(new Dictionary<int, ImageDrawing>());

            Image img = CreateNewImageLayer(out DrawingImage _, out DrawingGroup _dGroup);

            ImageLayer.Add(img);

            CurrentPanel.Children.Add(img);

            SetRect(_x, _y, _imageSizeX, _imageSizeY, _dGroup);

            return img;
        }

        /// <summary>
        /// Create Layer on Field
        /// </summary>
        /// <param name="_x">Amount of Images in width</param>
        /// <param name="_y">Amount of Images in height</param>
        /// <param name="_imageSizeX">X size of Image in Pixels</param>
        /// <param name="_imageSizeY">Y size of Image in Pixels</param>
        /// <param name="_layerIndex">Index of Layer to insert</param>
        /// <returns>Layer as Image</returns>
        public Image CreateLayer(int _x, int _y, int _imageSizeX, int _imageSizeY, int _layerIndex)
        {
            ImagesAtLayer.Insert(_layerIndex, new Dictionary<int, ImageDrawing>());

            Image img = CreateNewImageLayer(out DrawingImage _, out DrawingGroup _dGroup);

            ImageLayer.Insert(_layerIndex, img);

            //CurrentPanel.Children.Insert(_layerIndex, img);
            CurrentPanel.Children.Insert(_layerIndex + 1, img);

            SetRect(_x, _y, _imageSizeX, _imageSizeY, _dGroup);

            return img;
        }

        /// <summary>
        /// Remove Layer
        /// </summary>
        /// <param name="_layer">Index of layer</param>
        public void RemoveLayer(int _layer)
        {
            // Remove from Image
            ImageLayer.RemoveAt(_layer);
            //CurrentPanel.Children.RemoveAt(_layer);
            CurrentPanel.Children.RemoveAt(_layer + 1);

            // Remove from Dictionary
            ImagesAtLayer.RemoveAt(_layer);
        }

        /// <summary>
        /// Remove Layer
        /// </summary>
        /// <param name="_image">Image layer</param>
        public void RemoveLayer(Image _image)
        {
            int index = ImageLayer.IndexOf(_image);

            // Remove from Image
            ImageLayer.Remove(_image);
            CurrentPanel.Children.Remove(_image);

            // Remove from Dictionary
            ImagesAtLayer.RemoveAt(index);
        }

        /// <summary>
        /// Clear all existing layer
        /// </summary>
        public void ClearLayer()
        {
            // clear Images
            ImageLayer.Clear();
            CurrentPanel.Children.Clear();

            // clear Dictionary
            ImagesAtLayer.Clear();
        }

        /// <summary>
        /// Fill Layer with Placeholder Image
        /// </summary>
        /// <param name="_sizeX">Amount of Images on X-Axis</param>
        /// <param name="_sizeY">Amount of Images on Y-Axis</param>
        /// <param name="_imageSizeX">X size of Images in Pixel</param>
        /// <param name="_imageSizeY">Y size of Images in Pixel</param>
        /// <param name="_layer">Layerindex to insert Images</param>
        private void SetRect(int _sizeX, int _sizeY, int _imageSizeX, int _imageSizeY, int _layer)
        {

            for (int x = 0; x < _sizeX; x = x++)
            {
                for (int y = 0; y < _sizeY; y = y++)
                {
                    AddImage(x, y, _imageSizeX, _imageSizeY, _sizeX, _sizeY, _layer);
                }
            }
        }

        /// <summary>
        /// Fill Layer with Placeholder Image
        /// </summary>
        /// <param name="_sizeX">Amount of Images on X-Axis</param>
        /// <param name="_sizeY">Amount of Images on Y-Axis</param>
        /// <param name="_imageSizeX">X size of Images in Pixel</param>
        /// <param name="_imageSizeY">Y size of Images in Pixel</param>
        /// <param name="_dg">Drawinggroup to insert Images</param>
        private void SetRect(int _sizeX, int _sizeY, int _imageSizeX, int _imageSizeY, DrawingGroup _dg)
        {

            for (int x = 0; x < _sizeX; x++)
            {
                for (int y = 0; y < _sizeY; y++)
                {
                    AddImage(x, y, _imageSizeX, _imageSizeY, _sizeX, _sizeY, _dg);
                }
            }
        }

        /// <summary>
        /// Get first items Border. This only works if the first Item in <see cref="CurrentPanel"/> is an instance of <see cref="Border"/>.
        /// </summary>
        /// <returns>An instance of <see cref="Border"/>. If there is no <see cref="Border"/> return <see cref="null"/></returns>
        public Border GetBorder() { return CurrentPanel.Children[0] as Border; }

        /// <summary>
        /// Get Drawing Image
        /// </summary>
        /// <param name="_layer">Index of Layer</param>
        /// <returns>Drawing Image</returns>
        //public DrawingImage GetDrawingImage(int _layer) { return ((Image)CurrentPanel.Children[_layer]).Source as DrawingImage; }
        public DrawingImage GetDrawingImage(int _layer) { return ((Image)CurrentPanel.Children[_layer + 1]).Source as DrawingImage; }

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
        //public DrawingGroup GetDrawingGroup(int _layer) { return ((DrawingImage)((Image)CurrentPanel.Children[_layer]).Source).Drawing as DrawingGroup; }
        public DrawingGroup GetDrawingGroup(int _layer) { return ((DrawingImage)((Image)CurrentPanel.Children[_layer + 1]).Source).Drawing as DrawingGroup; }


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
        /// Get layer from <see cref="DrawingGroup"/>.
        /// </summary>
        /// <param name="_layer">Layer to look for</param>
        /// <returns>-1 if no layer was found</returns>
        public int GetLayerFromDrawingGroup(DrawingGroup _layer)
        {
            for (int i = 0; i < CurrentPanel.Children.Count; i++)
            {
                DrawingImage di = GetDrawingImage(i);
                if (Equals(di.Drawing, _layer))
                    return i;
            }

            return -1;
        }

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

        #region Dictionary
        /// <summary>
        /// Add Item to Dictionary
        /// </summary>
        /// <param name="_xPosition">X-index of image</param>
        /// <param name="_yPosition">Y-index of image</param>
        /// <param name="_xSize">Count of Images in width</param>
        /// <param name="_layer">Layer of image</param>
        /// <param name="_image">Image to add</param>
        private void AddToDictionary(int _xPosition, int _yPosition, int _xSize, int _layer, ImageDrawing _image)
        {
            ImagesAtLayer[_layer].Add(ChangeDimensions(_xPosition, _yPosition, _xSize), _image);
        }

        /// <summary>
        /// Add Item to Dictionary
        /// </summary>
        /// <param name="_position">One dimension Position</param>
        /// <param name="_layer">Layer of image</param>
        /// <param name="_image">Image to add</param>
        private void AddToDictionary(int _position, int _layer, ImageDrawing _image)
        {
            ImagesAtLayer[_layer].Add(_position, _image);
        }

        /// <summary>
        /// Remove Item from Dictionary
        /// </summary>
        /// <param name="_xPosition">X-index of image</param>
        /// <param name="_yPosition">Y-index of image</param>
        /// <param name="_xSize">Count of Images in width</param>
        /// <param name="_layer">Layer of image</param>
        private void RemoveFromDictionary(int _xPosition, int _yPosition, int _xSize, int _layer)
        {
            ImagesAtLayer[_layer].Remove(ChangeDimensions(_xPosition, _yPosition, _xSize));
        }

        /// <summary>
        /// Remove Item from Dictionary
        /// </summary>
        /// <param name="_position">One dimension Position</param>
        /// <param name="_layer">Layer of image</param>
        private void RemoveFromDictionary(int _position, int _layer)
        {
            ImagesAtLayer[_layer].Remove(_position);
        }
        #endregion

        /// <summary>
        /// Convert a 1-Dimensional Position into a 2-Dimensional Position.
        /// </summary>
        /// <param name="_position">One Dimensional Position</param>
        /// <param name="_xSize">Count of images in width</param>
        /// <param name="_xPosition">2-Dimensional X-Position</param>
        /// <param name="_yPosition">2-Dimensional Y-Position</param>
        public void ChangeDimensions(int _position, int _xSize, out int _xPosition, out int _yPosition)
        {
            _yPosition = _position / _xSize;
            _xPosition = _position % _xSize;
        }

        /// <summary>
        /// Convert a 2-Dimensional Position into a 1-Dimensional Position.
        /// </summary>
        /// <param name="_xPosition">Index of 2-Dimensional X-Position</param>
        /// <param name="_yPosition">Index of 2-Dimensional Y-Position</param>
        /// <param name="_xSize">Count of images in width</param>
        /// <returns></returns>
        public int ChangeDimensions(int _xPosition, int _yPosition, int _xSize)
        {
            int toReturn = _yPosition * _xSize;
            toReturn += _xPosition;

            return toReturn;
        }
    }
}
