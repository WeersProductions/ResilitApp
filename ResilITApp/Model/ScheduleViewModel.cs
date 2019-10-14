using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using Syncfusion.SfSchedule.XForms;
using Rg.Plugins.Popup.Extensions;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ResilITApp
{
    public class ScheduleViewModel : ContentPage
    {
        private const string TalksFile = "timetable.json";

        private ObservableCollection<Talk> talks;
        public ObservableCollection<Talk> Talks
        {
            get
            {
                return talks;
            }
            set
            {
                talks = value;
            }
        }
        private Color[] ScheduleColors = { Color.Blue, Color.LightBlue, Color.Coral };

        public ICommand ScheduleCellTapped { get; set; }
        public ICommand ScheduleCellDoubleTapped { get; set; }
        public ICommand ScheduleCellLongPressed { get; set; }
        public ICommand ScheduleVisibleDatesChanged { get; set; }

        public ScheduleViewModel()
        {
            Talks = new ObservableCollection<Talk>();
            _ = GetTalks();
            ScheduleCellTapped = new Command<CellTappedEventArgs>(CellTapped);
            ScheduleCellDoubleTapped = new Command<CellTappedEventArgs>(DoubleTapped);
            ScheduleCellLongPressed = new Command<CellTappedEventArgs>(LongPressed);
            ScheduleVisibleDatesChanged = new Command<VisibleDatesChangedEventArgs>(VisibleDatesChanged);
        }

        private void CellTapped(CellTappedEventArgs args)
        {
            if(args.Appointment != null)
            {
                ShowPopup(args.Appointment);
            }
        }

        private void DoubleTapped(CellTappedEventArgs args)
        {
            var selectedDateTime = args.Datetime;
        }

        private void LongPressed(CellTappedEventArgs args)
        {
            var selectedDateTime = args.Datetime;
        }

        private void VisibleDatesChanged(VisibleDatesChangedEventArgs args)
        {
            var visibleDates = args.visibleDates;
        }

        private async void ShowPopup(object appointment)
        {
            await Navigation.PushPopupAsync(new SchedulePopup(appointment));
        }

        /// <summary>
        /// Get the raw json talk objects.
        /// </summary>
        /// <returns></returns>
        private List<JSONTalk> GetJSONTalks()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{ assembly.GetName().Name}.{ TalksFile}");
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                return FromJSON(json);
            }
        }

        private async Task<List<JSONTalk>> GetJSONTalksFromWeb()
        {
            string json = await GetSchedule.GetScheduleJSON();
            return FromJSON(json);
        }

        private List<JSONTalk> FromJSON(string json)
        {
            JSONTimetable jsonResult = JsonConvert.DeserializeObject<JSONTimetable>(json);
            List<JSONTalk> result = new List<JSONTalk>();
            int index = 0;
            foreach (JSONTrack jsonTrack in jsonResult.tracks)
            {
                foreach (JSONTalk jsonTalk in jsonTrack.talks)
                {
                    jsonTalk.color = ScheduleColors[index];
                    result.Add(jsonTalk);
                }
                index += 1;
            }
            return result;
        }

        private async Task GetTalks()
        {
            var result = new ObservableCollection<Talk>();

            List<JSONTalk> jsonTalks = await GetJSONTalksFromWeb();

            foreach (var data in jsonTalks)
            {
                if (!data.enabled)
                {
                    continue;
                }
                DateTime from = Convert.ToDateTime(data.startTime).ToUniversalTime();
                DateTime to = Convert.ToDateTime(data.endTime).ToUniversalTime();
                result.Add(new Talk()
                {
                    Id = data.id,
                    EventName = data.title,
                    From = new DateTime(2019, 11, 26, from.Hour, from.Minute, from.Second),
                    To = new DateTime(2019, 11, 26, to.Hour, to.Minute, to.Second),
                    Color = data.color,
                    SubTitle = data.subTitle
                });
            }

            Talks.Clear();
            foreach (var talk in result)
            {
                Talks.Add(talk);
            }
        }
    }
}
