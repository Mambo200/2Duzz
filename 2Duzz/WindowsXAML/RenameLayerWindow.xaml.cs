using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace _2Duzz.WindowsXAML
{
    /// <summary>
    /// Interaktionslogik für RenameLayerWindow.xaml
    /// </summary>
    public partial class RenameLayerWindow : Window
    {
        private readonly string layername;
        public string NewLayerName { get => TextBox_LayerName.Text; }
        public RenameLayerWindow(string _layerName)
        {
            InitializeComponent();
            layername = _layerName;

            TextBox_LayerName.Text = layername;
            TextBox_LayerName.SelectAll();
            DialogResult = false;
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewLayerName))
                return;

            DialogResult = true;
            Close();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click_OK(sender, e);
            else if (e.Key == Key.Escape)
                Button_Click_Cancel(sender, e);
        }
    }
}
