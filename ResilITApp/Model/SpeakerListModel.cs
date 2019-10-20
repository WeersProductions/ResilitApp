using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ResilITApp.Model
{
    public class SpeakerListModel : INotifyPropertyChanged
    {
        private string speakerName;
        private string speakerDescription;
        private bool isPressenter;
        private JSONSpeakerInfoModel speakerDetail;

        public string SpeakerName
        {
            get
            {
                return speakerName;
            }
            set
            {
                speakerName = value;
                OnPropertyChanged();
            }
        }

        public string SpeakerDescription
        {
            get
            {
                return speakerDescription;
            }
            set
            {
                speakerDescription = value;
                OnPropertyChanged();
            }
        }

        public bool IsPresenter
        {
            get
            {
                return isPressenter;
            }
            set
            {
                isPressenter = value;
                OnPropertyChanged();
            }
        }

        public JSONSpeakerInfoModel SpeakerDetail
        {
            get
            {
                return speakerDetail;
            }
            set
            {
                speakerDetail = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
