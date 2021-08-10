using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _2Duzz.Helper
{
    public class PanelManager
    {
        #region Constructor
        private static PanelManager m_Instance;
        public static PanelManager Get
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new PanelManager();
                return m_Instance;
            }
        }

        private PanelManager()
        {
            Panels = new List<Panel>();
        }

        private MainWindow mainWindow;
        public void Init(MainWindow _main, Panel _panel)
        {
            mainWindow = _main;
            CurrentPanel = _panel;
        }
        #endregion

        public List<Panel> Panels { get; private set; }
        public Panel CurrentPanel { get; private set; }

        public int ChildrenCount { get => CurrentPanel.Children.Count; }

        /// <summary>
        /// Create Panel at Layer
        /// </summary>
        /// <param name="_layer">Layerindex</param>
        /// <returns>created Panel</returns>
        public Panel CreatePanel(int _layer)
        {
            // Create new Panel
            WrapPanel panel = new WrapPanel();

            // Add new Panel as children of main Panel
            CurrentPanel.Children.Insert(_layer, panel);

            // Add new Panel to list
            Panels.Insert(_layer, panel);

            return panel;
        }

        /// <summary>
        /// Create Panel at latest Layer (top front)
        /// </summary>
        /// <returns>created Panel</returns>
        public Panel CreatePanel()
        {
            // Create new Panel
            WrapPanel panel = new WrapPanel();

            // Add new Panel as children of main Panel
            CurrentPanel.Children.Add(panel);

            // Add new Panel to list
            Panels.Add(panel);

            return panel;
        }

        /// <summary>
        /// Delete Panel at index
        /// </summary>
        /// <param name="_layer">Layerindex to remove</param>
        public void DeletePanel(int _layer)
        {
            // Remove Panel from children of main Panel
            CurrentPanel.Children.RemoveAt(_layer);

            // Remove Panel from List
            Panels.RemoveAt(_layer);
        }

        /// <summary>
        /// Delete top most Panel
        /// </summary>
        public void DeletePanel()
        {
            // Remove Panel from children of main Panel
            CurrentPanel.Children.RemoveAt(CurrentPanel.Children.Count - 1);

            // Remove Panel from List
            Panels.RemoveAt(Panels.Count - 1);
        }

        /// <summary>
        /// Delete Panel
        /// </summary>
        /// <param name="_panel">Panelto remove</param>
        public void DeletePanel(Panel _panel)
        {
            // Remove Panel from children of main Panel
            CurrentPanel.Children.Remove(_panel);

            // Remove Panel from List
            Panels.Remove(_panel);
        }

        /// <summary>
        /// Clear Panels (You may want to use this to clear the map)
        /// </summary>
        public void ClearPanels()
        {
            // Clear all Panels from childen of main Panel
            CurrentPanel.Children.Clear();

            // Clear Panels list
            Panels.Clear();
        }

        /// <summary>
        /// Set size of Grid
        /// </summary>
        /// <param name="_fieldSizeX">size of width</param>
        /// <param name="_fieldSizeY">size of height</param>
        /// <param name="_viewModel">view model</param>
        public void SetFieldSize(double _fieldSizeX, double _fieldSizeY, ViewModels.MainViewModel _viewModel)
        {
            // We do not need to set Height, because height of Grid does not matter to us
            // EDIT: We need to set Height for Grid to have a set height so it can work with it.
            _viewModel.GridContentWidth = _fieldSizeX;
            _viewModel.GridContentHeight = _fieldSizeY;

        }

        /// <summary>
        /// Set size of grid according to Sprite size and level size
        /// </summary>
        /// <param name="_spriteSizeX">width of sprite size</param>
        /// <param name="_spriteSizeY">width of sprite size</param>
        /// <param name="_levelSizeX">width of level size</param>
        /// <param name="_levelSizeY">height of level size</param>
        /// <param name="_viewModel">view model</param>
        public void SetFieldSize(double _spriteSizeX, double _spriteSizeY, int _levelSizeX, int _levelSizeY, ViewModels.MainViewModel _viewModel)
        {
            _viewModel.GridContentWidth = _spriteSizeX * _levelSizeX;
            _viewModel.GridContentHeight = _spriteSizeY * _levelSizeY;
        }
    }
}
