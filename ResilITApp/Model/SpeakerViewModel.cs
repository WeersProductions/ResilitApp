using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using ResilITApp.Views;
using Rg.Plugins.Popup.Extensions;

namespace ResilITApp.Model
{
    public class SpeakerViewModel : ContentPage
    {
        private Command<object> readMoreCommand;

        private ObservableCollection<JSONSpeakerInfoModel> speakers;

        public ObservableCollection<JSONSpeakerInfoModel> Speakers
        {
            get {
                return speakers;
            }
            set {
                speakers = value;
            }
        }

        public Command<object> ReadMoreCommand
        {
            get { return readMoreCommand; }
            set { readMoreCommand = value; }
        }

        private Dictionary<string, JSONSpeakerInfoModel> idSpeakers;

        public SpeakerViewModel()
        {
            readMoreCommand = new Command<object>(NavigateToReadMoreContent);
            Speakers = new ObservableCollection<JSONSpeakerInfoModel>();
            _ = GetSpeakers();
        }

        private async void NavigateToReadMoreContent(object obj)
        {
            await Navigation.PushPopupAsync(new SpeakerPage() { BindingContext = obj });
        }

        internal void AddSpeakersFromIds(List<string> ids)
        {
            foreach (string speakerId in ids)
            {
                JSONSpeakerInfoModel speaker;
                if (idSpeakers.TryGetValue(speakerId, out speaker))
                {
                    Speakers.Add(speaker);
                }
            }
        }

        internal async Task GetSpeakers()
        {  
            idSpeakers = new Dictionary<string, JSONSpeakerInfoModel>();
            string speakersJSON = await Control.Speakers.GetSpeakersJSON();
            JSONSpeakerModel result = JsonConvert.DeserializeObject<JSONSpeakerModel>(speakersJSON);
            foreach (JSONSpeakerInfoModel speaker in result.speakers)
            {
                speaker.presenter = false;
                idSpeakers.Add(speaker.id, speaker);
            }

            AddSpeakersFromIds(result.speakerids.session1);
            AddSpeakersFromIds(result.speakerids.session2);
            AddSpeakersFromIds(result.speakerids.session3);

            List<string> presenterIds = new List<string>();
            foreach(JSONSpeakerInfoModel presenter in result.presenters)
            {
                presenter.presenter = true;
                idSpeakers.Add(presenter.id, presenter);
                presenterIds.Add(presenter.id);
            }
            AddSpeakersFromIds(presenterIds);
        }
    }
}
