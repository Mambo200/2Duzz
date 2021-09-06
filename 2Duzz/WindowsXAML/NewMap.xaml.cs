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
using _2Duzz.ViewModels;

namespace _2Duzz.WindowsXAML
{
    /// <summary>
    /// Interaktionslogik für NewMap.xaml
    /// </summary>
    public partial class NewMap : Window
    {
        public NewMapViewModel GetViewModel { get => this.DataContext as NewMapViewModel; }

        /// <summary>Name of level</summary>
        public string LevelName { get => GetViewModel.LevelNameText; }
        /// <summary>Amount of how many sprites are aalligned horizontally</summary>
        public int LevelSizeX { get => int.Parse(GetViewModel.LevelSizeXText); }
        /// <summary>Amount of how many sprites are aalligned vertically</summary>
        public int LevelSizeY { get => int.Parse(GetViewModel.LevelSizeYText); }
        /// <summary>Width of sprites</summary>
        public int SpriteSizeX { get => int.Parse(GetViewModel.SpriteSizeXText); }
        /// <summary>Height of sprites</summary>
        public int SpriteSizeY { get => int.Parse(GetViewModel.SpriteSizeYText); }

        public NewMap()
        {
            InitializeComponent();

            GetViewModel.ClickCreateLevel = new RelayCommand((o) => ClickCreateButton(new object()));
            GetViewModel.ClickCancel = new RelayCommand((o) => ClickCancelButton(new object()));
        }

        private void ClickCreateButton(object _parameter)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ClickCancelButton(object _parameter)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((System.Windows.Controls.Primitives.TextBoxBase)sender).SelectAll();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Create level if user pressed ENTER
            if (e.Key == Key.Enter
                && GetViewModel.CanClickButton)
                GetViewModel.ClickCreateLevel.Execute(this);

            // Close window if user pressed ESCAPE
            else if (e.Key == Key.Escape)
                GetViewModel.ClickCancel.Execute(this);
        }
    }
}
