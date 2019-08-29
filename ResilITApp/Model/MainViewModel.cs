using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ResilITApp.Control;

namespace ResilITApp.Model
{
    public class MainViewModel : INotifyPropertyChanged, IUserObserver
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string firstname;
        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
                NotifyPropertyChanged("Fullname");
            }
        }

        private string lastname;
        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
                NotifyPropertyChanged("Fullname");
            }
        }

        public string Fullname
        {
            get
            {
                return string.Format("{0} {1}", Firstname, Lastname);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnUserReceived(UserModel user)
        {
            Firstname = user.firstname;
            Lastname = user.surname;
        }

        public void OnUserLost()
        {
            Firstname = "";
            Lastname = "";
        }

        public MainViewModel()
        {
            Login.Instance.AddUserObserver(this);
        }
    }
}
