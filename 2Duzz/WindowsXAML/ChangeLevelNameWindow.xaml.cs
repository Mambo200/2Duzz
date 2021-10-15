using _2Duzz.ViewModels;
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
    /// Interaktionslogik für ChangeLevelNameWindow.xaml
    /// </summary>
    public partial class ChangeLevelNameWindow : Window
    {
        public ChangeLevelNameViewModel GetMainViewModel { get => this.DataContext as ChangeLevelNameViewModel; }
        public string LevelName { get; private set; }
        public ChangeLevelNameWindow(string _levelName)
        {
            InitializeComponent();

            LevelName = _levelName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetMainViewModel.LevelNameText = LevelName;

            GetMainViewModel.ClickOk = new RelayCommand((o) => ClickOkButton(Button_Ok));
            GetMainViewModel.ClickCancel = new RelayCommand((o) => ClickCancelButton(Button_Cancel));
        }

        #region MVVM Commands
        private void ClickOkButton(object _args)
        {
            LevelName = GetMainViewModel.LevelNameText;
            DialogResult = true;
            this.Close();
        }
        private void ClickCancelButton(object _args)
        {
            DialogResult = false;
            this.Close();
        }
        #endregion
    }
}
