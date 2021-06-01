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

            WPScaleX = 1;
            WPScaleY = 1;
        }

        #region Statusbar
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


        #region Wrappanel Scale
        private double m_WPScaleX;
        public double WPScaleX
        {
            get => m_WPScaleX;
            set
            {
                SetProperty(ref m_WPScaleX, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }

        private double m_WPScaleY;
        public double WPScaleY
        {
            get => m_WPScaleY;
            set
            {
                SetProperty(ref m_WPScaleY, value);
                //NotifyOfPropertyChange(nameof([PROPERTY]));
            }
        }
        #endregion
    }
}
