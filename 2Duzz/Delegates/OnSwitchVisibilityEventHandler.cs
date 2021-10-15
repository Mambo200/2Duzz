using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace _2Duzz.Delegates
{
    public delegate void OnSwitchVisibilityEventHandler(ItemsControl _control, int _index, Visibility _oldVisibility, Visibility _newVisibility);
}
