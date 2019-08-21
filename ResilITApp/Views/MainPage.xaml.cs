using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace ResilITApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            InitalizeHamburger();
            InitializeMenuItems();
        }

        private void InitializeMenuItems()
        {
            List<string> list = new List<string>();

            if(!Login.Instance.IsLoggedIn)
            {
                list.Add("Login");
            }
            list.Add("General");
            if(Login.Instance.IsLoggedIn)
            {
                list.Add("Favorites");
                list.Add("Logout");
            }

            listView.ItemsSource = list;
        }

        private void InitalizeHamburger()
        {
            hamburgerButton.BackgroundColor = Color.FromHex("#00a0e1");

            hamburgerButton.HeightRequest = 40;
            hamburgerButton.WidthRequest = 40;
            hamburgerButton.Text = "H";
            hamburgerButton.FontSize = 16;

            if (Device.RuntimePlatform == Device.iOS && Device.Idiom == TargetIdiom.Tablet)
            {
                //headingGrid.Padding = new Thickness(10, 10, 0, 10);
            }
            if (Device.RuntimePlatform == Device.Android)
            {
                if (Device.Idiom == TargetIdiom.Tablet)
                    hamburgerButton.WidthRequest = 60;
                hamburgerButton.FontFamily = "navigation.ttf#navigation"; Console.WriteLine();
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                hamburgerButton.FontSize = 21;
                hamburgerButton.Text = "\xE700";
                hamburgerButton.FontFamily = "Segoe MDL2 Assets";
                hamburgerButton.HeightRequest = 50;
                hamburgerButton.WidthRequest = 50;
            }
            else
            {
                hamburgerButton.ImageSource = (FileImageSource)ImageSource.FromFile("hamburger_icon.png");
                hamburgerButton.WidthRequest = 50;
                hamburgerButton.HeightRequest = 50;
            }
            hamburgerButton.TextColor = Color.White;
        }

        protected override void OnAppearing()
        {
            appContent.Content = Activator.CreateInstance<TimeTable>();
        }

        private void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            InitializeMenuItems();
            navigationDrawer.ToggleDrawer();
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem == null)
            {
                return;
            }
            // TODO: set based on the content afterwards, since based on popups different pages are shown.
            headerLabel.Text = e.SelectedItem.ToString();

            bool doToggleDrawer = true;

            if(e.SelectedItem.ToString() == "Login")
            {
                appContent.Content = Activator.CreateInstance<LoginPage>();
            }
            else if (e.SelectedItem.ToString() == "Logout")
            {
                bool logout = await DisplayAlert("Logout", "Are you sure you want to logout?", "logout", "cancel");
                if(logout)
                {
                    Login.Instance.DoLogout();
                    appContent.Content = Activator.CreateInstance<TimeTable>();
                }
                else
                {
                    listView.SelectedItem = null;
                    doToggleDrawer = false;
                }
            } else
            {
                // TODO: show favorites or general.
                appContent.Content = Activator.CreateInstance<TimeTable>();
            }

            if(doToggleDrawer)
            {
                navigationDrawer.ToggleDrawer();
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (Device.RuntimePlatform == Device.iOS)
            {
                headerLabel.WidthRequest = width - 100;
            }
        }
    }
}
