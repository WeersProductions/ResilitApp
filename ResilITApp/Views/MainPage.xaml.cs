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
            List<string> list = new List<string>
            {
                "General",
                "Favorites",
                "Logout"
            };
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

        private void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            headerLabel.Text = e.SelectedItem.ToString();

            appContent.Content = Activator.CreateInstance<TimeTable>();

            navigationDrawer.ToggleDrawer();
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
