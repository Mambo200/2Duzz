using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _2Duzz.Helper
{
    public class PanelHelper
    {
        #region Constructor
        private static PanelHelper m_Instance;
        public static PanelHelper Get
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new PanelHelper();
                return m_Instance;
            }
        }

        private PanelHelper()
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

        public List<Panel> Panels;
        public Panel CurrentPanel { get; private set; }

        public int ChildrenCount { get => CurrentPanel.Children.Count; }

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


    }
}
