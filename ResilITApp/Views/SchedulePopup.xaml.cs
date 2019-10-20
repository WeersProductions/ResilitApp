using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResilITApp.Control;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace ResilITApp
{
    public partial class SchedulePopup : PopupPage
    {
        public Talk Appointment;

        public SchedulePopup(object appointment)
        {
            Appointment = appointment as Talk;
            InitializeComponent();


            EventName.Text = Appointment.EventName;
            EventDescription.Text = Appointment.SubTitle;
            EventTime.Text = Appointment.From.ToShortTimeString() + " - " + Appointment.To.ToShortTimeString();
            _ = SetChecked();
            iconButton.Clicked += OnFavorite;
        }

        private async Task SetChecked()
        {
            iconButton.IsChecked = await Favorites.IsFavorite(Appointment);
        }

        private async void OnFavorite(object sender, EventArgs args)
        {
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
