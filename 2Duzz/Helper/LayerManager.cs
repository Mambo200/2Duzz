using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        ListView m_CurrentList;
        public ListView CurrentList { get => m_CurrentList; }
        public void Init(ListView _list)
        {
            m_CurrentList = _list;
        }
        #endregion

        #region Add Layer
        /// <summary>
        /// Add Layer
        /// </summary>
        /// <returns>The zero-based index at which the object is added or -1 if the item cannot be added.</returns>
        public int AddLayer()
        {
            ListViewItem item = PrepareListViewItem();
            return CurrentList.Items.Add(item);
        }

        /// <summary>
        /// Add Layer
        /// </summary>
        /// <param name="_index">Index of new layer</param>
        public void AddLayer(int _index)
        {
            ListViewItem item = PrepareListViewItem();
            CurrentList.Items.Insert(_index, item);
        }
        #endregion

        #region Remove Layer
        /// <summary>
        /// Remove Layer
        /// </summary>
        /// <param name="_index">Index of layer to remove</param>
        public void RemoveLayer(int _index)
        {
            CurrentList.Items.RemoveAt(_index);
        }

        /// <summary>
        /// Remove Layer
        /// </summary>
        /// /// <param name="_item">object to remove</param>
        /// <returns>true if item was removed; else false</returns>
        public bool RemoveLayer(object _item)
        {
            int items = CurrentList.Items.Count;
            CurrentList.Items.Remove(_item);

            return items != CurrentList.Items.Count;
        }
        #endregion

        private ListViewItem PrepareListViewItem(object _content = null)
        {
            ListViewItem tr = new ListViewItem();
            tr.Content = _content == null ? "New Layer" : _content;
            tr.ContextMenu = PrepareContextMenu();
            return tr;
        }
        private ListViewItem PrepareListViewItem()
        {
            ListViewItem tr = new ListViewItem();
            tr.Content = "New Layer";
            tr.ContextMenu = PrepareContextMenu();
            return tr;
        }

        private ContextMenu PrepareContextMenu()
        {
            ContextMenu menu = new ContextMenu();

            MenuItem child = new MenuItem();
            child.Header = "Rename";
            child.Click += Rename_Click;

            menu.Items.Add(child);

            return menu;
        }

        private void Rename_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Rename", "Rename");
        }
    }
}
