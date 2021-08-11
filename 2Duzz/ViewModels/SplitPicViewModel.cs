using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _2Duzz.ViewModels
{
    public class SplitPicViewModel : BaseViewModel
    {
        private ImageSource m_SelectedImageSource;
        public ImageSource SelectedImageSource
        {
            get => m_SelectedImageSource;
            set
            {
                SetProperty(ref m_SelectedImageSource, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }

    }
}
