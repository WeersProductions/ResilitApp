using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResilITApp.Control;
using ResilITApp.Model;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace ResilITApp
{
    public partial class SchedulePopup : PopupPage
    {
        public Talk Appointment;
        private bool _enrolled;

        public SchedulePopup(object appointment)
        {
            Appointment = appointment as Talk;
            InitializeComponent();
            EventName.Text = Appointment.EventName;
            EventDescription.Text = Appointment.Description;
            EventTime.Text = Appointment.From.ToShortTimeString() + " - " + Appointment.To.ToShortTimeString();
            _ = SetChecked();
            iconButton.Clicked += OnFavorite;
            _ = Init();
            EnrollButton.Clicked += OnEnroll;
        }

        private async Task Init()
        {
            EnrollButton.IsVisible = false;
            bool canEnroll = await Enroll.CanEnroll();
            if(!canEnroll)
            {
                return;
            }
            JSONEnrolled enrolled = await Enroll.EnrolledTalk(Appointment.Id);
            if(!enrolled.success)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Could not get your talk information.", "OK");
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                return;
            }
            _enrolled = enrolled.enrolled;
            EnrollButton.Text = _enrolled ? "Unenroll" : "Enroll";
            EnrollButton.IsVisible = canEnroll;
        }

        private async Task SetChecked()
        {
            iconButton.IsEnabled = false;
            iconButton.IsChecked = await Favorites.IsFavorite(Appointment);
            iconButton.IsEnabled = true;
        }

        private async void OnEnroll(object sender, EventArgs args)
        {
            EnrollButton.IsEnabled = false;
            if(!Login.Instance.IsLoggedIn)
            {
                await Application.Current.MainPage.DisplayAlert("Please login!", "Login to enroll for a talk.", "OK");
                EnrollButton.IsEnabled = true;
                return;
            }

            if(_enrolled)
            {
                JSONEnroll jsonEnroll = await Enroll.UnEnrollTalk(Appointment.Id);
                if(!jsonEnroll.success)
                {
                    await Application.Current.MainPage.DisplayAlert("Failed.", "Unenrollment did not succeed. Please try again.", "OK");
                    EnrollButton.IsEnabled = true;
                    return;
                }
            }
            else
            {
                JSONEnroll jsonEnroll = await Enroll.EnrollTalk(Appointment.Id);
                if (!jsonEnroll.success)
                {
                    await Application.Current.MainPage.DisplayAlert("Failed.", "Enrollment did not succeed. Please try again.", "OK");
                    EnrollButton.IsEnabled = true;
                    return;
                }
            }
            // It worked!
            _enrolled = !_enrolled;
            EnrollButton.Text = _enrolled ? "Unenroll" : "Enroll";
            EnrollButton.IsEnabled = true;
        }

        private async void OnFavorite(object sender, EventArgs args)
        {
            if(!Login.Instance.IsLoggedIn)
            {
                await Application.Current.MainPage.DisplayAlert("Please login!", "Login to make use of favorites.", "OK");
                return;
            }
            if(iconButton.IsChecked)
            {
                await Favorites.AddFavorite(Appointment);
            }
            else
            {
                await Favorites.RemoveFavorite(Appointment);
            }
        }
    }
}
