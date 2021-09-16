using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
namespace _2Duzz.Helper
{
    public class LayerManager
    {
        #region Constructor
        private static LayerManager m_Instance;
        public static LayerManager Get
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new LayerManager();
                return m_Instance;
            }
        }

        private LayerManager()
        {
        }

        public void Init(ListView _list)
        {
            m_CurrentList = _list;
        }
        #endregion

        #region List Variables
        ListView m_CurrentList;
        public ListView CurrentList { get => m_CurrentList; }

        public int CurrentSelectedIndex { get => CurrentList.SelectedIndex; set => CurrentList.SelectedIndex = value; }
        public int PreviousIndex { get => CurrentList.SelectedIndex - 1; }
        public int NextIndex { get => CurrentList.SelectedIndex + 1; }

        public ListViewItem CurrentSelectedItem { get => CurrentList.Items[CurrentSelectedIndex] as ListViewItem; }
        public ListViewItem PreviousItem { get => CurrentList.Items[PreviousIndex] as ListViewItem; }
        public ListViewItem NextItem { get => CurrentList.Items[NextIndex] as ListViewItem; }
        #endregion

        #region Brushes
        private Brush m_VisibleLayerBrush;
        public Brush VisibleLayerBrush
        {
            get
            {
                if (m_VisibleLayerBrush == null)
                {
                    BrushConverter converter = new System.Windows.Media.BrushConverter();
                    m_VisibleLayerBrush = (Brush)converter.ConvertFrom("#00FFFFFF");
                }

                return m_VisibleLayerBrush;
            }
        }
        private Brush m_HiddenLayerBrush;
        public Brush HiddenLayerBrush
        {
            get
            {
                if (m_HiddenLayerBrush == null)
                {
                    BrushConverter converter = new System.Windows.Media.BrushConverter();
                    m_HiddenLayerBrush = (Brush)converter.ConvertFrom("#780052FF");
                }

                return m_HiddenLayerBrush;
            }
        }
        #endregion

        #region Add Layer
        /// <summary>
        /// Add Layer
        /// </summary>
        /// <returns>The zero-based index at which the object is added or -1 if the item cannot be added.</returns>
        public int AddLayer(bool _select = true)
        {
            ListViewItem item = PrepareListViewItem();
            int tr = CurrentList.Items.Add(item);

            if (_select)
                CurrentList.SelectedIndex = tr;

            return tr;
        }
        /// <summary>
        /// Add Layer
        /// </summary>
        /// <param name="_layerName">Name of layer</param>
        /// <returns>The zero-based index at which the object is added or -1 if the item cannot be added.</returns>
        public int AddLayer(string _layerName, bool _select = true)
        {
            ListViewItem item = PrepareListViewItem(_layerName);
            int tr = CurrentList.Items.Add(item);

            if (_select)
                CurrentList.SelectedIndex = tr;

            return tr;
        }


        /// <summary>
        /// Add Layer
        /// </summary>
        /// <param name="_index">Index of new layer</param>
        public void AddLayer(int _index, bool _select = true)
        {
            ListViewItem item = PrepareListViewItem();
            CurrentList.Items.Insert(_index, item);

            if (_select)
                CurrentList.SelectedIndex = _index;
        }
        /// <summary>
        /// Add Layer
        /// </summary>
        /// <param name="_index">Index of new layer</param>
        /// <param name="_layerName">Name of layer</param>
        public void AddLayer(int _index, string _layerName, bool _select = true)
        {
            ListViewItem item = PrepareListViewItem(_layerName);
            CurrentList.Items.Insert(_index, item);

            if (_select)
                CurrentList.SelectedIndex = _index;
        }
        #endregion

        #region Remove Layer
        /// <summary>
        /// Remove Layer at index
        /// </summary>
        /// <param name="_index">Index of layer to remove</param>
        public void RemoveLayer(int _index)
        {
            CurrentList.Items.RemoveAt(_index);
        }
        /// <summary>
        /// Remove Layer via item
        /// </summary>
        /// /// <param name="_item">object to remove</param>
        /// <returns>true if item was removed; else false</returns>
        public bool RemoveLayer(object _item)
        {
            int items = CurrentList.Items.Count;
            CurrentList.Items.Remove(_item);

            return items != CurrentList.Items.Count;
        }
        /// <summary>
        /// Remove current selected Layer. See <see cref="CurrentSelectedIndex"/>.
        /// </summary>
        /// <param name="_index">Index of layer to remove</param>
        public bool RemoveLayer()
        {
            if (CurrentSelectedIndex < 0)
                return false;
            CurrentList.Items.RemoveAt(CurrentSelectedIndex);
            return true;
        }

        public void ClearList()
        {
            CurrentList.Items.Clear();
        }
        #endregion

        private ListViewItem PrepareListViewItem(object _content = null)
        {
            ListViewItem tr = new ListViewItem();
            tr.Content = _content == null ? "New Layer" : _content;
            tr.Background = VisibleLayerBrush;
            tr.ContextMenu = PrepareContextMenu();
            return tr;
        }
        private ListViewItem PrepareListViewItem()
        {
            ListViewItem tr = new ListViewItem();
            tr.Content = "New Layer";
            tr.Background = VisibleLayerBrush;
            tr.ContextMenu = PrepareContextMenu();
            return tr;
        }

        private ContextMenu PrepareContextMenu()
        {
            ContextMenu menu = new ContextMenu();

            // Rename
            MenuItem child = new MenuItem();
            child.Header = "Rename";
            child.Click += Rename_Click;
            menu.Items.Add(child);

            // Toggle Visibility
            MenuItem childVisibility = new MenuItem();
            childVisibility.Header = "Toggle Visibility";
            childVisibility.Click += ToggleVisibility_Click;
            menu.Items.Add(childVisibility);

            return menu;
        }

        public event Delegates.OnRenamingLayerEventHandler RenameLayer;
        private void Rename_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WindowsXAML.RenameLayerWindow w = new WindowsXAML.RenameLayerWindow(CurrentSelectedItem.Content as string);
            bool? result = w.ShowDialog();

            if (result == true)
            {
                string oldName = CurrentSelectedItem.Content as string;
                CurrentSelectedItem.Content = w.NewLayerName;
                if (RenameLayer != null)
                    RenameLayer(CurrentList, CurrentSelectedIndex, oldName, w.NewLayerName);
            }
        }

        public event Delegates.OnSwitchVisibilityEventHandler ChangeVisibility;
        private void ToggleVisibility_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Image currentImage = ImageDrawingHelper.Get.ImageLayer[CurrentSelectedIndex];
            System.Windows.Visibility visibilityStatus = currentImage.Visibility;
            switch (visibilityStatus)
            {
                case System.Windows.Visibility.Collapsed:
                    currentImage.Visibility = System.Windows.Visibility.Hidden;
                    break;

                case System.Windows.Visibility.Hidden:
                    currentImage.Visibility = System.Windows.Visibility.Visible;
                    break;

                case System.Windows.Visibility.Visible:
                    currentImage.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }

            // Coloring Brush
            if(currentImage.Visibility == System.Windows.Visibility.Visible)
                CurrentSelectedItem.Background = VisibleLayerBrush;
            else
                CurrentSelectedItem.Background = HiddenLayerBrush;

            if (ChangeVisibility != null)
                ChangeVisibility(CurrentList, CurrentSelectedIndex, visibilityStatus, currentImage.Visibility);
        }
    }
}
