using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ResilITApp.Control;

namespace ResilITApp.Model
{
    public class AppModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshBussy()
        {
            NotifyPropertyChanged("IsBussy");
        }

        public bool IsBussy
        {
            get
            {
                return AppController.IsBussy;
            }
        }

        public AppModel ()
        {
            AppController.AddListener(this);
        }
    }
}
