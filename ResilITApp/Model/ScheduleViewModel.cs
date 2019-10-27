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
using System.Runtime.CompilerServices;
using Syncfusion.XForms.Buttons;
using ResilITApp.Control;

namespace ResilITApp
{
    public class ScheduleViewModel : INotifyPropertyChanged
    {
        public static bool ShowFavoritesOnly;
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

        private ScheduleView scheduleType = ScheduleView.TimelineView; //DayView
        public ScheduleView ScheduleType
        {
            get
            {
                return scheduleType;
            }
            set
            {
                scheduleType = value;
                NotifyPropertyChanged();
                Application.Current.Properties["ScheduleType"] = (int)value;
                ScheduleSwitchChecked = value.Equals(ScheduleView.DayView);
            }
        }

        private bool scheduleSwitchChecked;
        public bool ScheduleSwitchChecked
        {
            get
            {
                return scheduleSwitchChecked;
            }
            set
            {
                scheduleSwitchChecked = value;
                NotifyPropertyChanged();
            }
        }

        private Color[] ScheduleColors = { Color.Blue, Color.LightBlue, Color.Coral };

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ScheduleCellTapped { get; set; }
        public ICommand ScheduleCellDoubleTapped { get; set; }
        public ICommand ScheduleCellLongPressed { get; set; }
        public ICommand ScheduleVisibleDatesChanged { get; set; }


        public ScheduleViewModel()
        {
            if (Application.Current.Properties.ContainsKey("ScheduleType"))
            {
                int? scheduleTypeTmp = Application.Current.Properties["ScheduleType"] as int?;
                if(scheduleTypeTmp != null)
                {
                    ScheduleType = (ScheduleView)(int)scheduleTypeTmp;
                }
            }

            Talks = new ObservableCollection<Talk>();
            _ = GetTalks();
            ScheduleCellTapped = new Command<CellTappedEventArgs>(CellTapped);
            ScheduleCellDoubleTapped = new Command<CellTappedEventArgs>(DoubleTapped);
            ScheduleCellLongPressed = new Command<CellTappedEventArgs>(LongPressed);
            ScheduleVisibleDatesChanged = new Command<VisibleDatesChangedEventArgs>(VisibleDatesChanged);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChangeSchedule(object sender, SwitchStateChangedEventArgs e)
        {
            if(e.NewValue != null)
            {
                ScheduleType = (bool)e.NewValue ? ScheduleView.DayView : ScheduleView.TimelineView;
            }
        }

        private void CellTapped(CellTappedEventArgs args)
        {
            if(args.Appointment != null)
            {
                _ = ShowPopup(args.Appointment);
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

        private async Task ShowPopup(object appointment)
        {
            await Application.Current.MainPage.Navigation.PushPopupAsync(new SchedulePopup(appointment));
        }

        /// <summary>
        /// Get the raw json talk objects.
        /// </summary>
        /// <returns></returns>
        private async Task<List<JSONTalk>> GetJSONTalks()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{ assembly.GetName().Name}.{ TalksFile}");
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                return await FromJSON(json);
            }
        }

        private async Task<List<JSONTalk>> GetJSONTalksFromWeb()
        {
            string json = await GetSchedule.GetScheduleJSON();
            return await FromJSON(json);
        }

        private async Task<List<JSONTalk>> FromJSON(string json)
        {
            JSONTimetable jsonResult = JsonConvert.DeserializeObject<JSONTimetable>(json);
            List<JSONTalk> result = new List<JSONTalk>();
            int index = 0;
            if(ShowFavoritesOnly && Login.Instance.User == null)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "You are not logged in. First login.", "OK");
                return result;
            }
            var favorites = ShowFavoritesOnly ? await Favorites.GetFavorites() : new int[] { };

            foreach (JSONTrack jsonTrack in jsonResult.tracks)
            {
                foreach (JSONTalk jsonTalk in jsonTrack.talks)
                {
                    if(ShowFavoritesOnly && !favorites.Contains(jsonTalk.id))
                    {
                        continue;
                    }
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
