using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskbar
{
    public class Settings : INotifyPropertyChanged
    {
        /* CPU Value */
        private bool _headings_enabled = false;

        public bool headings_enabled
        {
            get { return _headings_enabled; }
            set
            {
                _headings_enabled = value;
                OnPropertyChanged("headings_enabled");
            }
        }

        /* Value change listener */
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
