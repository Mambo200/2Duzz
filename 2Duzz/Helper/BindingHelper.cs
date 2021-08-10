using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace _2Duzz.Helper
{
    public class BindingHelper
    {
        private static BindingHelper m_Instance;
        public static BindingHelper Get
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new BindingHelper();
                return m_Instance;
            }
        }

        public Binding BindingImageSizeWidth;
        public Binding BindingImageSizeHeight;

        public void Init(MainWindow _main)
        {
            // Width Binding
            BindingImageSizeWidth = new Binding("ImageSizeX");
            BindingImageSizeWidth.Source = _main.GetMainViewModel;
            BindingImageSizeWidth.Path = new PropertyPath(nameof(_main.GetMainViewModel.ImageSizeX));
            BindingImageSizeWidth.Mode = BindingMode.OneWay;
            BindingImageSizeWidth.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            // Height Binding
            BindingImageSizeHeight = new Binding("ImageSizeX");
            BindingImageSizeHeight.Source = _main.GetMainViewModel;
            BindingImageSizeHeight.Path = new PropertyPath(nameof(_main.GetMainViewModel.ImageSizeY));
            BindingImageSizeHeight.Mode = BindingMode.OneWay;
            BindingImageSizeHeight.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

        }
    }
}
