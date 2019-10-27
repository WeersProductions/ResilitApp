using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ResilITApp
{
    public partial class TimeTable : ContentView
    {
        public TimeTable()
        {
            InitializeComponent();

            var bindable = (ScheduleViewModel)SwitchView.BindingContext;
            SwitchView.StateChanged += bindable.ChangeSchedule;
        }
    }
}
