using System;
using System.Collections.Generic;
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
        }
    }
}
