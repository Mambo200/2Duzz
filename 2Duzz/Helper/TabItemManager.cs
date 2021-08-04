using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media;
using _2Duzz.Tools;

namespace _2Duzz.Helper
{
    public class TabItemManager
    {
        private static Brush NoHighlightColor { get => Brushes.Black; }
        private static Brush HighlightColor { get => Brushes.Red; }

        #region Constructor
        private static TabItemManager m_Instance;
        public static TabItemManager Get
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new TabItemManager();
                return m_Instance;
            }
        }

        private TabItemManager()
        {

        }

        private MainWindow mainWindow;
        public void Init(MainWindow _main, ItemsControl _control)
        {
            mainWindow = _main;
            ItemControl = _control;
        }
        #endregion

        public ItemsControl ItemControl { get; private set; }

        #region Add TabTtem
        /// <summary>
        /// Add <see cref="TabItem"/> to Control
        /// </summary>
        /// <param name="_header">Header of TabItem</param>
        /// <returns></returns>
        public TabItem AddTabItem(object _header)
        {
            TabItem t = PrepareTabItem(_header);
            AddToItemControl(t);

            return t;
        }

        /// <summary>
        /// Add <see cref="TabItem"/> to Control at Index
        /// </summary>
        /// <param name="_header">Header of <see cref="TabItem"/></param>
        /// <param name="_layer">Layer of new <see cref="TabItem"/></param>
        /// <returns></returns>
        public TabItem AddTabItem(object _header, int _layer)
        {
            TabItem t = PrepareTabItem(_header);
            AddToItemControl(t, _layer);

            return t;
        }
        #endregion

        #region Add Image to TabItem
        /// <summary>
        /// Add Item to TabItem
        /// </summary>
        /// <param name="_layer">Position of TabItem in ItemControl</param>
        /// <param name="_path">Path of Image</param>
        /// <returns></returns>
        public Image AddImageToTabItem(int _layer, Uri _path)
        {
            WrapPanel wp = GetWrapPanel(GetTabItem(_layer));

            Image img = PrepareImage(_path);
            wp.Children.Add(PrepareBorder(img));
            //SetTag(img, wp);

            return img;
        }

        /// <summary>
        /// Add Item to TabItem
        /// </summary>
        /// <param name="_layer">TabItem</param>
        /// <param name="_path">Path of Image</param>
        /// <returns></returns>
        public Image AddImageToTabItem(TabItem _layer, Uri _path)
        {
            WrapPanel wp = GetWrapPanel(_layer);

            Image img = PrepareImage(_path);
            wp.Children.Add(PrepareBorder(img));
            //SetTag(img, wp);

            return img;
        }

        /// <summary>
        /// Add Item to TabItem
        /// </summary>
        /// <param name="_header">Header of TabItem in ItemControl</param>
        /// <param name="_path">Path of Image</param>
        /// <returns></returns>
        public Image AddImageToTabItem(object _header, Uri _path)
        {
            WrapPanel wp = GetWrapPanel(GetTabItem(_header));

            Image img = PrepareImage(_path);
            wp.Children.Add(PrepareBorder(img));
            //SetTag(img, wp);

            return img;
        }



        /// <summary>
        /// Add Item to TabItem
        /// </summary>
        /// <param name="_layer">Position of TabItem in ItemControl</param>
        /// <param name="_path">Path of Image</param>
        /// <param name="_leftButtonDown">event called when item is leftclicked</param>
        /// <param name="_rightButtonDown">event called when item is rightclicked</param>
        /// <returns></returns>
        public Image AddImageToTabItem(int _layer, Uri _path, MouseButtonEventHandler _leftButtonDown, MouseButtonEventHandler _rightButtonDown)
        {
            WrapPanel wp = GetWrapPanel(GetTabItem(_layer));

            Image img = PrepareImage(_path, _leftButtonDown, _rightButtonDown);
            //SetTag(img, wp);
            wp.Children.Add(PrepareBorder(img));

            return img;
        }

        /// <summary>
        /// Add Item to TabItem
        /// </summary>
        /// <param name="_layer">TabItem</param>
        /// <param name="_path">Path of Image</param>
        /// <param name="_leftButtonDown">event called when item is leftclicked</param>
        /// <param name="_rightButtonDown">event called when item is rightclicked</param>
        /// <returns></returns>
        public Image AddImageToTabItem(TabItem _layer, Uri _path, MouseButtonEventHandler _leftButtonDown, MouseButtonEventHandler _rightButtonDown)
        {
            WrapPanel wp = GetWrapPanel(_layer);

            Image img = PrepareImage(_path, _leftButtonDown, _rightButtonDown);
            //SetTag(img, wp);
            wp.Children.Add(PrepareBorder(img));

            return img;
        }

        /// <summary>
        /// Add Item to TabItem
        /// </summary>
        /// <param name="_header">Header of TabItem in ItemControl</param>
        /// <param name="_path">Path of Image</param>
        /// <param name="_leftButtonDown">event called when item is leftclicked</param>
        /// <param name="_rightButtonDown">event called when item is rightclicked</param>
        /// <returns></returns>
        public Image AddImageToTabItem(object _header, Uri _path, MouseButtonEventHandler _leftButtonDown, MouseButtonEventHandler _rightButtonDown)
        {
            WrapPanel wp = GetWrapPanel(GetTabItem(_header));

            Image img = PrepareImage(_path, _leftButtonDown, _rightButtonDown);
            //SetTag(img, wp);
            wp.Children.Add(PrepareBorder(img));

            return img;
        }


        [Obsolete("Does not Work with streams so be aware.")]
        /// <summary>
        /// Add Item to TabItem
        /// </summary>
        /// <param name="_layer">Position of TabItem in ItemControl</param>
        /// <param name="_path">Image Stream</param>
        /// <returns></returns>
        public Image AddImageToTabItem(int _layer, System.IO.Stream _path)
        {
            WrapPanel wp = GetWrapPanel(GetTabItem(_layer));

            Image img = PrepareImage(_path);
            wp.Children.Add(img);

            return img;
        }
        #endregion

        #region Preparing Image
        /// <summary>
        /// Prepare <see cref="Image"/>
        /// </summary>
        /// <param name="_path">Path of Image</param>
        /// <returns></returns>
        private Image PrepareImage(Uri _path)
        {
            Image img = new Image();

            img.BeginInit();
            img.Focusable = false;
            img.Source = new BitmapImage(_path);
            img.Height = 50;
            img.Width = 50;
            img.Stretch = System.Windows.Media.Stretch.Uniform;
            img.Tag = _path.AbsoluteUri;
            img.EndInit();

            return img;
        }

        /// <summary>
        /// Prepare <see cref="Image"/>
        /// </summary>
        /// <param name="_path">Path of Image</param>
        /// <param name="_leftButtonDown">event called when item is leftclicked</param>
        /// <param name="_rightButtonDown">event called when item is rightclicked</param>
        /// <returns></returns>
        private Image PrepareImage(Uri _path, MouseButtonEventHandler _leftButtonDown, MouseButtonEventHandler _rightButtonDown)
        {
            Image img = new Image();

            img.BeginInit();
            img.Focusable = false;
            img.Source = new BitmapImage(_path);
            img.Height = 50;
            img.Width = 50;
            img.Stretch = System.Windows.Media.Stretch.Uniform;
            img.MouseLeftButtonDown += _leftButtonDown;
            img.MouseRightButtonDown += _rightButtonDown;
            img.Tag = _path.AbsoluteUri;
            img.EndInit();


            return img;
        }

        [Obsolete("Does not Work with streams so be aware.")]
        /// <summary>
        /// Prepare <see cref="Image"/>
        /// </summary>
        /// <param name="_stream">Path of Image</param>
        /// <returns></returns>
        private Image PrepareImage(System.IO.Stream _stream)
        {
            Image img = new Image();

            img.BeginInit();
            img.Focusable = false;
            img.Source = new BitmapImage() { StreamSource = _stream };
            img.Height = 50;
            img.Width = 50;
            img.Stretch = System.Windows.Media.Stretch.Uniform;
            img.EndInit();

            return img;
        }

        [Obsolete("Does not Work with streams so be aware.")]
        /// <summary>
        /// Prepare <see cref="Image"/>
        /// </summary>
        /// <param name="_stream">Path of Image</param>
        /// <param name="_leftButtonDown">event called when item is leftclicked</param>
        /// <param name="_rightButtonDown">event called when item is rightclicked</param>
        /// <returns></returns>
        private Image PrepareImage(System.IO.Stream _stream, MouseButtonEventHandler _leftButtonDown, MouseButtonEventHandler _rightButtonDown)
        {
            Image img = new Image();

            img.BeginInit();
            img.Focusable = false;
            img.Source = new BitmapImage() { StreamSource = _stream };
            img.Height = 50;
            img.Width = 50;
            img.Stretch = System.Windows.Media.Stretch.Uniform;
            img.MouseLeftButtonDown += _leftButtonDown;
            img.MouseRightButtonDown += _rightButtonDown;
            img.EndInit();

            return img;
        }

        #endregion

        /// <summary>
        /// Preparing a TabItem. Structure:
        /// <see cref="Panel.Children"/> contains Many <see cref="TabItem"/>s. 
        /// <see cref="TabItem.Content"/> contains <see cref="ScrollViewer"/>. 
        /// <see cref="ScrollViewer.Content"/> contains <see cref="WrapPanel"/>. 
        /// <see cref="WrapPanel.Children"/> contains many Sprites
        /// </summary>
        /// <returns></returns>
        private TabItem PrepareTabItem(object _header)
        {
            TabItem t = new TabItem();
            t.Header = _header;
            ScrollViewer sv = new ScrollViewer();
            sv.Content = new WrapPanel();
            t.Content = sv;

            return t;
        }

        /// <summary>
        /// Get <see cref="TabItem"/> from ItemControl
        /// </summary>
        /// <param name="_index">Index of item</param>
        /// <returns></returns>
        public TabItem GetTabItem(int _index)
        {
            return ItemControl.Items[_index] as TabItem;
        }

        /// <summary>
        /// Get <see cref="TabItem"/> from ItemControl via its Header.
        /// </summary>
        /// <param name="_header">Variable of Header</param>
        /// <returns></returns>
        public TabItem GetTabItem(object _header)
        {
            int index = ItemControl.Items.IndexOf(_header);
            return GetTabItem(index);
        }

        /// <summary>
        /// Add Item to ItemControl at last (on Top)
        /// </summary>
        /// <param name="_item">Item to add</param>
        private void AddToItemControl(TabItem _item)
        {
            ItemControl.Items.Add(_item);
        }

        /// <summary>
        /// Add Item to ItemControl at layer
        /// </summary>
        /// <param name="_item">Item to add</param>
        /// <param name="_layer">Index of Layer</param>
        private void AddToItemControl(TabItem _item, int _layer)
        {
            ItemControl.Items.Insert(_layer, _item);
        }

        private WrapPanel GetWrapPanel(TabItem _item) => ((ScrollViewer)_item.Content).Content as WrapPanel;
        private WrapPanel GetWrapPanel(int _itemIndex)
        {
            // Now we have Children of Panel => TabItem
            ContentControl c = (ContentControl)ItemControl.Items[_itemIndex];

            // Now we have Children of TabItem => ScrollViewer
            c = (ContentControl)c.Content;

            // Now we have Children of ScrollViewer => WrapPanel
            WrapPanel w = (WrapPanel)c.Content;

            return w;
        }

        /// <summary>
        /// Prepare Border to add to list later.
        /// </summary>
        /// <param name="_img">Image which will be the child of <see cref="Border"/>.</param>
        /// <returns></returns>
        private Border PrepareBorder(Image _img)
        {
            Border b = new Border();
            b.BorderBrush = NoHighlightColor;
            b.BorderThickness = new Thickness(1);
            b.Child = _img;
            return b;
        }

        private void SetTag(Image img, Panel _panel)
        {
            img.Tag = _panel.Children.Count;
        }

        public Image GetImage(int _tabLayer, int _imageIndex)
        {
            WrapPanel wp = GetWrapPanel(GetTabItem(_tabLayer));
            Border b = wp.Children[_imageIndex] as Border;

            return b.Child as Image;
        }

        public Image GetImage(TabItem _tabItem, int _imageIndex)
        {
            WrapPanel wp = GetWrapPanel(_tabItem);
            Border b = wp.Children[_imageIndex] as Border;

            return b.Child as Image;
        }

        #region (un)highlight
        /// <summary>
        /// Unhighlight Border --> Set Border Color
        /// </summary>
        /// <param name="_border">Border to unhighlght</param>
        public void Unhighlight(Border _border)
        {
            _border.BorderBrush = NoHighlightColor;
        }

        /// <summary>
        /// Unhighlight Border --> Set Border Color. Does only work if Parent of <see cref="Image"/> is <see cref="Border"/>.
        /// </summary>
        /// <param name="_image">Image of which Parent has to be <see cref="Border"/>.</param>
        public void Unhighlight(Image _image)
        {
            if (_image != null 
                && _image.Parent.GetType() == typeof(Border))
                ((Border)(_image.Parent)).BorderBrush = NoHighlightColor;
        }

        /// <summary>
        /// Highlight Border --> Set Border Color
        /// </summary>
        /// <param name="_border">Border to highlght</param>
        public void Highlight(Border _border)
        {
            _border.BorderBrush = HighlightColor;
        }

        /// <summary>
        /// Highlight Border --> Set Border Color. Does only work if Parent of <see cref="Image"/> is <see cref="Border"/>.
        /// </summary>
        /// <param name="_image">Image of which Parent has to be <see cref="Border"/>.</param>
        public void Highlight(Image _image)
        {
            if (_image.Parent.GetType() == typeof(Border))
                ((Border)(_image.Parent)).BorderBrush = HighlightColor;
        }
        #endregion

    }
}
