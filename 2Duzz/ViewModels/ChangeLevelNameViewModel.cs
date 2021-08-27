using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2Duzz.ViewModels
{
    public class ChangeLevelNameViewModel : BaseViewModel
    {
        public ChangeLevelNameViewModel()
        {
            m_LevelNameText = "";
        }

        private string m_LevelNameText;
        public string LevelNameText
        {
            get => m_LevelNameText;
            set
            {
                SetProperty(ref m_LevelNameText, value);
                NotifyOfPropertyChange(nameof(CanClickOkButton));
            }
        }

        #region Buttons
        private ICommand m_ClickOk;
        public ICommand ClickOk
        {
            get => m_ClickOk;
            set
            {
                SetProperty(ref m_ClickOk, value);
            }
        }

        private ICommand m_ClickCancel;
        public ICommand ClickCancel
        {
            get => m_ClickCancel;
            set
            {
                SetProperty(ref m_ClickCancel, value);
            }
        }


        public bool CanClickOkButton
        {
            get => CanClick();
        }

        private bool CanClick()
        {
            return !string.IsNullOrEmpty(LevelNameText);
        }
    #endregion
    }

}
