using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _2Duzz.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Constructor of MV. This is called AFTER <see cref="MainWindow.MainWindow"/> so we can't set command there.
        /// </summary>
        public MainViewModel()
        {
            HeaderNewClickCommand = new RelayCommand((e) => ExecuteHeaderNewClick("MV Constructor New"));
            HeaderOpenClickCommand = new RelayCommand((e) => ExecuteHeaderOpenClick("MV Constructor Open"));
            HeaderSaveClickCommand = new RelayCommand((e) => ExecuteHeaderSaveClick("MV Constructor Save"));
            HeaderSaveAsClickCommand = new RelayCommand((e) => ExecuteHeaderSaveAsClick("MV Constructor SaveAs"));
            HeaderCloseClickCommand = new RelayCommand((e) => ExecuteHeaderCloseClick("MV Constructor Close"));

            WPScale = 1;
            WPWidth = 1200;

            ImageSizeX = 600;
            ImageSizeY = 600;

        }

        #region Statusbar2
        private object m_StatusBarContent;
        public object StatusBarContent
        {
            get => m_StatusBarContent;
            set
            {
                SetProperty(ref m_StatusBarContent, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }

        private object m_StatusBarScale;
        public object StatusBarScale
        {
            get => m_StatusBarScale;
            set
            {
                SetProperty(ref m_StatusBarScale, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }
        #endregion


        #region KeyBinding
        #region Header New Click
        /// <summary>
        /// Header New Click command
        /// </summary>
        public ICommand HeaderNewClickCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Header New Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderNewClick(object _parameter)
        {
            MessageBox.Show(_parameter.ToString());
        }
        #endregion

        #region Header Open Click
        /// <summary>
        /// Header Open Click command
        /// </summary>
        public ICommand HeaderOpenClickCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Header Open Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderOpenClick(object _parameter)
        {
            MessageBox.Show(_parameter.ToString());
        }
        #endregion

        #region Header Save Click
        /// <summary>
        /// Header Save Click command
        /// </summary>
        public ICommand HeaderSaveClickCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Header Save Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderSaveClick(object _parameter)
        {
            MessageBox.Show(_parameter.ToString());
        }
        #endregion

        #region Header SaveAs Click
        /// <summary>
        /// Header SaveAs Click command
        /// </summary>
        public ICommand HeaderSaveAsClickCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Header SaveAs Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderSaveAsClick(object _parameter)
        {
            MessageBox.Show(_parameter.ToString());
        }
        #endregion

        #region Header Close Click
        /// <summary>
        /// Header Close Click command
        /// </summary>
        public ICommand HeaderCloseClickCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Header Close Click Execution method
        /// </summary>
        /// <param name="_parameter"></param>
        private void ExecuteHeaderCloseClick(object _parameter)
        {
            MessageBox.Show(_parameter.ToString());
        }
        #endregion
        #endregion

        #region MouseBinding
        #endregion


        #region Wrappanel
        private double m_WPScale;
        public double WPScale
        {
            get => m_WPScale;
            set
            {
                SetProperty(ref m_WPScale, value);
                m_StatusBarScale = m_WPScale;
                NotifyOfPropertyChange(nameof(StatusBarScale));
            }
        }

        private double m_WPWidth;
        public double WPWidth
        {
            get => m_WPWidth;
            set
            {
                SetProperty(ref m_WPWidth, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }
        #endregion

        #region Image Size
        private double m_ImageSizeX;
        public double ImageSizeX
        {
            get => m_ImageSizeX;
            set
            {
                SetProperty(ref m_ImageSizeX, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }

        private double m_ImageSizeY;
        public double ImageSizeY
        {
            get => m_ImageSizeY;
            set
            {
                SetProperty(ref m_ImageSizeY, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }
        #endregion
    }
}
