using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _2Duzz.ViewModels
{
    public class SplitPicViewModel : BaseViewModel
    {
        public Images.SplitPic Split
        {
            get;
            private set;
        }

        public SplitPicViewModel()
        {

        }

        ~SplitPicViewModel()
        {
            if (Split != null)
                Split.Dispose();
        }

        private ImageSource m_SelectedImageSource;
        public ImageSource SelectedImageSource
        {
            get => m_SelectedImageSource;
            set
            {
                SetProperty(ref m_SelectedImageSource, value);

                if (Split != null)
                    Split.Dispose();

                Split = new Images.SplitPic(RemoveFileAtBeginning(this.SelectedImageSource.ToString()));
                NotifyOfPropertyChange(nameof(Split));
            }
        }

        #region Count
        private string m_splitPixelWidthText;
        public string SplitPixelWidthText
        {
            get => m_splitPixelWidthText;
            set
            {
                SetProperty(ref m_splitPixelWidthText, value);
                NotifyOfPropertyChange(nameof(CountW));
            }
        }

        private string m_splitPixelHeightText;
        public string SplitPixelHeightText
        {
            get => m_splitPixelHeightText;
            set
            {
                SetProperty(ref m_splitPixelHeightText, value);
                NotifyOfPropertyChange(nameof(CountH));
            }
        }

        public int CountW
        {
            get => SplitValueWidth();
        }

        public int CountH
        {
            get => SplitValueHeight();
        }

        private int SplitValueWidth()
        {
            bool work = int.TryParse(SplitPixelWidthText, out int number);
            if (!work)
                return -1;

            if (!Split.CanSplitWidth(number))
                return -1;

            return Split.ImageData.Width / number;
        }

        private int SplitValueHeight()
        {
            bool work = int.TryParse(SplitPixelHeightText, out int number);
            if (!work)
                return -1;

            if (!Split.CanSplitHeight(number))
                return -1;

            return Split.ImageData.Height / number;
        }
        #endregion

        #region Convert Button
        private ICommand m_ConvertButtonPressedCommand;
        public ICommand ConvertButtonPressedCommand
        {
            get => m_ConvertButtonPressedCommand;
            set
            {
                SetProperty(ref m_ConvertButtonPressedCommand, value);
            }
        }

        public bool CanPressConvertButton
        {
            get
            {
                return SplitPixelHeightText != "-1"
                    && SplitPixelWidthText != "-1"
                    && CountW <= 0
                    && CountH <= 0;
            }
        }
        #endregion


        private string RemoveFileAtBeginning(string _text)
        {
            if (_text.ToLower().StartsWith("file:///"))
            {
                return _text.Remove(0, 8);
            }
            else
                return _text;
        }
    }
}
